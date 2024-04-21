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

public class ReferenceViewModel : ViewModelBase {
    public int Id { get; set; } = -1;
    private string _doi;

    public string DOI {
        get => _doi;
        set => SetField(ref _doi, value);
    }

    private string _title;

    public string Title {
        get => _title;
        set => SetField(ref _title, value);
    }

    private string _year;

    public string Year {
        get => _year;
        set => SetField(ref _year, value);
    }

    private string _author;

    public string Author {
        get => _author;
        set => SetField(ref _author, value);
    }

    private string _journal;

    public string Journal {
        get => _journal;
        set => SetField(ref _journal, value);
    }

    private string _description;

    public string Description {
        get => _description;
        set => SetField(ref _description, value);
    }

    public string Detail { get; set; }
}