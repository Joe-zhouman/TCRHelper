// 
// TCRHelper
// Model
// 2024-4-14-20:53
// *Author*    : Joe, Zhou Man
// *Email*     : man.man.man.man.a@gmail.com
// *Email*     : joe_zhouman@foxmail.com
// *QQ*        : 1592020915
// *Weibo*     : @zhouman_LFC
// *Twitter*   : @zhouman_LFC
// *Website*   : www.joezhouman.com
// *Github*    : https://github.com/Joe-zhouman
// *Bilibili*  : @satisfactions

using System.Collections.ObjectModel;
using SI = Model.ViewModel.Unit.SIUnitsConvertFactor;
namespace Model.ViewModel.Unit;

public class UnitComboBoxViewModel {
    /// <summary>
    /// 摩尔质量单位
    /// </summary>
    private ObservableCollection<DisplayValuePair>? _molMassUnit;
    public ObservableCollection<DisplayValuePair> MolMassUnit =>
        _molMassUnit ??= [
            new DisplayValuePair("kg/mol", SI.KILO_GRAM/SI.MOL),
            new DisplayValuePair("g/mol", SI.GRAM/SI.MOL)
        ];
    /// <summary>
    /// 密度单位
    /// </summary>
    private ObservableCollection<DisplayValuePair>? _densityUnit;
    public ObservableCollection<DisplayValuePair> DensityUnit =>
        _densityUnit ??= [
            new DisplayValuePair("kg/m\u00b3", SI.KILO_GRAM/SI.CUBIC_METER),
            new DisplayValuePair("g/cm\u00b3", SI.GRAM/SI.CUBIC_CENTI_METER),
            new DisplayValuePair("g/ml",SI.GRAM/SI.MILLI_LITRE),
            new DisplayValuePair("g/L",SI.GRAM/SI.MILLI_LITRE)
        ];
    /// <summary>
    /// 比热单位
    /// </summary>
    private ObservableCollection<DisplayValuePair>? _specificHeat;

    public ObservableCollection<DisplayValuePair> SpecificHeat =>
        _specificHeat ??= [
            new DisplayValuePair("J/kgK", SI.JOULE / SI.KILO_GRAM / SI.KELVIN),
            new DisplayValuePair("kJ/kgK", SI.KILO_JOULE / SI.KILO_GRAM / SI.KELVIN)
        ];
    /// <summary>
    /// 热导率单位
    /// </summary>
    private ObservableCollection<DisplayValuePair>? _thermalConductivity;

    public ObservableCollection<DisplayValuePair> ThermalConductivity =>
        _thermalConductivity ??= [
            new DisplayValuePair("W/mK", SI.WATT / SI.METER / SI.KELVIN),
        ];
    /// <summary>
    /// 热膨胀系数单位
    /// </summary>
    private ObservableCollection<DisplayValuePair>? _thermalExpansion;

    public ObservableCollection<DisplayValuePair> ThermalExpansion =>
        _thermalExpansion ??= [
            new DisplayValuePair("μm/mK", SI.MICRO / SI.KELVIN),
            new DisplayValuePair("1/K", 1 / SI.KELVIN)
        ];
    private ObservableCollection<DisplayValuePair>? _pressure;

    public ObservableCollection<DisplayValuePair> Pressure =>
        _pressure ??= [
            new DisplayValuePair("Pa", SI.PASCAL),
            new DisplayValuePair("kPa", SI.KILO_PASCAL),
            new DisplayValuePair("MPa", SI.MEGA_PASCAL),
            new DisplayValuePair("GPa",SI.GIGA_PASCAL)
        ];
    private ObservableCollection<DisplayValuePair>? _length;
    public ObservableCollection<DisplayValuePair> Length =>
        _length ??= [
            new DisplayValuePair("m", SI.METER),
            new DisplayValuePair("cm", SI.CENTI_METER),
            new DisplayValuePair("mm",SI.MILLI_METER),
            new DisplayValuePair("μm",SI.MICRO_METER)
        ];
    private ObservableCollection<DisplayValuePair>? _heatFlux;
    public ObservableCollection<DisplayValuePair> HeatFlux =>
        _heatFlux ??= [
            new DisplayValuePair("W/m\u00b2", SI.WATT/SI.SQUARE_METER),
            new DisplayValuePair("W/cm\u00b2", SI.WATT/SI.SQUARE_CENTI_METER),
            new DisplayValuePair("W/mm²",SI.WATT/SI.SQUARE_MILLI_METER)
        ];

    private ObservableCollection<DisplayValuePair>? _temperature;

    public ObservableCollection<DisplayValuePair> Temperature =>
        _temperature ??= [
            new DisplayValuePair("K", SI.KELVIN),
            new DisplayValuePair("℃", SI.CELSIUS_DEGREE, SI.CELSTUS_DEGREE_ADDITION)
        ];
}
