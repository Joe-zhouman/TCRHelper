// 
// MlHelper
// Model
// 2025-2-16-13:26
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
using SI = Model.ViewModel.Unit.SIUnitsConvertFactor;
namespace Model.ViewModel.Db;
public static class VcTable {
    public const string TABLE_NAME = "vc";
    public const string ID = "id";
    public const string SIZE1 = "size1";
    public const string SIZE2 = "size2";
    public const string SIZE3 = "size3";
    public const string EVAPORATION_LEN = "evap_len";
    public const string CONDENSATION_LEN = "cond_len";
    public const string SHELL_THICK = "shell_thick";
    public const string ANGLE = "angle";
    public const string POWER = "power";
    public const string FILL_RATIO = "fill_ratio";
    public const string FLUID_ID = "fluid_id";
    public const string SHELL_ID = "shell_id";
    public const string WICK_ID = "wick_id";
    public const string WICK_NUM_MESH = "wick_num_mesh";
    public const string WICK_NUM_LAYER = "wick_num_layer";
    public const string WICK_THICK = "wick_thick";
    public const string RESISTANCE = "resistance";
    public const string TEMP_DIFF = "temp_diff";
    public const string MAX_HEAT = "max_heat";
    public const string DESCRIPTION = "description";
    public const string REF_ID = "ref_id";

}
public class VcViewModel : ViewModelBase {
    public VcViewModel() {
        Size1.Unit = SI.MILLI_METER;
        Size2.Unit = SI.MILLI_METER;
        Size3.Unit = SI.MILLI_METER;
        EvaporationLen.Unit = SI.MILLI_METER;
        CondensationLen.Unit = SI.MILLI_METER;
        ShellThick.Unit = SI.MILLI_METER;
        Angle.Unit = SI.DEGREE;
        Power.Unit = SI.WATT;
        WickThick.Unit = SI.MILLI_METER;
        Resistance.Unit = SI.KELVIN / SI.WATT;
        TempDiff.Unit = SI.KELVIN;
        MaxHeat.Unit = SI.WATT / SI.SQUARE_METER;
    }
    public ViewModelProperty<int> Id { get; set; } = new();
    public UnitaryValue Size1 { get; set; } = new();
    public UnitaryValue Size2 { get; set; } = new();
    public UnitaryValue Size3 { get; set; } = new();
    public UnitaryValue EvaporationLen { get; set; } = new();
    public UnitaryValue CondensationLen { get; set; } = new();
    public UnitaryValue ShellThick { get; set; } = new();
    public UnitaryValue Angle { get; set; } = new();
    public UnitaryValue Power { get; set; } = new();
    public UnitaryValue WickThick { get; set; } = new();
    public UnitaryValue Resistance { get; set; } = new();
    public UnitaryValue TempDiff { get; set; } = new();
    public UnitaryValue MaxHeat { get; set; } = new();
    public ViewModelProperty<double> FillRatio { get; set; } = new();
    public ViewModelProperty<int> WickNumMesh { get; set; } = new();
    public ViewModelProperty<int> WickNumLayer { get; set; } = new();
    public ViewModelProperty<string> WickId { get; set; } = new();
    public ViewModelProperty<string> FluidId { get; set; } = new();
    public ViewModelProperty<string> ShellId { get; set; } = new();
    public ViewModelProperty<string> Description { get; set; } = new();
    public ViewModelProperty<int> RefId { get; set; } = new();
}