namespace ModuleE
{
  using System;
  using Microsoft.Practices.Prism.Modularity;
  using ModuleTracking;

  public class ModuleE : IModule
  {
    private readonly IModuleTracker moduleTracker;

    public ModuleE(IModuleTracker moduleTracker)
    {
      if (moduleTracker == null)
        throw new ArgumentNullException("ModuleTracker");

      this.moduleTracker = moduleTracker;
      this.moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleE);
    }

    public void Initialize()
    {
      moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleE);
    }
  }
}
