using Model;
using System.Windows;
using System.Windows.Controls;

namespace TCRHelper.Desktop.Win.Tab.Import;
/// <summary>
/// ImportTabPage.xaml 的交互逻辑
/// </summary>
public partial class ImportTabPage : UserControl {
    private AppConfig _appConfig;
    public AppConfig Config { get => _appConfig; set => _appConfig = value; }
    public ImportTabPage() {
        InitializeComponent();
    }
    private void ImportFromPlotButton_OnClick(object sender, RoutedEventArgs e) {
        PlotDataCollectionWindows w = new();
        w.ShowDialog();
    }

    private void ImportFromTabularButton_OnClick(object sender, RoutedEventArgs e) {
        TabularDataCollectionWindow w = new(Config);
        w.ShowDialog();
    }
}
