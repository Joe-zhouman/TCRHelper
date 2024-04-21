// 
// TCRHelper
// Model
// 2024-4-21-15:55
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

public class ContactViewModel {
    public ContactViewModel() {
        ContactPressure.Unit.Value = SI.MEGA_PASCAL;
        AmbientPressure.Unit.Value = SI.PASCAL;
        AmbientTemperature.Unit.Value = SI.CELSIUS_DEGREE;
        HeatFlux.Unit.Value = SI.WATT / SI.SQUARE_METER;
        ContactResistance.Unit.Value = SI.CELSIUS_DEGREE / SI.WATT;
    }
    public SurfaceViewModel Surface1 { get; set; } = new();
    public SurfaceViewModel Surface2 { get; set; } = new();
    public UnitaryValue ContactPressure { get; set; } = new();
    public UnitaryValue AmbientPressure { get; set; } = new();
    public UnitaryValue AmbientTemperature { get; set; } = new();
    public UnitaryValue HeatFlux { get; set; } = new();
    public ViewModelProperty<int> RefId { get; set; } = new() { Value = -1 };
    public ViewModelProperty<string> Description { get; set; } = new();
    public UnitaryValue ContactResistance { get; set; } = new();
}