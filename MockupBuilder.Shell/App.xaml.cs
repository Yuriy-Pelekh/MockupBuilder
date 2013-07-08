namespace MockupBuilder.Shell
{
  using System;
  using System.Windows;

  public partial class App
  {
    public App()
    {
      Startup += ApplicationStartup;
      Exit += ApplicationExit;
      UnhandledException += ApplicationUnhandledException;

      InitializeComponent();
    }

    private void ApplicationStartup(object sender, StartupEventArgs e)
    {
      new QuickStartBootstrapper().Run();
    }

    private void ApplicationExit(object sender, EventArgs e)
    {

    }

    private void ApplicationUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
    {
      if (!System.Diagnostics.Debugger.IsAttached)
      {
        e.Handled = true;
        Deployment.Current.Dispatcher.BeginInvoke(() => ReportErrorToDOM(e));
      }
    }

    private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
    {
      try
      {
        string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
        errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

        System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
      }
      catch { }
    }
  }
}
