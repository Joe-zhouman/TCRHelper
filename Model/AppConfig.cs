// 
// TCRHelper
// Model
// 2024-3-15-23:55
// *Author*    : Joe, Zhou Man
// *Email*     : man.man.man.man.a@gmail.com
// *Email*     : joe_zhouman@foxmail.com
// *QQ*        : 1592020915
// *Weibo*     : @zhouman_LFC
// *Twitter*   : @zhouman_LFC
// *Website*   : www.joezhouman.com
// *Github*    : https://github.com/Joe-zhouman
// *LeetCode*  : https://leetcode-cn.com/u/joe_zm/

namespace Model;


public class OcrConfig {
    public OcrType Type { get; set; } = OcrType.BAIDU_STD;
    public string ApiKey { get; set; } = "";
    public string SecretKey { get; set; } = "";
}

public class AppConfig {
    public OcrConfig OcrConfig { get; set; } = new OcrConfig();
}