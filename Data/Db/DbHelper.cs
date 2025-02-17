// 
// TCRHelper
// Data
// 2024-4-22-19:47
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

using DbProvider;
using Model.ViewModel.Config;
using Model.ViewModel.Db;
using Model.ViewModel.Unit;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace Data.Db;

public class DbHelper {
    public static IDbClient? DbClient { get; set; }

    private static readonly string _COMMON_REFERENCE_TABLE_COL =
        $"{ReferenceTable.DOI},{ReferenceTable.TITLE},{ReferenceTable.AUTHOR},{ReferenceTable.YEAR},{ReferenceTable.JOURNAL},{ReferenceTable.DESCRIPTION},{ReferenceTable.DETAIL}";

    private static string CommonReferenceTableValue(ReferenceViewModel reference) =>
        $"'{reference.DOI.Value}','{reference.Title.Value}','{reference.Author.Value}','{reference.Year.Value}','{reference.Journal.Value}','{reference.Description.Value}','{reference.Detail}'";

    private static readonly string _COMMON_MATERIAL_TABLE_COL =
        $"{MaterialTable.MAT_NAME},{MaterialTable.MOLAR_MASS},{MaterialTable.DENSITY},{MaterialTable.DENSITY_REF},{MaterialTable.SPECIFIC_HEAT},{MaterialTable.SPECIFIC_HEAT_REF},{MaterialTable.THERMAL_CONDUCTIVITY},{MaterialTable.THERMAL_CONDUCTIVITY_REF},{MaterialTable.THERMAL_EXPANSION},{MaterialTable.THERMAL_EXPANSION_REF},{MaterialTable.YOUNG_MODULUS},{MaterialTable.YOUNG_MODULUS_REF},{MaterialTable.SHEAR_MODULUS},{MaterialTable.SHEAR_MODULUS_REF},{MaterialTable.BULK_MODULUS},{MaterialTable.BULK_MODULUS_REF},{MaterialTable.POISSON_RATIO},{MaterialTable.POISSON_RATIO_REF},{MaterialTable.MOTHS_HARDNESS},{MaterialTable.MOTHS_HARDNESS_REF},{MaterialTable.VICKERS_HARDNESS},{MaterialTable.VICKERS_HARDNESS_REF},{MaterialTable.BRINELL_HARDNESS},{MaterialTable.BRINELL_HARDNESS_REF},{MaterialTable.DESCRIPTION}";

    private static string CommonMaterialTableValue(MaterialViewModel material) =>
        $"'{material.Name.Value.Replace(" ", "").ToLower()}',{material.MolMass.Property.RealValue},{material.Density.Property.RealValue},{material.Density.RefId.Value},{material.SpecificHeat.Property.RealValue},{material.SpecificHeat.RefId.Value},{material.ThermalConductivity.Property.RealValue},{material.ThermalConductivity.RefId.Value},{material.ThermalExpansion.Property.RealValue},{material.ThermalExpansion.RefId.Value},{material.YoungModulus.Property.RealValue},{material.YoungModulus.RefId.Value},{material.ShearModulus.Property.RealValue},{material.ShearModulus.RefId.Value},{material.BulkModulus.Property.RealValue},{material.BulkModulus.RefId.Value},{material.PoissonRatio.Property.RealValue},{material.PoissonRatio.RefId.Value},{material.MothsHardness.Property.RealValue},{material.MothsHardness.RefId.Value},{material.VickersHardness.Property.RealValue},{material.VickersHardness.RefId.Value},{material.BrinellHardness.Property.RealValue},{material.BrinellHardness.RefId.Value},'{material.Description.Value}'";

    private static readonly string _COMMON_SOLID_TABLE_COL = $"{SolidTable.NAME},{SolidTable.DENSITY},{SolidTable.THERMAL_CONDUCTIVITY},{SolidTable.SPECIFIC_HEAT},{SolidTable.DESCRIPTION}";

    private static string CommonSolidTableValue(SolidViewModel material) =>
        $"'{material.Name.Value.Replace(" ", "").ToLower()}',{material.Density.RealValue},{material.ThermalConductivity.RealValue},{material.SpecificHeat.RealValue},'{material.Description.Value}'";
    private static readonly string _COMMON_Fluid_TABLE_COL = $"{FluidTable.NAME},{FluidTable.DENSITY},{FluidTable.THERMAL_CONDUCTIVITY},{FluidTable.SPECIFIC_HEAT},{FluidTable.VISCOSITY},{FluidTable.LATENT_HEAT},{FluidTable.MELT_POINT},{FluidTable.BOIL_POINT},{FluidTable.TENSION_COEFF},{FluidTable.CONTACT_ANGLE},{FluidTable.DESCRIPTION}";

