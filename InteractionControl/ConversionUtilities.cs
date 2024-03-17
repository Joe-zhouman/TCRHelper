// 
// TCRHelper
// Control
// 2024-3-7-16:0
// *Author*    : Joe, Zhou Man
// *Email*     : man.man.man.man.a@gmail.com
// *Email*     : joe_zhouman@foxmail.com
// *QQ*        : 1592020915
// *Weibo*     : @zhouman_LFC
// *Twitter*   : @zhouman_LFC
// *Website*   : www.joezhouman.com
// *Github*    : https://github.com/Joe-zhouman
// *LeetCode*  : https://leetcode-cn.com/u/joe_zm/

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Utilities;

public static class ConversionUtilities {
    /// <summary>
    /// 将 System.Drawing.Bitmap类型转换为System.Windows.Media.Imaging.BitmapSource类型
    /// </summary>
    /// <param name="bitmap"></param>
    /// <returns></returns>
    public static BitmapSource Bitmap2BitmapSource(System.Drawing.Bitmap bitmap) {
        BitmapData bitmapData = bitmap.LockBits(
            new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.ReadOnly,
            bitmap.PixelFormat);

        BitmapSource bitmapSource = BitmapSource.Create(
            bitmapData.Width,
            bitmapData.Height,
            bitmap.HorizontalResolution,
            bitmap.VerticalResolution,
            PixelFormats.Bgr32, // Set the appropriate format (e.g., Bgr24)
            null,
            bitmapData.Scan0,
            bitmapData.Stride * bitmapData.Height,
            bitmapData.Stride);

        bitmap.UnlockBits(bitmapData);
        return bitmapSource;
    }
    public static BitmapSource ImageSourceToBitmapSource(ImageSource imageSource) {
        // 尝试将 ImageSource 转换为 BitmapSource
        if(imageSource is BitmapSource source) { return source; }
        else {
            // 如果无法直接转换，创建新的 BitmapSource
            DrawingVisual drawingVisual = new DrawingVisual();
            using(DrawingContext drawingContext = drawingVisual.RenderOpen()) {
                drawingContext.DrawImage(imageSource, new Rect(0, 0, imageSource.Width, imageSource.Height));
            }

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)imageSource.Width,
                (int)imageSource.Height, 96, 96, PixelFormats.Bgr32);
            renderTargetBitmap.Render(drawingVisual);

            return renderTargetBitmap;
        }
    }

    public static string BitmapSource2Base64(BitmapSource image) {
        using MemoryStream ms = new();
        BitmapEncoder encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(image));
        encoder.Save(ms);
        return Convert.ToBase64String(ms.ToArray());
    }
    public class CoordinateConverter {
        private double _xMinCanvas, _xMaxCanvas, _yMinCanvas, _yMaxCanvas;
        private double _xMinCor, _xMaxCor, _yMinCor, _yMaxCor;
        private double dx, dy;
        public CoordinateConverter(
            double xMinCanvas, double xMaxCanvas, double yMinCanvas, double yMaxCanvas,
            double xMinCor, double xMaxCor, double yMinCor, double yMaxCor) {
            _xMinCanvas = xMinCanvas;
            _xMaxCanvas = xMaxCanvas;
            _yMinCanvas = yMinCanvas;
            _yMaxCanvas = yMaxCanvas;
            _xMinCor = xMinCor;
            _xMaxCor = xMaxCor;
            _yMinCor = yMinCor;
            _yMaxCor = yMaxCor;
            dx = (_xMaxCor - _xMinCor) / (_xMaxCanvas - _xMinCanvas);
            dy = (_yMaxCor - _yMinCor) / (_yMaxCanvas - _yMinCanvas);
        }

        public System.Windows.Point Convert(double xc, double yc) =>
            new() {
                X = (xc - _xMinCanvas) * dx + _xMinCor,
                Y = (yc - _yMaxCanvas) * dy + _yMaxCor
            };
    }
}