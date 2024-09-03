using Model.ViewModel.Config;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Utilities;
using Utilities.Ocr;

namespace TCRHelper.Desktop.Win.Tab.Import;
/// <summary>
/// TabularDataCollectionWindow.xaml 的交互逻辑
/// </summary>
public partial class TabularDataCollectionWindow : Window {
    private readonly List<Control> LOCKED_ELEMENTS;

    public int nRow;
    public int nCol;
    public BitmapSource[,]? images;
    public string[,] identifiedData;
    private IOcrProduct _ocr;
    private AppConfig _appConfig;
    public ImportTabPage parent;
    public TabularDataCollectionWindow(AppConfig appConfig, ImportTabPage parent) {
        InitializeComponent();
        UniformDistributedRadioButton.IsChecked = true;
        LOCKED_ELEMENTS = [RowNumSlider, ColNumSlider, RowNumTextBox, ColNumTextBox];
        _appConfig = appConfig;
        this.parent = parent;
    }
    private void RowNumSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
        RowNumTextBox.Text = RowNumSlider.Value.ToString(CultureInfo.InvariantCulture);
    }

    private void ColNumSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
        ColNumTextBox.Text = ColNumSlider.Value.ToString(CultureInfo.InvariantCulture);
    }

    private void Tabular_OnMouseEnter(object sender, MouseEventArgs e) {
        foreach(System.Windows.Controls.Control control in LOCKED_ELEMENTS) {
            control.Focusable = false;
        }
    }

    private void Tabular_OnMouseLeave(object sender, MouseEventArgs e) {
        foreach(System.Windows.Controls.Control control in LOCKED_ELEMENTS) {
            control.Focusable = true;
        }
    }

    private void UnFocusableTextBox_OnMouseEnter(object sender, MouseEventArgs e) {
        if(sender is TextBox textBox) { textBox.Focusable = true; }
    }

    private void ConfirmNumRowColCheckBox_OnChecked(object sender, RoutedEventArgs e) {
        if(!InteractionUtilities.TexBoxValueConvert(RowNumTextBox, ref nRow)
           || !InteractionUtilities.TexBoxValueConvert(ColNumTextBox, ref nCol)) {
            InteractionUtilities.ShowAndHideTooltip("输入的不是一个正确的数字, 请检查输入!");
            ConfirmNumRowColCheckBox.IsChecked = false;
            return;
        }
        Tabular.lineMovable = false;
        images = new BitmapSource[nRow, nCol];
    }
    private void ConfirmNumRowColCheckBox_OnUnchecked(object sender, RoutedEventArgs e) {
        Tabular.lineMovable = true;
        images = null;
    }
    private void UniformDistributedRadioButton_OnChecked(object sender, RoutedEventArgs e) {
        Tabular.pointAddable = false;
        Cursor = Cursors.Arrow;
        NonUniformDistributedRadioButton.IsChecked = false;
    }

    private void NonUniformDistributedRadioButton_OnChecked(object sender, RoutedEventArgs e) {
        Tabular.pointAddable = true;
        Cursor = Cursors.Cross;
        UniformDistributedRadioButton.IsChecked = false;
    }

    private void ShowSplitLinesCheckBox_OnChecked(object sender, RoutedEventArgs e) {
        Tabular.ShowSplitLines(
                nRow, nCol,
                (bool)UniformDistributedRadioButton.IsChecked! ? 0 : 1
                );
    }

    private void ShowSplitLinesCheckBox_OnUnchecked(object sender, RoutedEventArgs e) {
        Tabular.ClearLines();
    }

    private void IdentifyContentButton_OnClick(object sender, RoutedEventArgs e) {
        if(images is null) {
            InteractionUtilities.ShowAndHideTooltip("请先确定行列数!", 2);
            return;
        }
        if((bool)(!ConfirmNumRowColCheckBox.IsChecked)!) {
            ConfirmNumRowColCheckBox.IsChecked = true;
        }
        Tabular.SplitImage(nRow, nCol, ref images, 2);
        IdentifyResultWindows w = new(images, _ocr, this);
        w.ShowDialog();
    }


    private async void TabularDataCollectionWindow_OnLoaded(object sender, RoutedEventArgs e) {
        _ocr = await InteractionUtilities.CreateOcr(_appConfig.OcrConfig);
    }

    private void ConfirmSplitCheckBox_OnChecked(object sender, RoutedEventArgs e) {
        if(!Tabular.AddSplitPoints(
                nCol, nRow,
                (bool)UniformDistributedRadioButton.IsChecked! ? 0 : 1)
           ) {
            ConfirmSplitCheckBox.IsChecked = false;
            return;
        }
        Tabular.pointAddable = false;
        Tabular.ClearPoints();
        Cursor = Cursors.Arrow;
    }
}
