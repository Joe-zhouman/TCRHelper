// 
// TCRHelper
// DbProvider
// 2024-4-22-15:53
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
using MySql.Data.MySqlClient;

namespace DbProvider.Implement;

public class MySqlClient : DbClientBase {
    public MySqlClient(string connectionString) : base(connectionString) { }

    public override void OpenDb(string connectionString) {
        try {
            _dbConnection = new MySqlConnection(connectionString);
            _dbConnection.Open();
        }
        catch(Exception) {
            CloseSqlConnection();
            throw;
        }
    }

    public override bool HasTableCreated(string dbName, string tableName) {
        var query = ExecuteQuery($@"SELECT count(*) 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_SCHEMA = '{dbName}' 
AND TABLE_NAME = '{tableName}';");
        if(query is null)
            return false;
        query.Read();
        try {
            return query.GetBoolean(0);
        }
        catch(Exception e) {
            CloseSqlConnection();
            throw;
        }
    }
}