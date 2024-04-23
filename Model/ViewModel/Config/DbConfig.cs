// 
// TCRHelper
// Model
// 2024-4-22-16:52
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

using Model.ViewModel.Unit;
using System.Collections.ObjectModel;

namespace Model.ViewModel.Config;

public enum DbType {
    MYSQL,
    SQLITE
}
public class DbConfigComboBoxViewModel {
    private ObservableCollection<DisplayValuePair<DbType>>? _dbProvider;
    public ObservableCollection<DisplayValuePair<DbType>> DbProvider =>
        _dbProvider ??= [
            new DisplayValuePair<DbType>("MySql", DbType.MYSQL),
            new DisplayValuePair<DbType>("Sqlite", DbType.SQLITE)
        ];
}


public class DbConfig {
    public ViewModelProperty<DbType> Type { get; set; } = new() { Value = DbType.MYSQL };
    public ViewModelProperty<string> Db { get; set; } = new() { Value = "tcrdb" };
    public ViewModelProperty<string> Address { get; set; } = new() { Value = "127.0.0.1" };
    public ViewModelProperty<int> Port { get; set; } = new() { Value = 3306 };
    public ViewModelProperty<string> User { get; set; } = new();
    public ViewModelProperty<string> Password { get; set; } = new();
}