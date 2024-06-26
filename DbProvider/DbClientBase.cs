﻿// 
// TCRHelper
// DbProvider
// 2024-4-22-15:42
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
using System.Text;

namespace DbProvider;

public abstract class DbClientBase : IDbClient {
    public string ConnectString { get; set; }
    protected DbConnection? _dbConnection;
    public virtual DbConnection? Connection { get => _dbConnection; set => value = _dbConnection; }
    protected DbCommand? _dbCommand;
    protected DbDataReader? _reader;
    public DbClientBase(string connectionString) {
        ConnectString = connectionString;
    }

    public void OpenDb() => OpenDb(ConnectString);
    public abstract void OpenDb(string connectionString);

    public void CloseSqlConnection() {
        _dbCommand?.Dispose();
        _dbCommand = null;
        _reader?.Dispose();
        _reader = null;
        _dbConnection?.Close();
        _dbConnection = null;
    }

    public DbDataReader? ExecuteQuery(string sqlQuery) {
        _reader?.Dispose();
        if(_dbConnection is null) { return null; }
        try {
            _dbCommand = _dbConnection.CreateCommand();
            _dbCommand.CommandText = sqlQuery;
            _reader = _dbCommand.ExecuteReader();
            return _reader;
        }
        catch(Exception) {
            CloseSqlConnection();
            throw;
        }
    }

    public abstract bool HasTableCreated(string dbName, string tableName);

    public DbDataReader? CreateTable(string tableName, string[] colName, string[] colType, int keyIndex = -1, int[]? notNull = null) {
        _reader?.Dispose();
        bool[] isNotNull = new bool[colName.Length];
        if(notNull != null)
            foreach(int i in notNull) {
                isNotNull[i] = true;
            }
        StringBuilder tmp = new StringBuilder();
        for(int i = 0; i < colName.Length; i++) {
            tmp.Append($@"{colName[i]} {colType[i]}{(keyIndex == i ? " PRIMARY KEY" : "")}{(isNotNull[i] ? " NOT NULL" : "")}");
            tmp.AppendLine($@"{(i == colName.Length - 1 ? ' ' : ',')}");
        }
        return ExecuteQuery($@"CREATE TABLE {tableName} (
{tmp})");
    }

    public DbDataReader? DropTable(string tableName) {
        _reader?.Dispose();
        return ExecuteQuery($@"DROP TABLE {tableName}");
    }

    public DbDataReader? ReadFullTable(string tableName) {
        _reader?.Dispose();
        return ExecuteQuery($@"SELECT * FROM {tableName}");
    }

    public DbDataReader? InsertInto(string tableName, string values) {
        _reader?.Dispose();
        return ExecuteQuery($@"INSERT INTO {tableName}
VALUES ({values})");
    }

    public DbDataReader? InsertIntoSpecific(string tableName, string cols, string values) {
        _reader?.Dispose();
        return ExecuteQuery($@"INSERT INTO {tableName}
({cols})
VALUES({values})");
    }

    public DbDataReader? UpdateInto<TKey>(string tableName, IDictionary<string, TKey> updatePair, string condition) {
        return UpdateInto(tableName, updatePair.Keys, updatePair.Values, condition);
    }

    public DbDataReader? UpdateInto<TKey>(string tableName, ICollection<string> colNameList, ICollection<TKey> colValueList,
        string condition) {
        _reader?.Dispose();
        StringBuilder query = new StringBuilder();
        query.AppendLine($@"UPDATE {tableName} SET");

        query.AppendLine(string.Join(',', from i in Enumerable.Range(0, colNameList.Count)
                                          select $"{colNameList.ElementAt(i)}={colValueList.ElementAt(i)}"));
        query.AppendLine($@"WHERE {condition}");
        return ExecuteQuery(query.ToString());
    }

    public DbDataReader? DeleteWhere(string tableName, string condition) {
        _reader?.Dispose();
        return ExecuteQuery($@"DELETE FROM {tableName}
WHERE {condition}");
    }

    public DbDataReader? DeleteContents(string tableName) {
        _reader?.Dispose();
        return ExecuteQuery($@"DELETE FROM {tableName}");
    }

    public DbDataReader? SelectWhere(string tableName, string cols, string operation) {
        _reader?.Dispose();
        return ExecuteQuery($@"SELECT {cols}
FROM {tableName}
WHERE {operation}");
    }

    public DbDataReader? Select(string tableName, string cols) {
        _reader?.Dispose();
        return ExecuteQuery($@"SELECT {cols}
FROM {tableName}");
    }

    public DbDataReader? SelectTop(string tableName, string cols, int n, string orderCol) {
        _reader?.Dispose();
        return ExecuteQuery($@"SELECT {cols}
FROM {tableName}
ORDER BY {orderCol} DESC LIMIT {n}");
    }
}