namespace ModuleB
{
  using System;
  using Microsoft.Practices.Prism.Modularity;
  using ModuleTracking;

  public class ModuleB : IModule
  {
    private readonly IModuleTracker moduleTracker;

    public ModuleB(IModuleTracker moduleTracker)
    {
      if (moduleTracker == null)
        throw new ArgumentNullException("ModuleTracker");

      this.moduleTracker = moduleTracker;
      this.moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleB);
    }

    public void Initialize()
    {
      moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleB);
    }
  }
}
