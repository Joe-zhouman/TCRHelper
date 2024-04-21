// 
// TCRHelper
// Model
// 2024-4-21-15:10
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
using SI = Model.ViewModel.Unit.SIUnitsConvertFactor;
namespace Model.ViewModel.Db;

public class SurfaceViewModel {
    public SurfaceViewModel() {
        RA.Unit.Value = SI.MICRO_METER;
        RSM.Unit.Value = SI.MICRO_METER;
        RRM.Unit.Value = SI.MICRO_METER;
    }
    public ViewModelProperty<string> Name { get; set; } = new();
    public UnitaryValue RA { get; set; } = new();
    public UnitaryValue RSM { get; set; } = new();
    public UnitaryValue RRM { get; set; } = new();
}