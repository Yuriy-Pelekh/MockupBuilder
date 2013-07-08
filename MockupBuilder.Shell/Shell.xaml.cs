namespace MockupBuilder.Shell
{
  using System;
  using System.Globalization;
  using System.Windows;
  using Microsoft.Practices.Prism.Logging;
  using Microsoft.Practices.Prism.Modularity;
  using ModuleTracking;

  public partial class Shell
  {
    public IModuleTracker ModuleTracker { get; set; }
    public IModuleManager ModuleManager { get; set; }
    public CallbackLogger Logger { get; set; }

    public Shell(IModuleManager moduleManager, IModuleTracker moduleTracker, CallbackLogger logger)
    {
      if (moduleManager == null)
        throw new ArgumentNullException("ModuleManager");

      if (moduleTracker == null)
        throw new ArgumentNullException("ModuleTracker");

      if (logger == null)
        throw new ArgumentNullException("Logger");

      ModuleManager = moduleManager;
      ModuleTracker = moduleTracker;
      Logger = logger;

      DataContext = ModuleTracker;

      Logger.Callback = Log;

      ModuleManager.LoadModuleCompleted += ModuleManagerLoadModuleCompleted;
      ModuleManager.ModuleDownloadProgressChanged += ModuleManagerModuleDownloadProgressChanged;

      InitializeComponent();
    }

    public void Log(string message, Category category, Priority priority)
    {
      TraceTextBox.Text += string.Format(CultureInfo.CurrentUICulture, "[{0}][{1}] {2}\r\n", category, priority, message);
    }

    private void ModuleCRequestModuleLoad(object sender, EventArgs e)
    {
      ModuleManager.LoadModule(WellKnownModuleNames.ModuleC);
    }

    private void ModuleERequestModuleLoad(object sender, EventArgs e)
    {
      ModuleManager.LoadModule(WellKnownModuleNames.ModuleE);
    }

    private void ModuleFRequestModuleLoad(object sender, EventArgs e)
    {
      ModuleManager.LoadModule(WellKnownModuleNames.ModuleF);
    }

    private void UserControlLoaded(object sender, RoutedEventArgs e)
    {
      Logger.ReplaySavedLogs();
    }

    private void ModuleManagerModuleDownloadProgressChanged(object sender, ModuleDownloadProgressChangedEventArgs e)
    {
      ModuleTracker.RecordModuleDownloading(e.ModuleInfo.ModuleName, e.BytesReceived, e.TotalBytesToReceive);
    }

    private void ModuleManagerLoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
    {
      ModuleTracker.RecordModuleLoaded(e.ModuleInfo.ModuleName);
    }
  }
}
