// 
// TCRHelper
// DbProvider
// 2024-4-22-16:8
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

using Microsoft.Data.Sqlite;
namespace DbProvider.Implement;

public class SqliteClient : DbClientBase {
    public SqliteClient(string connectionString) : base(connectionString) { }
    public override void OpenDb(string connectionString) {


        try {
            _dbConnection = new SqliteConnection($@"Data Source={connectionString}");
            _dbConnection.Open();
        }
        catch(Exception e) {
            CloseSqlConnection();
            throw;
        }
    }

    public override bool HasTableCreated(string dbName, string tableName) {
        var query = ExecuteQuery($@"SELECT count(*) 
FROM sqlite_master
WHERE type='table'
AND name='{tableName}");
        if(query is null)
            return false;
        query.Read();
        try {
            return query.GetBoolean(0);
        }
        catch(Exception e) {
            CloseSqlConnection();
            throw new SqliteException(e.Message, 1);
        }
    }
}