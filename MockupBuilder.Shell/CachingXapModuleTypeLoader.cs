using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using Microsoft.Practices.Prism.Modularity;

namespace MockupBuilder.Shell
{
  public class CachingXapModuleTypeLoader : XapModuleTypeLoader
  {
    protected override IFileDownloader CreateDownloader()
    {
      return new CachingFileDownloader();
    }
  }

  public class CachingFileDownloader : IFileDownloader
  {
    private readonly WebClient webClient;

    public CachingFileDownloader()
    {
      webClient = new WebClient();
      webClient.OpenReadCompleted += WebClientOpenReadCompleted;
    }

    private void WebClientOpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
    {
      var fileName = e.UserState as Uri;
      using (var isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
      {
        var stream = isoStorage.CreateFile(fileName.ToString());
        WriteModuleFile(stream, e.Result);
      }

      if (DownloadCompleted != null)
        DownloadCompleted(this, new DownloadCompletedEventArgs(e.Result, e.Error, e.Cancelled, e.UserState));
    }

    public void WriteModuleFile(IsolatedStorageFileStream isoStorageStream, Stream moduleStream)
    {
      var size = 4096;
      var bytes = new byte[4096];
      int numBytes;
      
      while ((numBytes = moduleStream.Read(bytes, 0, size)) > 0)
        isoStorageStream.Write(bytes, 0, numBytes);
    }

    public void DownloadAsync(Uri uri, object userToken)
    {
      using (var isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
      {
        if (isoStorage.FileExists(uri.ToString()))
        {
          Stream moduleStream = isoStorage.OpenFile(uri.ToString(), FileMode.Open);
          if (DownloadCompleted != null)
            DownloadCompleted(this, new DownloadCompletedEventArgs(moduleStream, null, false, uri));
          return;
        }
      }

      webClient.OpenReadAsync(uri, userToken);
    }

    public event EventHandler<DownloadCompletedEventArgs> DownloadCompleted;

    public event EventHandler<DownloadProgressChangedEventArgs> DownloadProgressChanged;
  }
}
