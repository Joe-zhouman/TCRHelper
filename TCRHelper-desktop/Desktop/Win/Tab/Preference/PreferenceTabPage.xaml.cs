using Data;
using Data.Db;
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

    private void ConfigTestDbConnectButton_OnClickButton_OnClick(object sender, RoutedEventArgs e) {
        DbHelper helper = new DbHelper(Config.DbConfig);
        try {
            helper.TextDbConnect();
            InteractionUtilities.ShowAndHideTooltip("数据库连接成功!");
        }
        catch(Exception exception) {
            InteractionUtilities.ShowErrorMessageBox(@$"数据库连接失败, 具体原因如下:
{exception}");
        }
    }
}