    private static string CommonFluidTableValue(FluidViewModel material) =>
        $"'{material.Name.Value.Replace(" ", "").ToLower()}',{material.Density.RealValue},{material.ThermalConductivity.RealValue},{material.SpecificHeat.RealValue},{material.Viscosity.RealValue},{material.LatentHeat.RealValue},{material.MeltPoint.RealValue},{material.BoilPoint.RealValue},{material.TensionCoeff.RealValue},{material.ContactAngle.RealValue},'{material.Description.Value}'";


    private static readonly string _COMMON_RESISTANCE_TABLE_COL = $"{ResistanceTable.RESISTANCE},{ResistanceTable.CONTACT_MAT_1},{ResistanceTable.CONTACT_MAT_2},{ResistanceTable.MAT_1_RA},{ResistanceTable.MAT_2_RA},{ResistanceTable.MAT_1_RZ},{ResistanceTable.MAT_2_RZ},{ResistanceTable.MAT_1_RSM},{ResistanceTable.MAT_2_RSM},{ResistanceTable.MAT_1_RMR},{ResistanceTable.MAT_2_RMR},{ResistanceTable.AMBIENT_PRESS},{ResistanceTable.AMBIENT_TEMP},{ResistanceTable.CONTACT_PRESS},{ResistanceTable.HEAT_FLUX},{ResistanceTable.REF},{ResistanceTable.DESCRIPTION}";

    private static string CommonResistanceTableValue(ContactViewModel contact) => $"{contact.ContactResistance.RealValue},{contact.Surface1.Name.Value},{contact.Surface2.Name.Value},{contact.Surface1.RA.RealValue},{contact.Surface2.RA.RealValue},{contact.Surface1.RZ.RealValue},{contact.Surface2.RZ.RealValue},{contact.Surface1.RSM.RealValue},{contact.Surface2.RSM.RealValue},{contact.Surface1.RMR.RealValue},{contact.Surface2.RMR.RealValue},{contact.AmbientPressure.RealValue},{contact.AmbientTemperature.RealValue},{contact.ContactPressure.RealValue},{contact.HeatFlux.RealValue},{contact.RefId.Value},'{contact.Description.Value}'";

    public DbHelper(DbConfig config) {
        DbClient = DbClientFactory.Create(config);
    }
    public void TextDbConnect() {
        DbClient?.OpenDb();
        DbClient?.CloseSqlConnection();
    }

    public bool Exist(string tableName, string colName, string value) {
        string query = $"SELECT 1 FROM {tableName} WHERE {colName}='{value}' LIMIT 1";
        var reader = DbClient?.ExecuteQuery(query);
        return reader is not null && reader.HasRows;
    }

    public bool InsertRef(ReferenceViewModel reference) {
        DbClient?.OpenDb();
        if(Exist(ReferenceTable.TABLE_NAME, ReferenceTable.DOI, reference.DOI.Value)) { DbClient?.CloseSqlConnection(); return false; }
        DbClient?.InsertIntoSpecific(ReferenceTable.TABLE_NAME, _COMMON_REFERENCE_TABLE_COL, CommonReferenceTableValue(reference));
        var reader = DbClient?.ExecuteQuery($"SELECT {ReferenceTable.ID} FROM {ReferenceTable.TABLE_NAME} WHERE {ReferenceTable.DOI}='{reference.DOI.Value}'");
        if(reader is null || !reader.HasRows) { throw new DBConcurrencyException(); }
        reader.Read();
        reference.Id.Value = reader.GetInt32(ReferenceTable.ID);
        DbClient?.CloseSqlConnection();
        return true;
    }

