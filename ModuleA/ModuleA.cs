namespace ModuleA
{
  using System;
  using Microsoft.Practices.Prism.Logging;
  using Microsoft.Practices.Prism.Modularity;
  using ModuleTracking;

  public class ModuleA : IModule
  {
    private readonly ILoggerFacade logger;
    private readonly IModuleTracker moduleTracker;

    public ModuleA(ILoggerFacade logger, IModuleTracker moduleTracker)
    {
      if (logger == null)
        throw new ArgumentNullException("Logger");

      if (moduleTracker == null)
        throw new ArgumentNullException("ModuleTracker");

      this.logger = logger;
      this.moduleTracker = moduleTracker;
      this.moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleA);
    }

    public void Initialize()
    {
      logger.Log("ModuleA demonstrates logging during Initialize().", Category.Info, Priority.Medium);
      moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleA);
    }
  }
}
