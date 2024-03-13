using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace TCRHelper_desktop;
/// <summary>
/// TabularDataCollectionWindow.xaml 的交互逻辑
/// </summary>
public partial class TabularDataCollectionWindow : Window {
    private readonly List<System.Windows.Controls.Control> LOCKED_ELEMENTS;
    public TabularDataCollectionWindow() {
        InitializeComponent();
        LOCKED_ELEMENTS = [RowNumSlider, ColNumSlider, RowNumTextBox, ColNumTextBox];
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
}
