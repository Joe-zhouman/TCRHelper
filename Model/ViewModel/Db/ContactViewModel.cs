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

public static class ResistanceTable {
    public const string TABLE_NAME = "resistance";
    public const string ID = "resistance_id";
    public const string RESISTANCE = "resistance";
    public const string CONTACT_MAT_1 = "contact_material_1_id";
    public const string CONTACT_MAT_2 = "contact_material_2_id";
    public const string MAT_1_RA = "mat_1_ra";
    public const string MAT_2_RA = "mat_2_ra";
    public const string MAT_1_RZ = "mat_1_rz";
    public const string MAT_2_RZ = "mat_2_rz";
    public const string MAT_1_RSM = "mat_1_rsm";
    public const string MAT_2_RSM = "mat_2_rsm";
    public const string MAT_1_RMR = "mat_1_rmr";
    public const string MAT_2_RMR = "mat_2_rmr";
    public const string AMBIENT_PRESS = "ambient_pressure";
    public const string AMBIENT_TEMP = "ambient_temperature";
    public const string CONTACT_PRESS = "contact_pressure";
    public const string HEAT_FLUX = "heat_flux";
    public const string REF = "ref_id";
    public const string DESCRIPTION = "description";
}

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