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

namespace Model.Db;

public class Reference {
    public int Id { get; set; }
    public string DOI { get; set; }
    public string Title { get; set; }
    public string Year { get; set; }
    public string Author { get; set; }
    public string Journal { get; set; }
    public string? Description { get; set; }
    public string Detail { get; set; }
}