using System.Windows;

namespace TCRHelper_desktop;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
    }

    private void ImportFromPlotButton_OnClick(object sender, RoutedEventArgs e) {
        PlotDataCollectionWindows w = new();
        w.ShowDialog();
    }

    private void ImportFromTabularButton_OnClick(object sender, RoutedEventArgs e) {
        TabularDataCollectionWindow w = new();
        w.ShowDialog();
    }
}