    #region InsertMat
    public bool InsertMat(MaterialViewModel material) {
        DbClient?.OpenDb();
        if(Exist(MaterialTable.TABLE_NAME, MaterialTable.MAT_NAME, material.Name.Value)) {
            DbClient?.CloseSqlConnection();
            return false;
        }
        DbClient?.InsertIntoSpecific(MaterialTable.TABLE_NAME, _COMMON_MATERIAL_TABLE_COL, CommonMaterialTableValue(material));
        var reader = DbClient?.ExecuteQuery($"SELECT {MaterialTable.MAT_ID} FROM {MaterialTable.TABLE_NAME} WHERE {MaterialTable.MAT_NAME}='{material.Name.Value}'");
        if(reader is null || !reader.HasRows) { throw new DBConcurrencyException(); }
        reader.Read();
        material.Id.Value = reader.GetInt32(MaterialTable.MAT_ID);
        DbClient?.CloseSqlConnection();
        return true;
    }
    public bool InsertMat(SolidViewModel material) {
        DbClient?.OpenDb();
        if(Exist(SolidTable.TABLE_NAME, SolidTable.NAME, material.Name.Value)) {
            DbClient?.CloseSqlConnection();
            return false;
        }
        DbClient?.InsertIntoSpecific(SolidTable.TABLE_NAME, _COMMON_SOLID_TABLE_COL, CommonSolidTableValue(material));
        var reader = DbClient?.ExecuteQuery($"SELECT {SolidTable.ID} FROM {SolidTable.TABLE_NAME} WHERE {SolidTable.NAME}='{material.Name.Value}'");
        if(reader is null || !reader.HasRows) { throw new DBConcurrencyException(); }
        reader.Read();
        material.Id.Value = reader.GetInt32(SolidTable.ID);
        DbClient?.CloseSqlConnection();
        return true;
    }
    public bool InsertMat(FluidViewModel material) {
        DbClient?.OpenDb();
        if(Exist(FluidTable.TABLE_NAME, FluidTable.NAME, material.Name.Value)) {
            DbClient?.CloseSqlConnection();
            return false;
        }
        DbClient?.InsertIntoSpecific(FluidTable.TABLE_NAME, _COMMON_Fluid_TABLE_COL, CommonFluidTableValue(material));
        var reader = DbClient?.ExecuteQuery($"SELECT {FluidTable.ID} FROM {FluidTable.TABLE_NAME} WHERE {FluidTable.NAME}='{material.Name.Value}'");
        if(reader is null || !reader.HasRows) { throw new DBConcurrencyException(); }
        reader.Read();
        material.Id.Value = reader.GetInt32(FluidTable.ID);
        DbClient?.CloseSqlConnection();
        return true;
    }
    #endregion
    public bool InsertResistance(ContactViewModel contact) {
        DbClient?.OpenDb();
        DbClient?.InsertIntoSpecific(ResistanceTable.TABLE_NAME, _COMMON_RESISTANCE_TABLE_COL,
            CommonResistanceTableValue(contact));
        DbClient?.CloseSqlConnection();
        return true;
    }

    public bool SearchRef(ReferenceViewModel reference) {
        DbClient?.OpenDb();
        var reader = DbClient?.SelectWhere(ReferenceTable.TABLE_NAME, "*",
            $"{ReferenceTable.DOI}='{reference.DOI.Value}'");
        if(reader is null || !reader.HasRows) { DbClient?.CloseSqlConnection(); return false; }

        reader.Read();
        reference.Id.Value = reader.GetInt32(ReferenceTable.ID);
        reference.Author.Value = reader.GetString(ReferenceTable.AUTHOR);
        reference.Description.Value = reader.GetString(ReferenceTable.DESCRIPTION);
        reference.Detail = reader.GetString(ReferenceTable.DETAIL);
        reference.Journal.Value = reader.GetString(ReferenceTable.JOURNAL);
        reference.Title.Value = reader.GetString(ReferenceTable.TITLE);
        reference.Year.Value = reader.GetString(ReferenceTable.YEAR);
        DbClient?.CloseSqlConnection();
        return true;
    }

    #region SearchMat



