// 
// TCRHelper
// DbProvider
// 2024-4-22-15:37
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

using System.Data.Common;

namespace DbProvider;

public interface IDbClient {
    /// <summary>
    /// 连接数据库
    /// </summary>
    void OpenDb();
    /// <summary>
    /// 连接数据库
    /// </summary>
    /// <param name="connectionString"></param>
    void OpenDb(string connectionString);
    /// <summary>
    /// 关闭连接
    /// </summary>
    void CloseSqlConnection();
    /// <summary>
    /// 执行 sql 语句
    /// </summary>
    /// <param name="sqlQuery"></param>
    /// <returns></returns>
    DbDataReader? ExecuteQuery(string sqlQuery);
    /// <summary>
    /// 是否已经创建表
    /// </summary>
    /// <param name="tableName">表的名称</param>
    /// <returns></returns>
    bool HasTableCreated(string dbName, string tableName);
    /// <summary>
    /// 创建表
    /// </summary>
    /// <param name="tableName">表的名称</param>
    /// <param name="colName">列的名称数组</param>
    /// <param name="colType">列类型的名称数组</param>
    /// <param name="keyIndex">主键位置</param>
    /// <param name="notNull">非空列的编号</param>
    /// <returns></returns>
    DbDataReader? CreateTable(string tableName, string[] colName, string[] colType, int keyIndex = -1,
        int[]? notNull = null);
    /// <summary>
    /// 删除表
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    DbDataReader? DropTable(string tableName);
    /// <summary>
    /// 读取表内所有数据
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    DbDataReader? ReadFullTable(string tableName);
    /// <summary>
    /// 插入数据（数据已转化为字符串）
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    DbDataReader? InsertInto(string tableName, string values);
    /// <summary>
    /// 在指定列中插入数据
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="cols"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    DbDataReader? InsertIntoSpecific(string tableName, string cols, string values);
    /// <summary>
    /// 按指定操作更新数据
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="updatePair"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    DbDataReader? UpdateInto<TKey>(string tableName, IDictionary<string, TKey> updatePair, string condition);
    /// <summary>
    /// 按指定操作更新数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="colNameList">列名</param>
    /// <param name="colValueList">列值</param>
    /// <param name="condition">更新条件</param>
    /// <returns></returns>
    DbDataReader? UpdateInto<TKey>(string tableName, ICollection<string> colNameList, ICollection<TKey> colValueList, string condition);
    /// <summary>
    /// 按指定操作删除数据
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    DbDataReader? DeleteWhere(string tableName, string condition);
    /// <summary>
    /// 清空表
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    DbDataReader? DeleteContents(string tableName);
    /// <summary>
    /// 按条件查找对应列数据
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="cols"></param>
    /// <param name="operation"></param>
    /// <returns></returns>
    DbDataReader? SelectWhere(string tableName, string cols, string operation);
    /// <summary>
    /// 查找对应列数据
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="cols"></param>
    /// <returns></returns>
    DbDataReader? Select(string tableName, string cols);
    /// <summary>
    /// 按顺序选取前n个数据
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="cols"></param>
    /// <param name="n"></param>
    /// <param name="orderCol"></param>
    /// <returns></returns>
    DbDataReader? SelectTop(string tableName, string cols, int n, string orderCol);
}