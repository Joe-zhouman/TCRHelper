// 
// MlHelper
// Model
// 2025-2-16-12:5
// * Author *    : Joe, Zhou Man
// * Email *     : man.man.man.man.a@gmail.com
// * Email *     : joe_zhouman@foxmail.com
// * QQ *        : 2483578037
// * Weibo *     : @$T-QAQ-P
// * Twitter *   : @zhouman_LFC
// * Website *   : www.joezhouman.com
// * Github *    : https://github.com/Joe-zhouman
// * Bilibili *  : @satisfactions
// ************************************************
// *          YOU'LL NEVER WALK ALONE             *
// ************************************************

namespace Model.ViewModel.Db;
using SI = Model.ViewModel.Unit.SIUnitsConvertFactor;
public static class SolidTable {
    public const string TABLE_NAME = "solid_mat";
    public const string ID = "id";
    public const string NAME = "name";
    public const string DENSITY = "density";
    public const string THERMAL_CONDUCTIVITY = "thermal_conductivity";
    public const string SPECIFIC_HEAT = "specific_heat";
    public const string DESCRIPTION = "description";
    public const string REF_ID = "ref_id";
}

public class SolidViewModel : ViewModelBase {
    public SolidViewModel() {
        Density.Unit = SI.KILO_GRAM / SI.CUBIC_METER;
        ThermalConductivity.Unit = SI.WATT / SI.METER / SI.KELVIN;
        SpecificHeat.Unit = SI.JOULE / SI.KILO_GRAM / SI.KELVIN;
    }
    public ViewModelProperty<int> Id { get; set; } = new();
    public ViewModelProperty<string> Name { get; set; } = new();
    public UnitaryValue Density { get; set; } = new();
    public UnitaryValue ThermalConductivity { get; set; } = new();
    public UnitaryValue SpecificHeat { get; set; } = new();
    public ViewModelProperty<string> Description { get; set; } = new();
    public ViewModelProperty<int> RefId { get; set; } = new();
}