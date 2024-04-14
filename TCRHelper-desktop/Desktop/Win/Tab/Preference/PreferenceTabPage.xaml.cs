using Data;
using Model.Config;
using System.Windows;
using System.Windows.Controls;
using Utilities;

namespace TCRHelper.Desktop.Win.Tab.Preference;
/// <summary>
/// PreferenceTabPage.xaml 的交互逻辑
/// </summary>
public partial class PreferenceTabPage : UserControl {
    private AppConfig _config;

    public AppConfig Config {
        get => _config;
        set {
            _config = value;
            ConfigOcrTypeComboBox.SelectedIndex = (int)_config.OcrConfig.Type;
            ConfigApiKeyTextBox.Text = _config.OcrConfig.ApiKey;
            ConfigSecretKeyTextBox.Text = _config.OcrConfig.SecretKey;
        }
    }

    public PreferenceTabPage() {
        InitializeComponent();
        ConfigOcrTypeComboBox.ItemsSource = new List<string> { "百度标准版" };
    }
    private async void TestOcrApiButton_OnClick(object sender, RoutedEventArgs e) {
        ModifyOcrConfig();
        await InteractionUtilities.CreateOcr(Config.OcrConfig);
    }
    private void ConfigApplyButton_OnClick(object sender, RoutedEventArgs e) {
        ModifyOcrConfig();
        ConfigUtilities.SaveConfig(Config);
    }
    private void ModifyOcrConfig() {
        Config.OcrConfig.Type = (OcrType)ConfigOcrTypeComboBox.SelectedIndex;
        Config.OcrConfig.ApiKey = ConfigApiKeyTextBox.Text;
        Config.OcrConfig.SecretKey = ConfigSecretKeyTextBox.Text;
    }

    //private void PreferenceTabPage_OnLoaded(object sender, RoutedEventArgs e) {
    //    ConfigOcrTypeComboBox.SelectedIndex = (int)Config.OcrConfig.Type;
    //    ConfigApiKeyTextBox.Text = Config.OcrConfig.ApiKey;
    //    ConfigSecretKeyTextBox.Text = Config.OcrConfig.SecretKey;
    //}
}
