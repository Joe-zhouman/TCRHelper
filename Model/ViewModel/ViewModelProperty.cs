﻿// 
// TCRHelper
// Model
// 2024-4-21-11:14
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

namespace Model.ViewModel;

public class ViewModelProperty<T> : ViewModelBase {
    private T _value;
    public T Value {
        get => _value;
        set => SetField(ref _value, value);
    }
}