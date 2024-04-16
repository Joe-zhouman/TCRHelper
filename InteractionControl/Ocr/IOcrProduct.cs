// 
// TCRHelper
// Utilities
// 2024-3-14-14:51
// *Author*    : Joe, Zhou Man
// *Email*     : man.man.man.man.a@gmail.com
// *Email*     : joe_zhouman@foxmail.com
// *QQ*        : 1592020915
// *Weibo*     : @zhouman_LFC
// *Twitter*   : @zhouman_LFC
// *Website*   : www.joezhouman.com
// *Github*    : https://github.com/Joe-zhouman
// *Bilibili*  : @satisfactions

using System.Windows.Media.Imaging;

namespace Utilities.Ocr;

public interface IOcrProduct {
    public Task<string> GetToken();
    public Task<string> Identify(BitmapSource image);
}