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

namespace Data.Db;

public class DbHelper {
    public IDbClient DbClient { get; set; }

    public DbHelper(DbConfig config) {
        DbClient = DbClientFactory.Create(config);
    }
    public void TextDbConnect() {
        DbClient.OpenDb();
        DbClient.CloseSqlConnection();
    }
}