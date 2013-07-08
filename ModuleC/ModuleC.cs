namespace ModuleC
{
  using System;
  using Microsoft.Practices.Prism.Modularity;
  using ModuleTracking;

  public class ModuleC : IModule
  {
    private readonly IModuleTracker moduleTracker;

    public ModuleC(IModuleTracker moduleTracker)
    {
      if (moduleTracker == null)
        throw new ArgumentNullException("ModuleTracker");

      this.moduleTracker = moduleTracker;
      this.moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleC);
    }

    public void Initialize()
    {
      moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleC);
    }
  }
}
