namespace ModuleTracking
{
  public interface IModuleTracker
  {
    void RecordModuleDownloading(string moduleName, long bytesReceived, long totalBytesToReceive);
    void RecordModuleLoaded(string moduleName);
    void RecordModuleConstructed(string moduleName);
    void RecordModuleInitialized(string moduleName);
  }
}