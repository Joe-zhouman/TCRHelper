// 
// TCRHelper
// Model
// 2024-4-15-19:22
// *Author*    : Joe, Zhou Man
// *Email*     : man.man.man.man.a@gmail.com
// *Email*     : joe_zhouman@foxmail.com
// *QQ*        : 1592020915
// *Weibo*     : @zhouman_LFC
// *Twitter*   : @zhouman_LFC
// *Website*   : www.joezhouman.com
// *Github*    : https://github.com/Joe-zhouman
// *Bilibili*  : @satisfactions

using SI = Model.ViewModel.Unit.SIUnitsConvertFactor;

namespace Model.ViewModel.Db;

public static class MaterialTable {
    public const string TABLE_NAME = "material";
    public const string MAT_ID = "mat_id";
    public const string MAT_NAME = "mat_name";
    public const string MOLAR_MASS = "molar_mass";
    public const string DENSITY = "density";
    public const string DENSITY_REF = "density_ref";
    public const string SPECIFIC_HEAT = "specific_heat";
    public const string SPECIFIC_HEAT_REF = "specific_heat_ref";
    public const string THERMAL_CONDUCTIVITY = "thermal_conductivity";
    public const string THERMAL_CONDUCTIVITY_REF = "thermal_conductivity_ref";
    public const string THERMAL_EXPANSION = "thermal_expansion";
    public const string THERMAL_EXPANSION_REF = "thermal_expansion_ref";
    public const string YOUNG_MODULUS = "youngs_modulus";
    public const string YOUNG_MODULUS_REF = "youngs_modulus_ref";
    public const string SHEAR_MODULUS = "shear_modulus";
    public const string SHEAR_MODULUS_REF = "shear_modulus_ref";
    public const string BULK_MODULUS = "bulk_modulus";
    public const string BULK_MODULUS_REF = "bulk_modulus_ref";
    public const string POISSON_RATIO = "poisson_ratio";
    public const string POISSON_RATIO_REF = "poisson_ratio_ref";
    public const string MOTHS_HARDNESS = "moths_hardness";
    public const string MOTHS_HARDNESS_REF = "moths_hardness_ref";
    public const string VICKERS_HARDNESS = "vickers_hardness";
    public const string VICKERS_HARDNSS_REF = "vickers_hardness_ref";
    public const string BRINELL_HARDNESS = "brinell_hardness";
    public const string BRINELL_HARDNESS_REF = "brinell_hardness_ref";
    public const string DESCRIPTION = "description";
}

public class MaterialViewModel : ViewModelBase {
    public MaterialViewModel() {
        MolMass.Property.Unit.Value = SI.KILO_GRAM / SI.MOL;
        Density.Property.Unit.Value = SI.KILO_GRAM / SI.CUBIC_METER;
        SpecificHeat.Property.Unit.Value = SI.JOULE / SI.KILO_GRAM / SI.CELSIUS_DEGREE;
        ThermalConductivity.Property.Unit.Value = SI.WATT / SI.METER / SI.KELVIN;
        ThermalExpansion.Property.Unit.Value = SI.MICRO / SI.KELVIN;
        YoungModulus.Property.Unit.Value = SI.GIGA_PASCAL;
        ShearModulus.Property.Unit.Value = SI.GIGA_PASCAL;
        BulkModulus.Property.Unit.Value = SI.GIGA_PASCAL;
        VickersHardness.Property.Unit.Value = SI.MEGA_PASCAL;
        BrinellHardness.Property.Unit.Value = SI.MEGA_PASCAL;
    }

    public ViewModelProperty<int> Id { get; set; } = new();

    public ViewModelProperty<string> Name { get; set; } = new();

    public RefProperty MolMass { get; set; } = new();

    public RefProperty Density { get; set; } = new();

    public RefProperty SpecificHeat { get; set; } = new();

    public RefProperty ThermalConductivity { get; set; } = new();

    public RefProperty ThermalExpansion { get; set; } = new();

    public RefProperty YoungModulus { get; set; } = new();

    public RefProperty ShearModulus { get; set; } = new();

    public RefProperty BulkModulus { get; set; } = new();

    public RefProperty PoissonRatio { get; set; } = new();

    public RefProperty MothsHardness { get; set; } = new();

    public RefProperty VickersHardness { get; set; } = new();

    public RefProperty BrinellHardness { get; set; } = new();

    public ViewModelProperty<string> Description { get; set; } = new();
}