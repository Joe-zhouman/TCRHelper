// 
// TCRHelper
// Model
// 2024-3-16-10:18
// *Author*    : Joe, Zhou Man
// *Email*     : man.man.man.man.a@gmail.com
// *Email*     : joe_zhouman@foxmail.com
// *QQ*        : 1592020915
// *Weibo*     : @zhouman_LFC
// *Twitter*   : @zhouman_LFC
// *Website*   : www.joezhouman.com
// *Github*    : https://github.com/Joe-zhouman
// *LeetCode*  : https://leetcode-cn.com/u/joe_zm/

using System.Windows.Markup;

namespace Model;

public class EnumCollectionExtension : MarkupExtension {
    public Type EnumType { get; set; }

    public override object ProvideValue(IServiceProvider _) {
        if(EnumType != null) {
            return CreateEnumValueList(EnumType);
        }
        return default(object);
    }

    private List<object> CreateEnumValueList(Type enumType) {
        return Enum.GetNames(enumType)
            .Select(name => Enum.Parse(enumType, name))
            .ToList();
    }
}