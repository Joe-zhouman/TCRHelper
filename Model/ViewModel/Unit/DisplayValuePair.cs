// 
// TCRHelper
// Model
// 2024-4-15-15:24
// *Author*    : Joe, Zhou Man
// *Email*     : man.man.man.man.a@gmail.com
// *Email*     : joe_zhouman@foxmail.com
// *QQ*        : 1592020915
// *Weibo*     : @zhouman_LFC
// *Twitter*   : @zhouman_LFC
// *Website*   : www.joezhouman.com
// *Github*    : https://github.com/Joe-zhouman
// *Bilibili*  : @satisfactions

namespace Model.ViewModel.Unit;

public class DisplayValuePair {
    public DisplayValuePair() { }
    public DisplayValuePair(string display, double multiplier, double addition = 0.0) {
        Display = display;
        Value = new Tuple<double, double>(multiplier, addition);
    }
    public string Display { get; }
    public Tuple<double, double> Value { get; }
}