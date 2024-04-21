// 
// TCRHelper
// Model
// 2024-4-12-20:16
// *Author*    : Joe, Zhou Man
// *Email*     : man.man.man.man.a@gmail.com
// *Email*     : joe_zhouman@foxmail.com
// *QQ*        : 1592020915
// *Weibo*     : @zhouman_LFC
// *Twitter*   : @zhouman_LFC
// *Website*   : www.joezhouman.com
// *Github*    : https://github.com/Joe-zhouman
// *Bilibili*  : @satisfactions

using Model.ViewModel.Unit;
using System.Collections.ObjectModel;

namespace Model.ViewModel.Config;
public enum OcrType {
    BAIDU_STD,
}

public class OcrConfigComboBoxViewModel {
    private ObservableCollection<DisplayValuePair<OcrType>>? _ocrProvider;
    public ObservableCollection<DisplayValuePair<OcrType>> OcrProvider =>
        _ocrProvider ??= [
            new DisplayValuePair<OcrType>("百度标准版", OcrType.BAIDU_STD),
        ];
}

public class OcrConfig {
    public ViewModelProperty<OcrType> OcrType { get; set; } = new() { Value = Config.OcrType.BAIDU_STD };
    public ViewModelProperty<string> ApiKey { get; set; } = new();
    public ViewModelProperty<string> SecretKey { get; set; } = new();
}