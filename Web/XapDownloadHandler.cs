namespace Web
{
  using System;
  using System.IO;
  using System.Threading;
  using System.Web;

  public class XapDownloadHandler : IHttpHandler
  {
    private const double DefaultTransmitChunkPercent = 0.1;

    public bool IsReusable { get { return true; } }

    public void ProcessRequest(HttpContext context)
    {
      string physicalPath = context.Server.MapPath(context.Request.Path);

      if (context.Request.Path.Contains("ModuleB"))
        SlowlyTransmitFile(context, physicalPath, 100);
      else if (context.Request.Path.Contains("ModuleD"))
        SlowlyTransmitFile(context, physicalPath, 200);
      else if (context.Request.Path.Contains("ModuleE"))
        SlowlyTransmitFile(context, physicalPath, 600);
      else if (context.Request.Path.Contains("ModuleF"))
        SlowlyTransmitFile(context, physicalPath, 300);
      else
        SlowlyTransmitFile(context, physicalPath, 0);
    }

    private void SlowlyTransmitFile(HttpContext context, string path, int milliSecondsDelayPerChunk)
    {
      context.Response.BufferOutput = false;

      long fileSize = -1L;
      if (File.Exists(path))
      {
        var fileInfo = new FileInfo(path);
        fileSize = fileInfo.Length;
      }

      context.Response.AppendHeader("Content-Length", fileSize.ToString());

      byte[] moduleBuffer = File.ReadAllBytes(path);
      byte[] chunkBufer;
      int chunkSize = (int)(moduleBuffer.Length * DefaultTransmitChunkPercent);
      int chunkCount = moduleBuffer.Length / chunkSize;
      int leftOverSize = moduleBuffer.Length % chunkSize;
      int i = 0;

      if (chunkCount > 0)
      {
        chunkBufer = new byte[chunkSize];
        while (i < chunkCount * chunkSize)
        {
          Array.Copy(moduleBuffer, i, chunkBufer, 0, chunkSize);
          context.Response.BinaryWrite(chunkBufer);
          i += chunkSize;

          Thread.Sleep(milliSecondsDelayPerChunk);
        }
      }

      if (leftOverSize != 0)
      {
        chunkBufer = new byte[leftOverSize];
        Array.Copy(moduleBuffer, i, chunkBufer, 0, leftOverSize);
        context.Response.BinaryWrite(chunkBufer);
      }
    }
  }
}