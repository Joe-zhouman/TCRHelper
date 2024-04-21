// 
// TCRHelper
// Utilities
// 2024-4-12-22:23
// *Author*    : Joe, Zhou Man
// *Email*     : man.man.man.man.a@gmail.com
// *Email*     : joe_zhouman@foxmail.com
// *QQ*        : 1592020915
// *Weibo*     : @zhouman_LFC
// *Twitter*   : @zhouman_LFC
// *Website*   : www.joezhouman.com
// *Github*    : https://github.com/Joe-zhouman
// *Bilibili*  : @satisfactions

using Model.ViewModel.Db;

namespace Utilities.RefQuery;

public interface IRefQueryProduct {
    public Task GetRef(ReferenceViewModel referenceViewModel);
}