namespace ModuleD
{
  using System;
  using Microsoft.Practices.Prism.Modularity;
  using ModuleTracking;

  public class ModuleD : IModule
  {
    private readonly IModuleTracker moduleTracker;

    public ModuleD(IModuleTracker moduleTracker)
    {
      if (moduleTracker == null)
        throw new ArgumentNullException("ModuleTracker");

      this.moduleTracker = moduleTracker;
      this.moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleD);
    }

    public void Initialize()
    {
      moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleD);
    }
  }
}
