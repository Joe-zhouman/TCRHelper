// 
// TCRHelper
// DbProvider
// 2024-4-22-16:51
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

using DbProvider.Implement;
using Model.ViewModel.Config;

namespace DbProvider;

public class DbClientFactory {
    public static IDbClient Create(DbConfig config) {
        return config.Type.Value switch {
            DbType.SQLITE => new SqliteClient(""),
            // default is MySql
            _ => new MySqlClient($"Server={config.Address.Value};Port={config.Port.Value};Database={config.Db.Value};Uid={config.User.Value};Pwd={config.Password.Value};")
        };
    }
}