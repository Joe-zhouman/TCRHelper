using Data;
using Model;
using System.Windows;
namespace TCRHelper.Desktop;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    private AppConfig _appConfig;
    //private ConfigUtilities _configUtilities;

    public MainWindow() {
        InitializeComponent();

        //_configUtilities = new ConfigUtilities();
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
        _appConfig = ConfigUtilities.LoadConfig();
        ImportTabPage.Config = _appConfig;
        PreferenceTabPage.Config = _appConfig;
    }


}