// 
// TCRHelper
// Control
// 2024-3-7-15:51
// *Author*    : Joe, Zhou Man
// *Email*     : man.man.man.man.a@gmail.com
// *Email*     : joe_zhouman@foxmail.com
// *QQ*        : 1592020915
// *Weibo*     : @zhouman_LFC
// *Twitter*   : @zhouman_LFC
// *Website*   : www.joezhouman.com
// *Github*    : https://github.com/Joe-zhouman
// *LeetCode*  : https://leetcode-cn.com/u/joe_zm/

using System.IO;

namespace Utilities;

public static class VerificationUtilities {
    /// <summary>
    /// 根据文件名判断文件是否为图片
    /// </summary>
    /// <remarks>
    /// 目前仅将jpg, jpeg, png, bmp文件视作图片文件
    /// </remarks>
    /// <param name="path">文件路径名</param>
    /// <returns></returns>
    public static bool IsImage(string path) {
        string extension = Path.GetExtension(path);
        string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp" };
        return imageExtensions.Contains(extension.ToLower());
    }
}