// 
// TCRHelper
// Model
// 2024-4-26-11:27
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

using Model.ViewModel.Unit;
using System.Collections.ObjectModel;

namespace Model.ViewModel.Db;

public class MatListComboBoxViewModel {
    public ObservableCollection<DisplayValuePair<int>>? MatList { get; set; }
}