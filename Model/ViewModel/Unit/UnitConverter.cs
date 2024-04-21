// 
// TCRHelper
// Model
// 2024-4-15-20:20
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

using System.Globalization;
using System.Windows.Data;

namespace Model.ViewModel.Unit;

public class UnitConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return parameter is Tuple<double, double> tuple && value is double val
            ? (val - tuple.Item2) / tuple.Item1
            : value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return parameter is Tuple<double, double> tuple && value is double val
            ? val * tuple.Item1 + tuple.Item2
            : value;
    }
}