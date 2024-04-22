using Data;
using Model.ViewModel.Config;
using System.Windows;
using System.Windows.Controls;
using Utilities;

namespace TCRHelper.Desktop.Win.Tab.Preference;
/// <summary>
/// PreferenceTabPage.xaml 的交互逻辑
/// </summary>
public partial class PreferenceTabPage : UserControl {

    public AppConfig Config { get; set; }

    public PreferenceTabPage() {
        InitializeComponent();
        RootGrid.DataContext = Config;
    }
    private async void TestOcrApiButton_OnClick(object sender, RoutedEventArgs e) {
        UnFocusTextBox.Focus();
        await InteractionUtilities.CreateOcr(Config.OcrConfig);
    }
    private void ConfigApplyButton_OnClick(object sender, RoutedEventArgs e) {
        UnFocusTextBox.Focus();
        ConfigUtilities.SaveConfig(Config);
    }
    private void ModifyOcrConfig() {

    }

    //private void PreferenceTabPage_OnLoaded(object sender, RoutedEventArgs e) {
    //    ConfigOcrTypeComboBox.SelectedIndex = (int)Config.OcrConfig.Type;
    //    ConfigApiKeyTextBox.Text = Config.OcrConfig.ApiKey;
    //    ConfigSecretKeyTextBox.Text = Config.OcrConfig.SecretKey;
    //}
    private void PreferenceTabPage_OnLoaded(object sender, RoutedEventArgs e) {
        RootGrid.DataContext = Config;
    }
}
