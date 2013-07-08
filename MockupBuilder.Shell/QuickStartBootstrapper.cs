namespace MockupBuilder.Shell
{
  using System;
  using System.Windows;
  using Microsoft.Practices.Prism.Logging;
  using Microsoft.Practices.Prism.Modularity;
  using Microsoft.Practices.Prism.UnityExtensions;
  using Microsoft.Practices.ServiceLocation;
  using Microsoft.Practices.Unity;
  using ModuleTracking;
  using Modularity = Microsoft.Practices.Prism.Modularity;

  public class QuickStartBootstrapper : UnityBootstrapper
  {
    private readonly CallbackLogger callbackLogger = new CallbackLogger();

    protected override DependencyObject CreateShell()
    {
      return ServiceLocator.Current.GetInstance<Shell>();
    }

    protected override void InitializeShell()
    {
      base.InitializeShell();
      Application.Current.RootVisual = (UIElement)Shell;
    }

    protected override ILoggerFacade CreateLogger()
    {
      return callbackLogger;
    }

    protected override void ConfigureContainer()
    {
      RegisterTypeIfMissing(typeof(IModuleManager), typeof(OOBModuleManager), true);
      base.ConfigureContainer();
      RegisterTypeIfMissing(typeof(IModuleTracker), typeof(ModuleTracker), true);
      Container.RegisterInstance(callbackLogger);
    }

    protected override IModuleCatalog CreateModuleCatalog()
    {
      return Modularity.ModuleCatalog.CreateFromXaml(new Uri("/MockupBuilder.Shell;component/ModulesCatalog.xaml", UriKind.Relative));
    }

    protected override void ConfigureModuleCatalog()
    {
      Type moduleAType = typeof(ModuleA.ModuleA);
      ModuleCatalog.AddModule(new ModuleInfo(moduleAType.Name, moduleAType.AssemblyQualifiedName, WellKnownModuleNames.ModuleD));

      Type moduleCType = typeof(ModuleC.ModuleC);
      ModuleCatalog.AddModule(new ModuleInfo
                                {
                                  ModuleName = moduleCType.Name,
                                  ModuleType = moduleCType.AssemblyQualifiedName,
                                  InitializationMode = InitializationMode.OnDemand
                                });
    }
  }
}


