using Data;
using Model;
using System.Windows;
using Utilities;

namespace TCRHelper_desktop;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    private AppConfig _appConfig;
    //private ConfigUtilities _configUtilities;

    public MainWindow() {
        InitializeComponent();
        ConfigOcrTypeComboBox.ItemsSource = new List<string> { "百度标准版" };
        //_configUtilities = new ConfigUtilities();
    }

    private void ImportFromPlotButton_OnClick(object sender, RoutedEventArgs e) {
        PlotDataCollectionWindows w = new();
        w.ShowDialog();
    }

    private void ImportFromTabularButton_OnClick(object sender, RoutedEventArgs e) {
        TabularDataCollectionWindow w = new(_appConfig);
        w.ShowDialog();
    }

    private async void TestOcrApiButton_OnClick(object sender, RoutedEventArgs e) {
        ModifyOcrConfig();
        await InteractionUtilities.CreateOcr(_appConfig.OcrConfig);
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
        _appConfig = ConfigUtilities.LoadConfig();
        ConfigOcrTypeComboBox.SelectedIndex = (int)_appConfig.OcrConfig.Type;
        ConfigApiKeyTextBox.Text = _appConfig.OcrConfig.ApiKey;
        ConfigSecretKeyTextBox.Text = _appConfig.OcrConfig.SecretKey;
    }

    private void ConfigApplyButton_OnClick(object sender, RoutedEventArgs e) {
        ModifyOcrConfig();
        ConfigUtilities.SaveConfig(_appConfig);
    }

    private void ModifyOcrConfig() {
        _appConfig.OcrConfig.Type = (OcrType)ConfigOcrTypeComboBox.SelectedIndex;
        _appConfig.OcrConfig.ApiKey = ConfigApiKeyTextBox.Text;
        _appConfig.OcrConfig.SecretKey = ConfigSecretKeyTextBox.Text;
    }
}