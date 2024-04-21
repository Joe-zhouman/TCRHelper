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