    public bool SearchMat(MaterialViewModel mat) {
        DbClient?.OpenDb();
        var reader =
            DbClient?.SelectWhere(MaterialTable.TABLE_NAME, "*", $"{MaterialTable.MAT_NAME}='{mat.Name.Value}'");
        if(reader is null || !reader.HasRows) { DbClient?.CloseSqlConnection(); return false; }

        reader.Read();
        mat.Id.Value = reader.GetInt32(MaterialTable.MAT_ID);
        mat.MolMass.Property.RealValue = reader.GetDouble(MaterialTable.MOLAR_MASS);
        mat.Density.Property.RealValue = reader.GetDouble(MaterialTable.DENSITY);
        mat.Density.RefId.Value = reader.GetInt32(MaterialTable.DENSITY_REF);
        mat.SpecificHeat.Property.RealValue = reader.GetDouble(MaterialTable.SPECIFIC_HEAT);
        mat.SpecificHeat.RefId.Value = reader.GetInt32(MaterialTable.SPECIFIC_HEAT_REF);
        mat.ThermalConductivity.Property.RealValue = reader.GetDouble(MaterialTable.THERMAL_CONDUCTIVITY);
        mat.ThermalConductivity.RefId.Value = reader.GetInt32(MaterialTable.THERMAL_CONDUCTIVITY_REF);
        mat.ThermalExpansion.Property.RealValue = reader.GetDouble(MaterialTable.THERMAL_EXPANSION);
        mat.ThermalExpansion.RefId.Value = reader.GetInt32(MaterialTable.THERMAL_EXPANSION_REF);
        mat.YoungModulus.Property.RealValue = reader.GetDouble(MaterialTable.YOUNG_MODULUS);
        mat.YoungModulus.RefId.Value = reader.GetInt32(MaterialTable.YOUNG_MODULUS_REF);
        mat.ShearModulus.Property.RealValue = reader.GetDouble(MaterialTable.SHEAR_MODULUS);
        mat.ShearModulus.RefId.Value = reader.GetInt32(MaterialTable.SHEAR_MODULUS_REF);
        mat.BulkModulus.Property.RealValue = reader.GetDouble(MaterialTable.BULK_MODULUS);
        mat.BulkModulus.RefId.Value = reader.GetInt32(MaterialTable.BULK_MODULUS_REF);
        mat.PoissonRatio.Property.RealValue = reader.GetDouble(MaterialTable.POISSON_RATIO);
        mat.PoissonRatio.RefId.Value = reader.GetInt32(MaterialTable.POISSON_RATIO_REF);
        mat.MothsHardness.Property.RealValue = reader.GetDouble(MaterialTable.MOTHS_HARDNESS);
        mat.MothsHardness.RefId.Value = reader.GetInt32(MaterialTable.MOTHS_HARDNESS_REF);
        mat.VickersHardness.Property.RealValue = reader.GetDouble(MaterialTable.VICKERS_HARDNESS);
        mat.VickersHardness.RefId.Value = reader.GetInt32(MaterialTable.VICKERS_HARDNESS_REF);
        mat.BrinellHardness.Property.RealValue = reader.GetDouble(MaterialTable.BRINELL_HARDNESS);
        mat.BrinellHardness.RefId.Value = reader.GetInt32(MaterialTable.BRINELL_HARDNESS_REF);
        mat.Description.Value = reader.GetString(MaterialTable.DESCRIPTION);

        DbClient?.CloseSqlConnection();
        return true;
    }
    public bool SearchFluidMat(FluidViewModel mat) {
        DbClient?.OpenDb();
        var reader =
            DbClient?.SelectWhere(FluidTable.TABLE_NAME, "*", $"{FluidTable.NAME}='{mat.Name.Value}'");
        if(reader is null || !reader.HasRows) { DbClient?.CloseSqlConnection(); return false; }

        reader.Read();
        mat.Id.Value = reader.GetInt32(FluidTable.ID);
        mat.Density.RealValue = reader.GetDouble(FluidTable.DENSITY);
        mat.SpecificHeat.RealValue = reader.GetDouble(FluidTable.SPECIFIC_HEAT);
        mat.ThermalConductivity.RealValue = reader.GetDouble(FluidTable.THERMAL_CONDUCTIVITY);
        mat.Viscosity.RealValue = reader.GetDouble(FluidTable.VISCOSITY);
        mat.LatentHeat.RealValue = reader.GetDouble(FluidTable.LATENT_HEAT);
        mat.MeltPoint.RealValue = reader.GetDouble(FluidTable.MELT_POINT);
        mat.BoilPoint.RealValue = reader.GetDouble(FluidTable.BOIL_POINT);
        mat.TensionCoeff.RealValue = reader.GetDouble(FluidTable.TENSION_COEFF);
        mat.ContactAngle.RealValue = reader.GetDouble(FluidTable.CONTACT_ANGLE);
        mat.Description.Value = reader.GetString(FluidTable.DESCRIPTION);
        mat.RefId.Value = reader.GetInt32(FluidTable.REF_ID);
        DbClient?.CloseSqlConnection();
        return true;
    }
    public bool SearchSolidMat(SolidViewModel mat) {
        DbClient?.OpenDb();
        var reader =
            DbClient?.SelectWhere(SolidTable.TABLE_NAME, "*", $"{SolidTable.NAME}='{mat.Name.Value}'");
        if(reader is null || !reader.HasRows) { DbClient?.CloseSqlConnection(); return false; }

        reader.Read();
        mat.Id.Value = reader.GetInt32(SolidTable.ID);
        mat.Density.RealValue = reader.GetDouble(SolidTable.DENSITY);
        mat.SpecificHeat.RealValue = reader.GetDouble(SolidTable.SPECIFIC_HEAT);
        mat.ThermalConductivity.RealValue = reader.GetDouble(SolidTable.THERMAL_CONDUCTIVITY);
        mat.Description.Value = reader.GetString(SolidTable.DESCRIPTION);
        mat.RefId.Value = reader.GetInt32(SolidTable.REF_ID);
        DbClient?.CloseSqlConnection();
        return true;
    }
    #endregion

