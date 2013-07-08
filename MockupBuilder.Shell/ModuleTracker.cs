namespace MockupBuilder.Shell
{
  using Microsoft.Practices.Prism.Logging;
  using Microsoft.Practices.Prism.Modularity;
  using ModuleTracking;
  using System;

  public class ModuleTracker : IModuleTracker
  {
    private readonly ModuleTrackingState moduleATrackingState;
    private readonly ModuleTrackingState moduleBTrackingState;
    private readonly ModuleTrackingState moduleCTrackingState;
    private readonly ModuleTrackingState moduleDTrackingState;
    private readonly ModuleTrackingState moduleETrackingState;
    private readonly ModuleTrackingState moduleFTrackingState;

    private readonly ILoggerFacade logger;

    public ModuleTracker(ILoggerFacade logger)
    {
      if (logger == null)
      {
        throw new ArgumentNullException("logger");
      }
      this.logger = logger;

      moduleATrackingState = new ModuleTrackingState
                               {
                                 ModuleName = WellKnownModuleNames.ModuleA,
                                 ExpectedDiscoveryMethod = DiscoveryMethod.ApplicationReference,
                                 ExpectedInitializationMode = InitializationMode.WhenAvailable,
                                 ExpectedDownloadTiming = DownloadTiming.WithApplication,
                                 ConfiguredDependencies = WellKnownModuleNames.ModuleD,
                               };

      moduleBTrackingState = new ModuleTrackingState
                               {
                                 ModuleName = WellKnownModuleNames.ModuleB,
                                 ExpectedDiscoveryMethod = DiscoveryMethod.XamlManifest,
                                 ExpectedInitializationMode = InitializationMode.WhenAvailable,
                                 ExpectedDownloadTiming = DownloadTiming.InBackground,
                               };
      moduleCTrackingState = new ModuleTrackingState
                               {
                                 ModuleName = WellKnownModuleNames.ModuleC,
                                 ExpectedDiscoveryMethod = DiscoveryMethod.ApplicationReference,
                                 ExpectedInitializationMode = InitializationMode.OnDemand,
                                 ExpectedDownloadTiming = DownloadTiming.WithApplication,
                               };
      moduleDTrackingState = new ModuleTrackingState
                               {
                                 ModuleName = WellKnownModuleNames.ModuleD,
                                 ExpectedDiscoveryMethod = DiscoveryMethod.XamlManifest,
                                 ExpectedInitializationMode = InitializationMode.WhenAvailable,
                                 ExpectedDownloadTiming = DownloadTiming.InBackground,
                               };
      moduleETrackingState = new ModuleTrackingState
                               {
                                 ModuleName = WellKnownModuleNames.ModuleE,
                                 ExpectedDiscoveryMethod = DiscoveryMethod.XamlManifest,
                                 ExpectedInitializationMode = InitializationMode.OnDemand,
                                 ExpectedDownloadTiming = DownloadTiming.InBackground,
                               };
      moduleFTrackingState = new ModuleTrackingState
                               {
                                 ModuleName = WellKnownModuleNames.ModuleF,
                                 ExpectedDiscoveryMethod = DiscoveryMethod.XamlManifest,
                                 ExpectedInitializationMode = InitializationMode.OnDemand,
                                 ExpectedDownloadTiming = DownloadTiming.InBackground,
                                 ConfiguredDependencies = WellKnownModuleNames.ModuleE,
                               };
    }

    public ModuleTrackingState ModuleATrackingState
    {
      get { return moduleATrackingState; }
    }

    public ModuleTrackingState ModuleBTrackingState
    {
      get { return moduleBTrackingState; }
    }

    public ModuleTrackingState ModuleCTrackingState
    {
      get { return moduleCTrackingState; }
    }

    public ModuleTrackingState ModuleDTrackingState
    {
      get { return moduleDTrackingState; }
    }

    public ModuleTrackingState ModuleETrackingState
    {
      get { return moduleETrackingState; }
    }

    public ModuleTrackingState ModuleFTrackingState
    {
      get { return moduleFTrackingState; }
    }

    public void RecordModuleConstructed(string moduleName)
    {
      ModuleTrackingState moduleTrackingState = GetModuleTrackingState(moduleName);
      if (moduleTrackingState != null)
      {
        moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Constructed;
      }

      logger.Log(string.Format("'{0}' module constructed.", moduleName), Category.Debug, Priority.Low);
    }

    public void RecordModuleDownloading(string moduleName, long bytesReceived, long totalBytesToReceive)
    {
      ModuleTrackingState moduleTrackingState = GetModuleTrackingState(moduleName);
      if (moduleTrackingState != null)
      {
        moduleTrackingState.BytesReceived = bytesReceived;
        moduleTrackingState.TotalBytesToReceive = totalBytesToReceive;

        if (bytesReceived < totalBytesToReceive)
          moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Downloading;
        else
          moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Downloaded;
      }

      logger.Log(string.Format("'{0}' module is loading {1}/{2} bytes.", moduleName, bytesReceived, totalBytesToReceive), Category.Debug, Priority.Low);
    }

    public void RecordModuleInitialized(string moduleName)
    {
      ModuleTrackingState moduleTrackingState = GetModuleTrackingState(moduleName);
      if (moduleTrackingState != null)
      {
        moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Initialized;
      }

      logger.Log(string.Format("{0} module initialized.", moduleName), Category.Debug, Priority.Low);
    }

    public void RecordModuleLoaded(string moduleName)
    {
      logger.Log(string.Format("'{0}' module loaded.", moduleName), Category.Debug, Priority.Low);
    }

    private ModuleTrackingState GetModuleTrackingState(string moduleName)
    {
      switch (moduleName)
      {
        case WellKnownModuleNames.ModuleA:
          return ModuleATrackingState;
        case WellKnownModuleNames.ModuleB:
          return ModuleBTrackingState;
        case WellKnownModuleNames.ModuleC:
          return ModuleCTrackingState;
        case WellKnownModuleNames.ModuleD:
          return ModuleDTrackingState;
        case WellKnownModuleNames.ModuleE:
          return ModuleETrackingState;
        case WellKnownModuleNames.ModuleF:
          return ModuleFTrackingState;
        default:
          return null;
      }
    }
  }
}
