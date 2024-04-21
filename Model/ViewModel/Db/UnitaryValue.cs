// 
// TCRHelper
// Model
// 2024-4-21-11:11
// * Author *    : Joe, Zhou Man
// * Email *     : man.man.man.man.a @gmail.com
// * Email *     : joe_zhouman @foxmail.com
// * QQ *        : 1592020915
// * Weibo *     : @zhouman_LFC
// * Twitter *   : @zhouman_LFC
// * Website *   : www.joezhouman.com
// * Github *    : https://github.com/Joe-zhouman
// * Bilibili *  : @satisfactions
// ************************************************
// *          YOU'LL NEVER WALK ALONE             *
// ************************************************

namespace Model.ViewModel.Db;

public class UnitaryValue : ViewModelBase {
    public ViewModelProperty<double> Unit { get; set; } = new() { Value = 1.0 };
    private double _value;
    public double Value {
        get => _value * Unit.Value;
        set => SetField(ref _value, value);
    }
}