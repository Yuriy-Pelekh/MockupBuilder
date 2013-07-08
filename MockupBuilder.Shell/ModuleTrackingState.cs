namespace MockupBuilder.Shell
{
  using System.ComponentModel;
  using Microsoft.Practices.Prism.Modularity;

  public class ModuleTrackingState : INotifyPropertyChanged
  {
    private string moduleName;
    private ModuleInitializationStatus moduleInitializationStatus;
    private DiscoveryMethod expectedDiscoveryMethod;
    private InitializationMode expectedInitializationMode;
    private DownloadTiming expectedDownloadTiming;
    private string configuredDependencies = "(none)";
    private long bytesReceived;
    private long totalBytesToReceive;

    public string ModuleName
    {
      get { return moduleName; }
      set
      {
        if (moduleName != value)
        {
          moduleName = value;
          RaisePropertyChanged(PropertyNames.ModuleName);
        }
      }
    }

    public ModuleInitializationStatus ModuleInitializationStatus
    {
      get { return moduleInitializationStatus; }
      set
      {
        if (moduleInitializationStatus != value)
        {
          moduleInitializationStatus = value;
          RaisePropertyChanged(PropertyNames.ModuleInitializationStatus);
        }
      }
    }

    public DiscoveryMethod ExpectedDiscoveryMethod
    {
      get { return expectedDiscoveryMethod; }
      set
      {
        if (expectedDiscoveryMethod != value)
        {
          expectedDiscoveryMethod = value;
          RaisePropertyChanged(PropertyNames.ExpectedDiscoveryMethod);
        }
      }
    }

    public InitializationMode ExpectedInitializationMode
    {
      get { return expectedInitializationMode; }
      set
      {
        if (expectedInitializationMode != value)
        {
          expectedInitializationMode = value;
          RaisePropertyChanged(PropertyNames.ExpectedInitializationMode);
        }
      }
    }

    public DownloadTiming ExpectedDownloadTiming
    {
      get { return expectedDownloadTiming; }
      set
      {
        if (expectedDownloadTiming != value)
        {
          expectedDownloadTiming = value;
          RaisePropertyChanged(PropertyNames.ExpectedDownloadTiming);
        }
      }
    }

    public string ConfiguredDependencies
    {
      get { return configuredDependencies; }
      set
      {
        if (configuredDependencies != value)
        {
          configuredDependencies = value;
          RaisePropertyChanged(PropertyNames.ConfiguredDependencies);
        }
      }
    }

    public long BytesReceived
    {
      get { return bytesReceived; }
      set
      {
        if (bytesReceived != value)
        {
          bytesReceived = value;
          RaisePropertyChanged(PropertyNames.BytesReceived);
          RaisePropertyChanged(PropertyNames.DownloadProgressPercentage);
        }
      }
    }

    public long TotalBytesToReceive
    {
      get { return totalBytesToReceive; }
      set
      {
        if (totalBytesToReceive != value)
        {
          totalBytesToReceive = value;
          RaisePropertyChanged(PropertyNames.TotalBytesToReceive);
          RaisePropertyChanged(PropertyNames.DownloadProgressPercentage);
        }
      }
    }

    public int DownloadProgressPercentage
    {
      get
      {
        if (bytesReceived < totalBytesToReceive)
        {
          return (int)(bytesReceived * 100.0 / totalBytesToReceive);
        }

        return 100;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void RaisePropertyChanged(string propertyName)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public static class PropertyNames
    {
      public const string ModuleName = "ModuleName";
      public const string ModuleInitializationStatus = "ModuleInitializationStatus";
      public const string ExpectedDiscoveryMethod = "ExpectedDiscoveryMethod";
      public const string ExpectedInitializationMode = "ExpectedInitializationMode";
      public const string ExpectedDownloadTiming = "ExpectedDownloadTiming";
      public const string ConfiguredDependencies = "ConfiguredDependencies";
      public const string BytesReceived = "BytesReceived";
      public const string TotalBytesToReceive = "TotalBytesToReceive";
      public const string DownloadProgressPercentage = "DownloadProgressPercentage";
    }
  }
}
