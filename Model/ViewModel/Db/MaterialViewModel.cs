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

namespace Model.ViewModel.Db;

public class MaterialViewModel : ViewModelBase {
    public int Id { get; set; } = -1;
    private string _name;

    public string Name {
        get => _name;
        set => SetField(ref _name, value);
    }

    public double MolMass { get; set; }
    private int _molMassRefId = -1;

    public int MolMassRefId {
        get => _molMassRefId;
        set => SetField(ref _molMassRefId, value);
    }

    public double Density { get; set; }
    private int _densityRefId = -1;

    public int DensityRefId {
        get => _densityRefId;
        set => SetField(ref _densityRefId, value);
    }

    public double SpecificHeat { get; set; }
    private int _specificHeatRefId = -1;

    public int SpecificHeatRefId {
        get => _specificHeatRefId;
        set => SetField(ref _specificHeatRefId, value);
    }

    public double ThermalConductivity { get; set; }
    private int _thermalConductivityRefId = -1;

    public int ThermalConductivityRefId {
        get => _thermalConductivityRefId;
        set => SetField(ref _thermalConductivityRefId, value);
    }

    public double ThermalExpansion { get; set; }
    private int _thermalExpansionRefId = -1;

    public int ThermalExpansionRefId {
        get => _thermalExpansionRefId;
        set => SetField(ref _thermalExpansionRefId, value);
    }

    public double YoungModulus { get; set; }
    private int _youngModulusRefId = -1;

    public int YoungModulusRefId {
        get => _youngModulusRefId;
        set => SetField(ref _youngModulusRefId, value);
    }

    public double ShearModulus { get; set; }

    private int _shearModulusRefId = -1;

    public int ShearModulusRefId {
        get => _shearModulusRefId;
        set => SetField(ref _shearModulusRefId, value);
    }

    public double BulkModulus { get; set; }

    private int _bulkModulusRefId = -1;

    public int BulkModulusRefId {
        get => _bulkModulusRefId;
        set => SetField(ref _bulkModulusRefId, value);
    }

    public double PoissonRatio { get; set; }

    private int _poissonRatioRefId = -1;

    public int PoissonRatioRefId {
        get => _poissonRatioRefId;
        set => SetField(ref _poissonRatioRefId, value);
    }

    public double MothsHardness { get; set; }

    private int _mothsHardnessRefId = -1;

    public int MothsHardnessRefId {
        get => _mothsHardnessRefId;
        set => SetField(ref _mothsHardnessRefId, value);
    }

    public double VickersHardness { get; set; }

    private int _vickersHardnessRefId = -1;

    public int VickersHardnessRefId {
        get => _vickersHardnessRefId;
        set => SetField(ref _vickersHardnessRefId, value);
    }

    public double BrinellHardness { get; set; }

    private int _brinellHardnessRefId = -1;

    public int BrinellHardnessRefId {
        get => _brinellHardnessRefId;
        set => SetField(ref _brinellHardnessRefId, value);
    }

    private string _description;

    public string Description {
        get => _description;
        set => SetField(ref _description, value);
    }
}