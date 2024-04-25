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
using System.Data;

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
        $"'{material.Name.Value}',{material.MolMass.Property.Value},{material.Density.Property.Value},{material.Density.RefId.Value},{material.SpecificHeat.Property.Value},{material.SpecificHeat.RefId.Value},{material.ThermalConductivity.Property.Value},{material.ThermalConductivity.RefId.Value},{material.ThermalExpansion.Property.Value},{material.ThermalExpansion.RefId.Value},{material.YoungModulus.Property.Value},{material.YoungModulus.RefId.Value},{material.ShearModulus.Property.Value},{material.ShearModulus.RefId.Value},{material.BulkModulus.Property.Value},{material.BulkModulus.RefId.Value},{material.PoissonRatio.Property.Value},{material.PoissonRatio.RefId.Value},{material.MothsHardness.Property.Value},{material.MothsHardness.RefId.Value},{material.VickersHardness.Property.Value},{material.VickersHardness.RefId.Value},{material.BrinellHardness.Property.Value},{material.BrinellHardness.RefId.Value},'{material.Description.Value}'";

    private static readonly string _COMMON_RESISTANCE_TABLE_COL = $"{ResistanceTable.RESISTANCE},{ResistanceTable.CONTACT_MAT_1},{ResistanceTable.CONTACT_MAT_2}.{ResistanceTable.MAT_1_RA},{ResistanceTable.MAT_2_RA},{ResistanceTable.MAT_1_RZ},{ResistanceTable.MAT_2_RZ},{ResistanceTable.MAT_1_RSM},{ResistanceTable.MAT_2_RSM},{ResistanceTable.MAT_1_RMR},{ResistanceTable.MAT_2_RMR},{ResistanceTable.AMBIENT_PRESS},{ResistanceTable.AMBIENT_TEMP},{ResistanceTable.CONTACT_PRESS},{ResistanceTable.HEAT_FLUX},{ResistanceTable.REF},{ResistanceTable.DESCRIPTION}";

    private static string CommonResistanceTableValue(ContactViewModel contact) => $"{contact.ContactResistance.Value},{contact.Surface1.Name.Value},{contact.Surface2.Name.Value},{contact.Surface1.RA.Value},{contact.Surface2.RA.Value},{contact.Surface1.RZ.Value},{contact.Surface2.RZ.Value},{contact.Surface1.RSM.Value},{contact.Surface2.RSM.Value},{contact.Surface1.RMR.Value},{contact.Surface2.RMR.Value},{contact.AmbientPressure.Value},{contact.AmbientTemperature.Value},{contact.ContactPressure.Value},{contact.HeatFlux.Value},{contact.RefId.Value},'{contact.Description.Value}'";

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

    public bool InsertMat(MaterialViewModel material) {
        DbClient?.OpenDb();
        if(Exist(MaterialTable.TABLE_NAME, MaterialTable.MAT_NAME, material.Name.Value)) {
            DbClient?.CloseSqlConnection();
            return false;
        }
        DbClient?.InsertIntoSpecific(MaterialTable.TABLE_NAME, _COMMON_MATERIAL_TABLE_COL, CommonMaterialTableValue(material));
        DbClient?.CloseSqlConnection();
        return true;
    }

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
}