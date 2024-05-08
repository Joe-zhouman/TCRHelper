using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Utilities;

namespace TCRHelper.Desktop.Win.Tab.Import;
//TODO: log coordinate
//TODO: data transformation
/// <summary>
/// PlotDataCollectionWindows.xaml 的交互逻辑
/// </summary>
public partial class PlotDataCollectionWindows : Window {
    private double _xMin;
    private double _xMax;
    private double _yMin;
    private double _yMax;
    private List<Point> _points;
    private ImportTabPage _parent;
    public PlotDataCollectionWindows(ImportTabPage parent) {
        InitializeComponent();
        _points = [];
        _parent = parent;
    }


    private void ManualSelectedButton_OnClick(object sender, RoutedEventArgs e) {
        _points.Clear();
        Cursor = Cursors.Arrow;
        Plot.ConvertToCoordinate(_xMin, _xMax, _yMin, _yMax, ref _points);
        PlotDataGrid.ItemsSource = _points;
    }

    private void SendDataButton_OnClick(object sender, RoutedEventArgs e) {
        _parent.ShowPoints(_points);
    }

    private void Plot_OnMouseEnter(object sender, MouseEventArgs e) {
        Plot.Focus();
        //InteractionUtilities.SetFocusableForChildren(FunctionStackPanel, false);
    }

    private void Plot_OnKeyDown(object sender, KeyEventArgs e) {
        Plot.Focus();
    }

    private void ConfirmBoundaryCheckbox_OnChecked(object sender, RoutedEventArgs e) {
        if(!InteractionUtilities.TexBoxValueConvert(XMinTextBox, ref _xMin)
            || !InteractionUtilities.TexBoxValueConvert(XMaxTextBox, ref _xMax)
            || !InteractionUtilities.TexBoxValueConvert(YMinTextBox, ref _yMin)
            || !InteractionUtilities.TexBoxValueConvert(YMaxTextBox, ref _yMax)) {
            InteractionUtilities.ShowAndHideTooltip("输入的不是一个正确的数字, 请检查输入!");
            ConfirmBoundaryCheckbox.IsChecked = false;
            return;
        }
        if(_xMin > _xMax || _yMin > _yMax) {
            InteractionUtilities.ShowAndHideTooltip("坐标下限大于上限,请检查输入!");
            ConfirmBoundaryCheckbox.IsChecked = false;
            return;
        }
        InteractionUtilities.ShowAndHideTooltip("边界范围确定完毕, 现在可以点击图片上的位置取点了!");
        Cursor = Cursors.Cross;
        Plot.lineMovable = false;
        Plot.pointAddable = true;
    }

    private void ConfirmBoundaryCheckbox_OnUnchecked(object sender, RoutedEventArgs e) {
        Cursor = Cursors.Arrow;
        Plot.lineMovable = true;
        Plot.pointAddable = false;
    }

    private void AutoSelectedButton_OnClick(object sender, RoutedEventArgs e) {
        InteractionUtilities.ShowAndHideTooltip("该功能暂未实现, 请在图中手动拾取数据点!");
    }

    private void ResetButton_OnClick(object sender, RoutedEventArgs e) {
        XMinTextBox.Text = "0.0";
        XMaxTextBox.Text = "0.0";
        YMinTextBox.Text = "0.0";
        YMaxTextBox.Text = "0.0";
        ConfirmBoundaryCheckbox.IsChecked = false;
        Plot.ClearPoints();
    }

    private void ReGetPointButton_OnClick(object sender, RoutedEventArgs e) => Plot.ClearPoints();

    private void SortMethodRadioButton_OnChecked(object sender, RoutedEventArgs e) {
        InteractionUtilities.RadioButtonSwitch(sender);
    }



    private void ShowPlotLineCheckBox_OnChecked(object sender, RoutedEventArgs e) {
        int sortMethod = 0;
        if((bool)SortByXRadioButton.IsChecked!) { sortMethod = 1; }
        else if((bool)SortByYRadioButton.IsChecked!) { sortMethod = 2; }
        Plot.ConnectPoints(sortMethod);
    }

    private void ShowPlotLineCheckBox_OnUnchecked(object sender, RoutedEventArgs e) {
        Plot.ClearLines();
    }


    private void PlotGrid_OnMouseEnter(object sender, MouseEventArgs e) { Plot.Focus(); }
    private void PlotGrid_OnKeyDown(object sender, KeyEventArgs e) { Plot.Focus(); }

    private void UnFocusableTextBox_OnMouseEnter(object sender, MouseEventArgs e) {
        if(sender is TextBox textBox) { textBox.Focusable = true; }
    }
}
