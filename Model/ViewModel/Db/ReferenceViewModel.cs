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

namespace Model.ViewModel.Db;

public static class ReferenceTable {
    public const string TABLE_NAME = "reference";
    public const string ID = "ref_id";
    public const string DOI = "doi";
    public const string TITLE = "title";
    public const string YEAR = "year";
    public const string AUTHOR = "author";
    public const string JOURNAL = "journal";
    public const string DETAIL = "detail";
    public const string DESCRIPTION = "description";
}

public class ReferenceViewModel {
    public ViewModelProperty<int> Id { get; set; } = new();
    public ViewModelProperty<string> DOI { get; set; } = new();
    public ViewModelProperty<string> Title { get; set; } = new();
    public ViewModelProperty<string> Year { get; set; } = new();
    public ViewModelProperty<string> Author { get; set; } = new();
    public ViewModelProperty<string> Journal { get; set; } = new();
    public ViewModelProperty<string> Description { get; set; } = new();
    public string Detail { get; set; }
}