using Model.ViewModel.Config;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Utilities.Ocr;

namespace Utilities;

public static class InteractionUtilities {
    /// <summary>
    /// 显示提示信息, 信息悬停1s后消失
    /// </summary>
    /// <param name="content">提示条内容</param>
    /// <param name="hangOnSecond">提示条悬停时间</param>
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
    /// 将TextBox的内容转化为数字
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
            InteractionUtilities.ShowAndHideTooltip("OCR API连接成功!");
        }
        catch(Exception e) {
            InteractionUtilities.ShowErrorMessageBox($"获取OCR秘钥失败, 请检查OCR相关设置\n具体错误如下:\n{e}");
        }
        return ocr;
    }
    public static void SetFocusableForChildren(DependencyObject parent, bool focusable) {
        // 遍历parent内的所有子元素，设置Focusable属性
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

