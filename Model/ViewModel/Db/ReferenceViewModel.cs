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

public class ReferenceViewModel {
    public int Id { get; set; } = -1;
    public ViewModelProperty<string> DOI { get; set; } = new();
    public ViewModelProperty<string> Title { get; set; } = new();
    public ViewModelProperty<string> Year { get; set; } = new();
    public ViewModelProperty<string> Author { get; set; } = new();
    public ViewModelProperty<string> Journal { get; set; } = new();
    public ViewModelProperty<string> Description { get; set; } = new();
    public string Detail { get; set; }
}