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

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.Db;

public class Material : INotifyPropertyChanged {
    public int Id { get; set; }
    public string Name { get; set; }
    public double MolMass { get; set; }
    public int MolMassRefId { get; set; }
    public double Density { get; set; }
    public int DensityRefId { get; set; }
    public double SpecificHeat { get; set; }
    public int SpecificHeatRefId { get; set; }
    public double ThermalConductivity { get; set; }
    public int ThermalConductivityRefId { get; set; }
    public double ThermalExpansion { get; set; }
    public int ThermalExpansionRefId { get; set; }
    public double YoungModulus { get; set; }
    public int YoungModulusRefId { get; set; }
    public double ShearModulus { get; set; }
    public int ShearModulusRefId { get; set; }
    public double BulkModulus { get; set; }
    public int BulkModulusRefId { get; set; }
    public double PoissonRatio { get; set; }
    public int PoissonRatioRefId { get; set; }
    public double MothsHardness { get; set; }
    public int MothsHardnessRefId { get; set; }
    public double VickersHardness { get; set; }
    public int VickersHardnessId { get; set; }
    public double BrinellHardness { get; set; }
    public int BrinellHardnessRefId { get; set; }
    public string? Description { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if(EqualityComparer<T>.Default.Equals(field, value))
            return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}