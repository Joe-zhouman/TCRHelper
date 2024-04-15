// 
// TCRHelper
// Model
// 2024-4-15-15:56
// *Author*    : Joe, Zhou Man
// *Email*     : man.man.man.man.a@gmail.com
// *Email*     : joe_zhouman@foxmail.com
// *QQ*        : 1592020915
// *Weibo*     : @zhouman_LFC
// *Twitter*   : @zhouman_LFC
// *Website*   : www.joezhouman.com
// *Github*    : https://github.com/Joe-zhouman
// *LeetCode*  : https://leetcode-cn.com/u/joe_zm/

namespace Model.Unit;

public static class SIUnitsConvertFactor {
    /// <summary>
    /// 单位前缀
    /// </summary>
    #region UnitPrefix

    public const double QUETTA = 1E30;
    public const double RONNA = 1E27;
    public const double YOTTA = 1E24;
    public const double ZETTA = 1E21;
    public const double EXA = 1E18;
    public const double PETA = 1E15;
    public const double TERA = 1E12;
    public const double GIGA = 1E9;
    public const double MEGA = 1E6;
    public const double KILO = 1000;
    public const double HECTO = 100;
    public const double DECA = 10;
    public const double DECI = 0.1;
    public const double CENTI = 0.01;
    public const double MILLI = 0.001;
    public const double MICRO = 1E-6;
    public const double NANO = 1E-9;
    public const double PICO = 1e-12;
    public const double FEMTO = 1e15;
    public const double ATTO = 1e-18;
    public const double ZEPTO = 1e-21;
    public const double YOCTO = 1e-27;
    public const double QUECTO = 1e-30;

    #endregion

    /// <summary>
    /// 常用的质量单位, 以千克为基准
    /// </summary>

    #region MassUnit

    public const double KILO_GRAM = 1.0;
    public const double GRAM = KILO_GRAM / KILO;
    #endregion

    #region LengthUnit

    public const double METER = 1.0;
    public const double CENTI_METER = CENTI * METER;
    public const double MILLI_METER = MILLI * METER;
    public const double MICRO_METER = MICRO * METER;
    #endregion

    #region TimeUnit

    public const double SECOND = 1.0;
    public const double MINUTE = 60 * SECOND;
    public const double HOUR = 60 * MINUTE;
    #endregion

    #region TemperatureUnit

    public const double KELVIN = 1.0;
    public const double CELSIUS_DEGREE = 1.0;
    public const double CELSTUS_DEGREE_ADDITION = 273.15;

    #endregion

    #region AmountOfSubstanceUnit

    public const double MOL = 1.0;

    #endregion

    #region OtherCommonUsedUnit

    public const double SQUARE_METER = METER * METER;
    public const double SQUARE_CENTI_METER = CENTI_METER * CENTI_METER;
    public const double SQUARE_MILLI_METER = MILLI_METER * MILLI_METER;

    public const double CUBIC_METER = METER * METER * METER;
    public const double CUBIC_CENTI_METER = CENTI_METER * CENTI_METER * CENTI_METER;
    public const double CUBIC_MILLI_METER = MILLI_METER * MILLI_METER * MILLI_METER;
    public const double LITRE = METER * METER * METER / DECA / DECA / DECA;
    public const double MILLI_LITRE = MILLI * LITRE;

    public const double NEWTON = KILO_GRAM * METER / SECOND / SECOND;

    public const double PASCAL = NEWTON / SQUARE_METER;
    public const double KILO_PASCAL = KILO * PASCAL;
    public const double MEGA_PASCAL = MEGA * PASCAL;
    public const double GIGA_PASCAL = GIGA * PASCAL;

    public const double JOULE = NEWTON / METER;
    public const double KILO_JOULE = KILO * JOULE;

    public const double WATT = JOULE / SECOND;
    public const double KILO_WATT = KILO * WATT;

    #endregion

}