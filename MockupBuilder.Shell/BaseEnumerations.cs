namespace MockupBuilder.Shell
{
  public enum DownloadTiming
  {
    WithApplication,
    InBackground
  }

  public enum DiscoveryMethod
  {
    ApplicationReference,
    XamlManifest,
    ConfigurationManifest,
    DirectorySweep
  }

  public enum ModuleInitializationStatus
  {
    NotStarted,
    Downloading,
    Downloaded,
    Constructed,
    Initialized
  }
}