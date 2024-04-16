// 
// TCRHelper
// Model
// 2024-4-12-20:16
// *Author*    : Joe, Zhou Man
// *Email*     : man.man.man.man.a@gmail.com
// *Email*     : joe_zhouman@foxmail.com
// *QQ*        : 1592020915
// *Weibo*     : @zhouman_LFC
// *Twitter*   : @zhouman_LFC
// *Website*   : www.joezhouman.com
// *Github*    : https://github.com/Joe-zhouman
// *Bilibili*  : @satisfactions

using System.ComponentModel;

namespace Model.Config;
public enum OcrType {
    [Description("百度标准版")]
    BAIDU_STD,
}
public class OcrConfig {
    public OcrType Type { get; set; } = OcrType.BAIDU_STD;
    public string ApiKey { get; set; } = "";
    public string SecretKey { get; set; } = "";
}