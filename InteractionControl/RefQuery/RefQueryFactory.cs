// 
// TCRHelper
// Utilities
// 2024-4-13-22:45
// *Author*    : Joe, Zhou Man
// *Email*     : man.man.man.man.a@gmail.com
// *Email*     : joe_zhouman@foxmail.com
// *QQ*        : 1592020915
// *Weibo*     : @zhouman_LFC
// *Twitter*   : @zhouman_LFC
// *Website*   : www.joezhouman.com
// *Github*    : https://github.com/Joe-zhouman
// *Bilibili*  : @satisfactions

using Utilities.Ocr;
using Utilities.RefQuery.Implement;

namespace Utilities.RefQuery;

public enum RefQueryApi {
    CROSSREF,
}
public class RefQueryFactory {
    public static IRefQueryProduct Create(RefQueryApi apiType) {
        IOcrProduct ocr;
        return apiType switch {
            RefQueryApi.CROSSREF => new CrossrefQuery(),
            _ => new CrossrefQuery()
        };
    }
}