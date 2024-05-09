using Data.Db;
using Model.ViewModel.Config;
using Model.ViewModel.Db;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
    private IList<Point>? _points;

    public ImportTabPage() {
        InitializeComponent();
        ImportRefViewModelGroupBox.RootGrid.DataContext = _ref;
        ImportMatPropViewModelGroupBox.RootGrid.DataContext = _materialViewModel;
        _contact.Surface1 = _surf1;
        _contact.Surface2 = _surf2;
        ContactGroupBox.DataContext = _contact;
        this.Loaded += this.ImportTabPage_OnLoaded;
    }



    public void ShowPoints(List<Point> points) {
        _points = points;
        ContactDataGrid.ItemsSource = points;
    }
    private void ImportFromPlotButton_OnClick(object sender, RoutedEventArgs e) {
        PlotDataCollectionWindows w = new(this);
        w.Show();
    }

    private void ImportFromTabularButton_OnClick(object sender, RoutedEventArgs e) {
        TabularDataCollectionWindow w = new(Config);
        w.Show();
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
        try { _matList.MatList = _dbHelper.GetMatList(); }
        catch { }
        finally {
            Loaded -= ImportTabPage_OnLoaded;
        }
    }

    private void SearchDatabaseButton_OnClick(object sender, RoutedEventArgs e) {
        VirtualTextBox.Focus();
        try { InteractionUtilities.ShowAndHideTooltip(_dbHelper.SearchRef(_ref) ? "数据查找成功" : "记录不存在!"); }
        catch(Exception exception) { ShowDbError(exception, "查找"); }
    }

    private void ClearRefButton_OnClick(object sender, RoutedEventArgs e) { }

    private void InsertMatToDbButton_OnClick(object sender, RoutedEventArgs e) {
        VirtualTextBox.Focus();
        try {
            InteractionUtilities.ShowAndHideTooltip(_dbHelper.InsertMat(_materialViewModel) ? "数据插入成功" : "记录已存在!");
            _matList.MatList = _dbHelper.GetMatList();
            ImportMatListComboBox.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
            Surf1MatListComboBox.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
            Surf2MatListComboBox.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
        }
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

    private void ContactCheckBox_Checked(object sender, RoutedEventArgs e) {
        ChangeTextBoxStatusFromCheckBox(sender, false);
    }

    private static void ChangeTextBoxStatusFromCheckBox(object sender, bool status) {
        CheckBox checkBox = (CheckBox)sender;
        // 找到同一个StackPanel下的所有TextBox，并设置为不可编辑状态
        StackPanel stackPanel = (StackPanel)checkBox.Parent;
        foreach(var child in stackPanel.Children) {
            if(child is TextBox textBox) {
                textBox.IsEnabled = status;
            }
        }
    }
    private void ContactCheckBox_Unchecked(object sender, RoutedEventArgs e) {
        ChangeTextBoxStatusFromCheckBox(sender, true);
    }

    private void GetNumCheckedBox(DependencyObject parent, ref int numCheckedBox) {
        for(int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++) {
            DependencyObject child = VisualTreeHelper.GetChild(parent, i);

            if(child is CheckBox checkBox) {
                if(checkBox.IsChecked is not null && (bool)checkBox.IsChecked) {
                    numCheckedBox++;
                }
            }
            else {
                // 递归检查子控件
                GetNumCheckedBox(child, ref numCheckedBox);
            }
        }
    }

    private void InsertContactToDb() {
        try { InteractionUtilities.ShowAndHideTooltip(_dbHelper.InsertResistance(_contact) ? "数据插入成功" : "记录已存在!"); }
        catch(Exception exception) { ShowDbError(exception, "插入"); }
    }

    private void ImportContactButton_OnClick(object sender, RoutedEventArgs e) {
        int numCheckedBox = 0;
        GetNumCheckedBox(ContactGroupBox, ref numCheckedBox);
        if(numCheckedBox is 1 or > 2) {
            InteractionUtilities.ShowErrorMessageBox($"请检查被勾选的CheckBox数量, 应该为0或2!");
            return;
        }

        if(numCheckedBox is 2 && ThermalConductivityCheckBox.IsChecked is not null &&
            !(bool)ThermalConductivityCheckBox.IsChecked) {
            InteractionUtilities.ShowErrorMessageBox($"勾选了两个CheckBox, 但未勾选接触热阻相关的CheckBox, 请检查正确性!");
            return;
        }

        VirtualTextBox.Focus();
        if(numCheckedBox is 0) { InsertContactToDb(); return; }

        if(_points is null || _points.Count == 0) {
            InteractionUtilities.ShowErrorMessageBox($"没有可用的数据!");
            return;
        }

        UnitaryValue x;
        if((bool)Surf1RaCheckbox.IsChecked!) { x = _contact.Surface1.RA; }
        else if((bool)Surf1RzCheckBox.IsChecked!) { x = _contact.Surface1.RZ; }
        else if((bool)Surf1RsmCheckBox.IsChecked!) { x = _contact.Surface1.RSM; }
        else if((bool)Surf1RmrCheckBox.IsChecked!) { x = _contact.Surface1.RMR; }
        else if((bool)Surf2RaCheckBox.IsChecked!) { x = _contact.Surface2.RA; }
        else if((bool)Surf2RzCheckBox.IsChecked!) { x = _contact.Surface2.RZ; }
        else if((bool)Surf2RsmCheckBox.IsChecked!) { x = _contact.Surface2.RSM; }
        else if((bool)Surf2RmrCheckBox.IsChecked!) { x = _contact.Surface2.RMR; }
        else if((bool)ContactPressCheckBox.IsChecked!) { x = _contact.ContactPressure; }
        else if((bool)AmbientPressCheckBox.IsChecked!) { x = _contact.AmbientPressure; }
        else if((bool)AmbientTemperatureCheckBox.IsChecked!) { x = _contact.AmbientTemperature; }
        else { x = _contact.HeatFlux; }

        bool isResistance = ResistanceRadioButton.IsChecked.HasValue && ResistanceRadioButton.IsChecked.Value;

        foreach(Point point in _points) {
            x.Value = point.X;
            _contact.ContactResistance.Value = isResistance ? point.Y : 1 / point.Y;
            _dbHelper.InsertResistance(_contact);
        }
        InteractionUtilities.ShowAndHideTooltip("所有数据插入成功");

    }

    private void ResistanceConductanceSwitchRadioButton_OnChecked(object sender, RoutedEventArgs e) {
        InteractionUtilities.RadioButtonSwitch(sender);
    }
}
