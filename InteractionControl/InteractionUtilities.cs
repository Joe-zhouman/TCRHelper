using Model.ViewModel.Config;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Utilities.Ocr;

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
    public static bool TexBoxValueConvert<T>(TextBox textBox, ref T result) {
        try {
            result = (T)Convert.ChangeType(textBox.Text.Replace(" ", ""), typeof(T));
            return true;
        }
        catch {
            return false;
        }
    }

    public static void ShowErrorMessageBox(string errorMessage) {
        MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
    public static async Task<IOcrProduct> CreateOcr(OcrConfig ocrConfig) {
        var ocr = OcrFactory.Create(ocrConfig);
        try {
            string s = await ocr.GetToken();
            if(s != "") { throw new Exception(s); }
            InteractionUtilities.ShowAndHideTooltip("OCR API���ӳɹ�!");
        }
        catch(Exception e) {
            InteractionUtilities.ShowErrorMessageBox($"��ȡOCR��Կʧ��, ����OCR�������\n�����������:\n{e}");
        }
        return ocr;
    }
    public static void SetFocusableForChildren(DependencyObject parent, bool focusable) {
        // ����parent�ڵ�������Ԫ�أ�����Focusable����
        for(int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++) {
            DependencyObject child = VisualTreeHelper.GetChild(parent, i);
            if(child is GroupBox or Panel or ContentControl) {
                SetFocusableForChildren(child, focusable);
            }
            if(child is Control control) {
                control.Focusable = focusable;
            }
        }
    }
}

