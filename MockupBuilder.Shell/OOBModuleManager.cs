using System.Collections.Generic;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;

namespace MockupBuilder.Shell
{
  public class OOBModuleManager : ModuleManager
  {
    private IEnumerable<IModuleTypeLoader> typeLoaders;

    public OOBModuleManager(IModuleInitializer moduleInitializer, IModuleCatalog moduleCatalog, ILoggerFacade loggerFacade)
      : base(moduleInitializer, moduleCatalog, loggerFacade)
    { }
    
    public override IEnumerable<IModuleTypeLoader> ModuleTypeLoaders
    {
      get { return typeLoaders ?? (typeLoaders = new List<IModuleTypeLoader> {new CachingXapModuleTypeLoader()}); }
      set { typeLoaders = value; }
    }
  }
}
