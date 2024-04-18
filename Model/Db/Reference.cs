﻿// * Author *    : Joe, Zhou Man 
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

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.Db;

public class Reference : INotifyPropertyChanged {
    public int Id { get; set; }
    public string DOI { get; set; }
    public string Title { get; set; }
    public string Year { get; set; }
    public string Author { get; set; }
    public string Journal { get; set; }
    public string? Description { get; set; }
    public string Detail { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if(EqualityComparer<T>.Default.Equals(field, value))
            return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}