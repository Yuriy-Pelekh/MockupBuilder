namespace ModuleF
{
  using System;
  using Microsoft.Practices.Prism.Modularity;
  using ModuleTracking;

  public class ModuleF : IModule
  {
    private readonly IModuleTracker moduleTracker;

    public ModuleF(IModuleTracker moduleTracker)
    {
      if (moduleTracker == null)
        throw new ArgumentNullException("ModuleTracker");

      this.moduleTracker = moduleTracker;
      this.moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleF);
    }

    public void Initialize()
    {
      moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleF);
    }
  }
}
