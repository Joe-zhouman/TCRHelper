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
    private MatListComboBoxViewModel _matList = new();
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

    private async void SearchOnlineButton_OnClick(object sender, RoutedEventArgs e) {
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
        VirtualTextBox.Focus();
        try { InteractionUtilities.ShowAndHideTooltip(_dbHelper.InsertRef(_ref) ? "数据插入成功" : "记录已存在!"); }
        catch(Exception exception) { ShowDbError(exception, "插入"); }
    }

    private static void ShowDbError(Exception exception, string op) => InteractionUtilities.ShowErrorMessageBox($"向数据库中{op}时遇到未知错误, 前检查相应设置.\n具体错误如下:\n{exception}");

    private void ImportTabPage_OnLoaded(object sender, RoutedEventArgs e) {
        _dbHelper = InteractionUtilities.CreateDbHelper(Config.DbConfig);
        _matList.MatList = _dbHelper.GetMatList();
        ImportMatListComboBox.ItemsSource = _matList.MatList;
        Surf1MatListComboBox.ItemsSource = _matList.MatList;
        Surf2MatListComboBox.ItemsSource = _matList.MatList;
    }

    private void SearchDatabaseButton_OnClick(object sender, RoutedEventArgs e) {
        VirtualTextBox.Focus();
        try { InteractionUtilities.ShowAndHideTooltip(_dbHelper.SearchRef(_ref) ? "数据查找成功" : "记录不存在!"); }
        catch(Exception exception) { ShowDbError(exception, "查找"); }
    }

    private void ClearRefButton_OnClick(object sender, RoutedEventArgs e) { }

    private void InsertMatToDbButton_OnClick(object sender, RoutedEventArgs e) {
        VirtualTextBox.Focus();
        try { InteractionUtilities.ShowAndHideTooltip(_dbHelper.InsertMat(_materialViewModel) ? "数据插入成功" : "记录已存在!"); }
        catch(Exception exception) { ShowDbError(exception, "插入"); }
    }

    private void SearchFromNameTextBoxButton_OnClick(object sender, RoutedEventArgs e) {
        VirtualTextBox.Focus();
        SearchMatFromDb();
    }

    private void SearchMatFromDb() {
        try {
            InteractionUtilities.ShowAndHideTooltip(_dbHelper.SearchMat(_materialViewModel) ? "查询成功!" : "记录不存在!");
        }
        catch(Exception exception) {
            ShowDbError(exception, "查找");
        }
    }

    private void ImportMatListComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
        _materialViewModel.Name.Value = ImportMatListComboBox.SelectedValue.ToString()!;
        VirtualTextBox.Focus();
        SearchMatFromDb();
    }

    private void UpdateMatButton_OnClick(object sender, RoutedEventArgs e) {
        VirtualTextBox.Focus();
        try {
            InteractionUtilities.ShowAndHideTooltip(_dbHelper.UpdateMat(_materialViewModel) ? "更新成功!" : "记录不存在!");
        }
        catch(Exception exception) {
            ShowDbError(exception, "更新");
        }
    }
}
