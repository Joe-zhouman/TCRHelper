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

    public DbHelper(DbConfig config) {
        DbClient = DbClientFactory.Create(config);
    }
    public void TextDbConnect() {
        DbClient?.OpenDb();
        DbClient?.CloseSqlConnection();
    }

    public bool Exist(string tableName, string colName, string value) {
        var reader = DbClient?.ExecuteQuery(@$"SELECT 1
FROM {tableName}
WHERE {colName}={value}");
        if(reader is null || !reader.HasRows) { return false; }
        return true;

    }

    public bool InsertRef(ReferenceViewModel reference) {
        DbClient?.OpenDb();
        if(Exist(ReferenceTable.TABLE_NAME, ReferenceTable.DOI, reference.DOI.Value)) { DbClient?.CloseSqlConnection(); return false; }
        var reader = DbClient?.InsertIntoSpecific(ReferenceTable.TABLE_NAME,
            $"{ReferenceTable.DOI},{ReferenceTable.TITLE},{ReferenceTable.AUTHOR},{ReferenceTable.YEAR},{ReferenceTable.JOURNAL},{ReferenceTable.DESCRIPTION},{ReferenceTable.DETAIL}",
            $"{reference.DOI.Value},{reference.Title.Value},{reference.Author.Value},{reference.Year.Value},{reference.Journal.Value},{reference.Description.Value},{reference.Detail}");
        if(reader is null || !reader.HasRows) { throw new DBConcurrencyException(); }
        reader.Read();
        reference.Id.Value = reader.GetInt32(ReferenceTable.ID);
        DbClient?.CloseSqlConnection();
        return true;
    }
}