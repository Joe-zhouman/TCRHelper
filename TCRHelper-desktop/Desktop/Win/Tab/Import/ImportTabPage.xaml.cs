using Data.Db;
using Model.ViewModel.Config;
using Model.ViewModel.Db;
using System.Windows;
using System.Windows.Controls;
using Utilities;
using Utilities.RefQuery;

namespace TCRHelper.Desktop.Win.Tab.Import;
/// <summary>
/// ImportTabPage.xaml 的交互逻辑
/// </summary>
public partial class ImportTabPage : UserControl {
    private AppConfig _appConfig;
    public AppConfig Config { get => _appConfig; set => _appConfig = value; }
    private MaterialViewModel _materialViewModel = new MaterialViewModel();
    private ReferenceViewModel _ref = new ReferenceViewModel();
    private SurfaceViewModel _surf1 = new SurfaceViewModel();
    private SurfaceViewModel _surf2 = new SurfaceViewModel();
    private ContactViewModel _contact = new ContactViewModel();
    private static DbHelper _dbHelper;
    public ImportTabPage() {
        InitializeComponent();
        ImportRefViewModelGroupBox.RootGrid.DataContext = _ref;
        ImportMatPropViewModelGroupBox.RootGrid.DataContext = _materialViewModel;
        _contact.Surface1 = _surf1;
        _contact.Surface2 = _surf2;
        ContactGroupBox.DataContext = _contact;
    }
    private void ImportFromPlotButton_OnClick(object sender, RoutedEventArgs e) {
        PlotDataCollectionWindows w = new();
        w.ShowDialog();
    }

    private void ImportFromTabularButton_OnClick(object sender, RoutedEventArgs e) {
        TabularDataCollectionWindow w = new(Config);
        w.ShowDialog();
    }

    private async void SearchButton_OnClick(object sender, RoutedEventArgs e) {
        try {
            VirtualTextBox.Focus();
            IRefQueryProduct query = RefQueryFactory.Create(RefQueryApi.CROSSREF);
            await query.GetRef(_ref);
        }
        catch(Exception exception) {
            InteractionUtilities.ShowErrorMessageBox(exception.Message);
        }
    }

    private void TestButton_Click(object sender, RoutedEventArgs e) {
        InteractionUtilities.ShowAndHideTooltip(_ref.Title.Value, 3);
    }

    private void InsertIntoDbButton_OnClick(object sender, RoutedEventArgs e) {
        try { InteractionUtilities.ShowAndHideTooltip(_dbHelper.InsertRef(_ref) ? "数据插入成功" : "记录已存在!"); }
        catch(Exception exception) { ShowDbInsertError(exception); }
    }

    private static void ShowDbInsertError(Exception exception) => InteractionUtilities.ShowErrorMessageBox($"向数据库中插入是遇到未知错误, 前检查相应设置.\n具体错误如下:\n{exception}");

    private void ImportTabPage_OnLoaded(object sender, RoutedEventArgs e) {
        _dbHelper = InteractionUtilities.CreateDbHelper(Config.DbConfig);
    }
}
