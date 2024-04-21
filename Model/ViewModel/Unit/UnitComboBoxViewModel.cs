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
    private ObservableCollection<DisplayValuePair<double>>? _molMassUnit;
    public ObservableCollection<DisplayValuePair<double>> MolMassUnit =>
        _molMassUnit ??= [
            new DisplayValuePair<double>("kg/mol", SI.KILO_GRAM/SI.MOL),
            new DisplayValuePair<double>("g/mol", SI.GRAM/SI.MOL)
        ];
    /// <summary>
    /// 密度单位
    /// </summary>
    private ObservableCollection<DisplayValuePair<double>>? _densityUnit;
    public ObservableCollection<DisplayValuePair<double>> DensityUnit =>
        _densityUnit ??= [
            new DisplayValuePair<double>("kg/m\u00b3", SI.KILO_GRAM/SI.CUBIC_METER),
            new DisplayValuePair<double>("g/cm\u00b3", SI.GRAM/SI.CUBIC_CENTI_METER),
            new DisplayValuePair<double>("g/ml",SI.GRAM/SI.MILLI_LITRE),
            new DisplayValuePair<double>("g/L",SI.GRAM/SI.MILLI_LITRE)
        ];
    /// <summary>
    /// 比热单位
    /// </summary>
    private ObservableCollection<DisplayValuePair<double>>? _specificHeat;

    public ObservableCollection<DisplayValuePair<double>> SpecificHeat =>
        _specificHeat ??= [
            new DisplayValuePair<double>("J/kgK", SI.JOULE / SI.KILO_GRAM / SI.KELVIN),
            new DisplayValuePair<double>("kJ/kgK", SI.KILO_JOULE / SI.KILO_GRAM / SI.KELVIN)
        ];
    /// <summary>
    /// 热导率单位
    /// </summary>
    private ObservableCollection<DisplayValuePair<double>>? _thermalConductivity;

    public ObservableCollection<DisplayValuePair<double>> ThermalConductivity =>
        _thermalConductivity ??= [
            new DisplayValuePair<double>("W/mK", SI.WATT / SI.METER / SI.KELVIN),
        ];
    /// <summary>
    /// 热膨胀系数单位
    /// </summary>
    private ObservableCollection<DisplayValuePair<double>>? _thermalExpansion;

    public ObservableCollection<DisplayValuePair<double>> ThermalExpansion =>
        _thermalExpansion ??= [
            new DisplayValuePair<double>("μm/mK", SI.MICRO / SI.KELVIN),
            new DisplayValuePair<double>("1/K", 1 / SI.KELVIN)
        ];
    private ObservableCollection<DisplayValuePair<double>>? _pressure;

    public ObservableCollection<DisplayValuePair<double>> Pressure =>
        _pressure ??= [
            new DisplayValuePair<double>("Pa", SI.PASCAL),
            new DisplayValuePair<double>("kPa", SI.KILO_PASCAL),
            new DisplayValuePair<double>("MPa", SI.MEGA_PASCAL),
            new DisplayValuePair<double>("GPa",SI.GIGA_PASCAL)
        ];
    private ObservableCollection<DisplayValuePair<double>>? _length;
    public ObservableCollection<DisplayValuePair<double>> Length =>
        _length ??= [
            new DisplayValuePair<double>("m", SI.METER),
            new DisplayValuePair<double>("cm", SI.CENTI_METER),
            new DisplayValuePair<double>("mm",SI.MILLI_METER),
            new DisplayValuePair<double>("μm",SI.MICRO_METER)
        ];
    private ObservableCollection<DisplayValuePair<double>>? _heatFlux;
    public ObservableCollection<DisplayValuePair<double>> HeatFlux =>
        _heatFlux ??= [
            new DisplayValuePair<double>("W/m\u00b2", SI.WATT/SI.SQUARE_METER),
            new DisplayValuePair<double>("W/cm\u00b2", SI.WATT/SI.SQUARE_CENTI_METER),
            new DisplayValuePair<double>("W/mm²",SI.WATT/SI.SQUARE_MILLI_METER)
        ];

    private ObservableCollection<DisplayValuePair<double>>? _temperature;

    public ObservableCollection<DisplayValuePair<double>> Temperature =>
        _temperature ??= [
            //new DisplayValuePair("K", SI.KELVIN),
            new DisplayValuePair<double>("℃", SI.CELSIUS_DEGREE)
        ];

    private ObservableCollection<DisplayValuePair<double>>? _contactResistance;

    public ObservableCollection<DisplayValuePair<double>> ContactResistance =>
        _contactResistance ??= [
            new DisplayValuePair<double>("mm²K/W", SI.CELSIUS_DEGREE / SI.WATT),
            new DisplayValuePair<double>("cm²K/W", SI.CELSIUS_DEGREE / SI.WATT * SI.SQUARE_CENTI_METER / SI.SQUARE_MILLI_METER),
            new DisplayValuePair<double>("m²K/W", SI.SQUARE_METER * SI.CELSIUS_DEGREE / SI.WATT / SI.SQUARE_MILLI_METER)
        ];
}
