
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Utilities;

public static class InteractionUtilities {
    /// <summary>
    /// ��ʾ��ʾ��Ϣ, ��Ϣ��ͣ1s����ʧ
    /// </summary>
    /// <param name="content">��ʾ������</param>
    /// <param name="hangOnSecond">��ʾ����ͣʱ��</param>
    public static void ShowAndHideTooltip(string content, int hangOnSecond = 1) {
        ToolTip toolTip = new() {
            Content = content,
            IsOpen = true
        };
        DispatcherTimer timer = new() {
            Interval = TimeSpan.FromSeconds(hangOnSecond)
        };
        timer.Tick += (s, args) => {
            toolTip.IsOpen = false;
            timer.Stop();
        };
        timer.Start();
    }
    /// <summary>
    /// ��TextBox������ת��Ϊ����
    /// </summary>
    /// <param name="textBox"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TexBoxValueConvert(TextBox textBox, ref double result) {
        if(ConversionUtilities.Str2Num(textBox.Text, ref result)) { return true; }
        InteractionUtilities.ShowAndHideTooltip("�������!");
        return false;
    }

    public static void SetFocusableForChildren(DependencyObject parent, bool focusable) {
        // ����parent�ڵ�������Ԫ�أ�����Focusable����
        for(int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++) {
            DependencyObject child = VisualTreeHelper.GetChild(parent, i);
            if(child is GroupBox or Panel or ContentControl) {
                SetFocusableForChildren(child, focusable);
            }
            if(child is Control control) {
                control.IsEnabled = focusable;
            }
        }
    }
}

