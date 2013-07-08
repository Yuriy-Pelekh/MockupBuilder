namespace MockupBuilder.Shell
{
  using System;
  using System.Windows;
  using System.Windows.Input;
  using Microsoft.Practices.Prism.Modularity;

  public partial class ModuleControl
  {
    public ModuleControl()
    {
      InitializeComponent();
    }

    public event EventHandler RequestModuleLoad;

    private void RaiseRequestModuleLoad()
    {
      if (RequestModuleLoad != null)
      {
        RequestModuleLoad(this, EventArgs.Empty);
      }
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
      base.OnMouseLeftButtonUp(e);

      if (!e.Handled)
      {
        var moduleTrackingState = DataContext as ModuleTrackingState;
        if ((moduleTrackingState != null) && (moduleTrackingState.ExpectedInitializationMode == InitializationMode.OnDemand) && (moduleTrackingState.ModuleInitializationStatus == ModuleInitializationStatus.NotStarted))
        {
          RaiseRequestModuleLoad();
          e.Handled = true;
        }
      }
    }

    private void UpdateClickToLoadTextBlockVisibility()
    {
      var moduleTrackingState = DataContext as ModuleTrackingState;
      if ((moduleTrackingState != null)
          && (moduleTrackingState.ExpectedInitializationMode == InitializationMode.OnDemand)
          && (moduleTrackingState.ModuleInitializationStatus == ModuleInitializationStatus.NotStarted))
      {
        ClickToLoadTextBlock.Visibility = Visibility.Visible;
      }
      else
      {
        ClickToLoadTextBlock.Visibility = Visibility.Collapsed;
      }
    }

    private void UpdateLoadProgressTextBlockVisibility()
    {
      var moduleTrackingState = DataContext as ModuleTrackingState;
      if ((moduleTrackingState != null)
          && (moduleTrackingState.ExpectedDownloadTiming == DownloadTiming.InBackground)
          && (moduleTrackingState.ModuleInitializationStatus == ModuleInitializationStatus.Downloading))
      {
        LoadProgressPanel.Visibility = Visibility.Visible;
      }
      else
      {
        LoadProgressPanel.Visibility = Visibility.Collapsed;
      }
    }

    private void ContentControlLoaded(object sender, RoutedEventArgs e)
    {
      UpdateClickToLoadTextBlockVisibility();
      UpdateLoadProgressTextBlockVisibility();

      var moduleTrackingState = DataContext as ModuleTrackingState;
      if (moduleTrackingState != null)
      {
        moduleTrackingState.PropertyChanged += ModuleTrackingStatePropertyChanged;
      }
    }

    void ModuleTrackingStatePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      UpdateClickToLoadTextBlockVisibility();
      UpdateLoadProgressTextBlockVisibility();
    }
  }
}
