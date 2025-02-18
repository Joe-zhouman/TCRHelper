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

using Model.ViewModel.Db;
using Model.ViewModel.Unit;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace Data.Db;

public partial class DbHelper {


    private static readonly string _COMMON_SOLID_TABLE_COL = $"{SolidTable.NAME},{SolidTable.DENSITY},{SolidTable.THERMAL_CONDUCTIVITY},{SolidTable.SPECIFIC_HEAT},{SolidTable.DESCRIPTION}";

    private static string CommonSolidTableValue(SolidViewModel material) =>
        $"'{material.Name.Value.Replace(" ", "").ToLower()}',{material.Density.RealValue},{material.ThermalConductivity.RealValue},{material.SpecificHeat.RealValue},'{material.Description.Value}'";
    private static readonly string _COMMON_Fluid_TABLE_COL = $"{FluidTable.NAME},{FluidTable.DENSITY},{FluidTable.THERMAL_CONDUCTIVITY},{FluidTable.SPECIFIC_HEAT},{FluidTable.VISCOSITY},{FluidTable.LATENT_HEAT},{FluidTable.MELT_POINT},{FluidTable.BOIL_POINT},{FluidTable.TENSION_COEFF},{FluidTable.CONTACT_ANGLE},{FluidTable.DESCRIPTION}";

    private static string CommonFluidTableValue(FluidViewModel material) =>
        $"'{material.Name.Value.Replace(" ", "").ToLower()}',{material.Density.RealValue},{material.ThermalConductivity.RealValue},{material.SpecificHeat.RealValue},{material.Viscosity.RealValue},{material.LatentHeat.RealValue},{material.MeltPoint.RealValue},{material.BoilPoint.RealValue},{material.TensionCoeff.RealValue},{material.ContactAngle.RealValue},'{material.Description.Value}'";


    #region InsertMat
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



    #region SearchMat


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