    #region GetMatList
    //TODO : refactor needed 抽象为一个方法

    public ObservableCollection<DisplayValuePair<int>> GetMatList() {
        DbClient?.OpenDb();
        var reader = DbClient?.Select(MaterialTable.TABLE_NAME, $"{MaterialTable.MAT_ID},{MaterialTable.MAT_NAME}");
        ObservableCollection<DisplayValuePair<int>> matList = [];
        if(reader is null || !reader.HasRows) { DbClient?.CloseSqlConnection(); return matList; }

        while(reader.Read()) {
            matList.Add(new DisplayValuePair<int>(reader.GetString(MaterialTable.MAT_NAME), reader.GetInt32(MaterialTable.MAT_ID)));
        }
        DbClient?.CloseSqlConnection();
        return matList;
    }
    public ObservableCollection<DisplayValuePair<int>> GetSolidMatList() {
        DbClient?.OpenDb();
        var reader = DbClient?.Select(SolidTable.TABLE_NAME, $"{SolidTable.ID},{SolidTable.NAME}");
        ObservableCollection<DisplayValuePair<int>> matList = [];
        if(reader is null || !reader.HasRows) { DbClient?.CloseSqlConnection(); return matList; }

        while(reader.Read()) {
            matList.Add(new DisplayValuePair<int>(reader.GetString(SolidTable.NAME), reader.GetInt32(SolidTable.ID)));
        }
        DbClient?.CloseSqlConnection();
        return matList;
    }
    public ObservableCollection<DisplayValuePair<int>> GetFluidMatList() {
        DbClient?.OpenDb();
        var reader = DbClient?.Select(FluidTable.TABLE_NAME, $"{FluidTable.ID},{FluidTable.NAME}");
        ObservableCollection<DisplayValuePair<int>> matList = [];
        if(reader is null || !reader.HasRows) { DbClient?.CloseSqlConnection(); return matList; }

        while(reader.Read()) {
            matList.Add(new DisplayValuePair<int>(reader.GetString(FluidTable.NAME), reader.GetInt32(FluidTable.ID)));
        }
        DbClient?.CloseSqlConnection();
        return matList;
    }
    #endregion
    public bool UpdateMat(MaterialViewModel mat) {
        DbClient?.OpenDb();
        List<string> colNameList = _COMMON_MATERIAL_TABLE_COL.Split(',').ToList();
        List<string> colValueList = CommonMaterialTableValue(mat).Split(',').ToList();
        DbClient?.UpdateInto(MaterialTable.TABLE_NAME, colNameList, colValueList,
            $"{MaterialTable.MAT_ID}={mat.Id.Value}");
        DbClient?.CloseSqlConnection();
        return true;
    }
    public bool UpdateFluidMat(FluidViewModel mat) {
        DbClient?.OpenDb();
        List<string> colNameList = _COMMON_Fluid_TABLE_COL.Split(',').ToList();
        List<string> colValueList = CommonFluidTableValue(mat).Split(',').ToList();
        DbClient?.UpdateInto(FluidTable.TABLE_NAME, colNameList, colValueList,
            $"{FluidTable.ID}={mat.Id.Value}");
        DbClient?.CloseSqlConnection();
        return true;
    }
    public bool UpdateSolidMat(SolidViewModel mat) {
        DbClient?.OpenDb();
        List<string> colNameList = _COMMON_SOLID_TABLE_COL.Split(',').ToList();
        List<string> colValueList = CommonSolidTableValue(mat).Split(',').ToList();
        DbClient?.UpdateInto(SolidTable.TABLE_NAME, colNameList, colValueList,
            $"{SolidTable.ID}={mat.Id.Value}");
        DbClient?.CloseSqlConnection();
        return true;
    }
}