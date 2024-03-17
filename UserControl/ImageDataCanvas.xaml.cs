using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Utilities;
using Brushes = System.Windows.Media.Brushes;
using Path = System.IO.Path;
using Point = System.Windows.Point;
namespace TCRHelperUserControl {
    /// <summary>
    /// ImageDataGathering.xaml 的交互逻辑
    /// </summary>
    public partial class ImageDataCanvas : UserControl {
        private readonly BitmapImage _defaultImage;
        private static string _default_image_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img/OpenFromFolder.png");
        private bool _defaultImageLoaded;
        public bool lineMovable;
        private Line? _movedLine;
        private double _movingDistance;
        public bool pointAddable;
        private List<Ellipse> _points;
        private Ellipse? _selectedPoint;
        private List<Line> _lines;
        private List<Point> _splitePoints;

        public ImageDataCanvas() {
            InitializeComponent();
            _defaultImage = new BitmapImage();
            _defaultImage.BeginInit();
            _defaultImage.UriSource = new Uri(_default_image_path, UriKind.RelativeOrAbsolute);
            _defaultImage.EndInit();
            ResetImageToDefault();
            lineMovable = true;
            _movingDistance = 2;
            _points = [];
            _selectedPoint = null;
            pointAddable = false;
            _lines = [];
            _splitePoints = [];
        }
        /// <summary>
        /// 将Image控件的备注设置为默认背景
        /// </summary>
        public void ResetImageToDefault() {
            this.LoadedImage.Stretch = Stretch.None;
            this.LoadedImage.HorizontalAlignment = HorizontalAlignment.Center;
            this.LoadedImage.VerticalAlignment = VerticalAlignment.Center;
            this.LoadedImage.Source = _defaultImage;
            _defaultImageLoaded = true;
        }

        /// <summary>
        /// 将路径指定的照片加载到Image控件里
        /// </summary>
        /// <param name="path"></param>
        private void SetImage(string path) {
            BitmapImage bitmap = new(new Uri(path));
            SetImage(bitmap);
        }
        /// <summary>
        /// 将指定的ImageSource嵌入Image控件中
        /// </summary>
        /// <param name="image"></param>
        private void SetImage(ImageSource image) {
            LoadedImage.Stretch = Stretch.Uniform;
            LoadedImage.Source = image;
            _defaultImageLoaded = false;
        }

        private void ImageOnKeyDown(Key key) {
            switch(key) {
                case Key.V when
                    (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control: {
                        SetImageFromClipboard();
                        break;
                    }
                case Key.Delete or Key.Back:
                    ResetImageToDefault();
                    break;
                case Key.C when Keyboard.Modifiers == ModifierKeys.Control: {
                        if(_defaultImageLoaded) { return; }
                        InteractionUtilities.ShowAndHideTooltip("图片已写入至剪切板");
                        //TODO: test 
                        Clipboard.SetImage(LoadedImage.Source as BitmapSource);
                        break;
                    }
            }
        }

        private void LineOnKeyDown(Key key) {
            switch(key) {
                case Key.Left
                    when(_movedLine == XMinLine || _movedLine == XMaxLine): {
                        _movedLine.X1 -= _movingDistance;
                        _movedLine.X2 = _movedLine.X1;
                        break;
                    }
                case Key.Right
                    when(_movedLine == XMinLine || _movedLine == XMaxLine): {
                        _movedLine.X1 += _movingDistance;
                        _movedLine.X2 = _movedLine.X1;
                        break;
                    }
                case Key.Up
                    when(_movedLine == YMinLine || _movedLine == YMaxLine): {
                        _movedLine.Y1 -= _movingDistance;
                        _movedLine.Y2 = _movedLine.Y1;
                        break;
                    }
                case Key.Down
                    when(_movedLine == YMinLine || _movedLine == YMaxLine): {
                        _movedLine.Y1 += _movingDistance;
                        _movedLine.Y2 = _movedLine.Y1;
                        break;
                    }
                default: { return; }
            }
        }
        private void PointOnKeyDown(Key key) {
            Debug.Assert(_selectedPoint != null, nameof(_selectedPoint) + " != null");
            switch(key) {
                case Key.Left: {
                        Canvas.SetLeft(_selectedPoint,
                            Canvas.GetLeft(_selectedPoint) - _movingDistance);
                        break;
                    }
                case Key.Right: {
                        Canvas.SetLeft(_selectedPoint,
                            Canvas.GetLeft(_selectedPoint) + _movingDistance);
                        break;
                    }
                case Key.Up: {
                        Canvas.SetTop(_selectedPoint,
                        Canvas.GetTop(_selectedPoint) - _movingDistance);
                        break;
                    }
                case Key.Down: {
                        Canvas.SetTop(_selectedPoint,
                            Canvas.GetTop(_selectedPoint) + _movingDistance);
                        break;
                    }
                case Key.Delete: {
                        _points.Remove(_selectedPoint);
                        ImageProcessCanvas.Children.Remove(_selectedPoint);
                        if(_points.Count == 0) { _selectedPoint = null; }
                        else {
                            _selectedPoint = _points.Last();
                            _selectedPoint.Style = Resources["PointSelected"] as Style;
                        }
                        break;
                    }
                default: { return; }
            }
        }
        /// <summary>
        /// 在Canvas里用键盘交互.
        /// </summary>
        /// <remarks>
        /// 具体交互逻辑:
        /// <list type="number">
        /// <item>
        /// <term>Ctrl+C</term>
        /// <description>若剪切板存在图片, 则将图片显示在Image控件里</description>
        /// </item>
        /// <item>
        /// <term>Ctrl+V</term>
        /// <description>若Image控件内显示的不是默认背景图片, 则将图片写入剪切板内</description>
        /// </item>
        /// <item>
        /// <term>DEL或BACK</term>
        /// <description>若Image控件内显示的不是默认背景图片, 则将背景还原为默认图片</description>
        /// </item>
        /// <item>
        /// <term>上下左右</term>
        /// <description>对应线元素微微移动</description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageProcessCanvas_OnKeyDown(object sender, KeyEventArgs e) {
            if(lineMovable && _movedLine is not null) {
                LineOnKeyDown(e.Key);
            }
            else if(pointAddable && _selectedPoint is not null) {
                PointOnKeyDown(e.Key);
            }
            else {
                ImageOnKeyDown(e.Key);
            }
        }
        private void SetImageFromClipboard() {
            bool getImage = false;
            if(Clipboard.ContainsFileDropList()) {
                string? path = Clipboard.GetFileDropList()[0];
                if(path != null && VerificationUtilities.IsImage(path)) {
                    SetImage(path);
                    getImage = true;
                }
            }
            else if(Clipboard.ContainsImage()) {
                var dataObject = Clipboard.GetDataObject();
                //var formats = dataObject.GetFormats();
                if(dataObject?.GetData("System.Drawing.Bitmap") is Bitmap bitmap) {
                    SetImage(ConversionUtilities.Bitmap2BitmapSource(bitmap));
                    getImage = true;
                }
            }

            if(!getImage) {
                InteractionUtilities.ShowAndHideTooltip("图片已写入至剪切板");
            }
        }
        private void ImageProcessCanvas_OnLoaded(object sender, RoutedEventArgs e) {
            (sender as Canvas)?.Focus();
        }

        /// <summary>
        /// 选中line元素, 将Line元素的线性从实线变为虚线
        /// </summary>
        /// <param name="line"></param>
        private void SelectLine(Line line) {
            if(!lineMovable) { return; }
            if(_movedLine != null) { _movedLine.Style = Resources["LineUnSelected"] as Style; }
            _movedLine = line;
            _movedLine.Style = Resources["LineSelected"] as Style;
        }
        private void SelectPoint(Ellipse ellipse) {
            if(_selectedPoint != null) {
                _selectedPoint.Style = Resources["PointUnSelected"] as Style;
            }
            _selectedPoint = ellipse;
            _selectedPoint.Style = Resources["PointSelected"] as Style;
        }
        /// <summary>
        /// 将line移动到指定位置
        /// </summary>
        /// <param name="point"></param>
        private void MoveLineToPoint(System.Windows.Point point) {
            Debug.Assert(_movedLine != null, nameof(_movedLine) + " != null");
            if(_movedLine == XMinLine || _movedLine == XMaxLine) {
                _movedLine.X1 = point.X;
                _movedLine.X2 = _movedLine.X1;
            }
            else {
                _movedLine.Y1 = point.Y;
                _movedLine.Y2 = _movedLine.Y1;
            }
        }
        private void ImageProcessCanvas_OnMouseLeftButtonDown(object sender, MouseEventArgs e) {
            ImageProcessCanvas.Focus();
            System.Windows.Point mousePosition = e.GetPosition(ImageProcessCanvas);
            HitTestResult result = VisualTreeHelper.HitTest(ImageProcessCanvas, mousePosition);
            if(result is null) { return; }
            if(result.VisualHit is Line line) {
                SelectLine(line);
                return;
            }
            if(result.VisualHit is Ellipse ellipse) {
                SelectPoint(ellipse);
                return;
            }
            if(lineMovable && _movedLine is not null) {
                MoveLineToPoint(mousePosition);
            }
            else if(pointAddable) { AddPoint(mousePosition); }
            else {
                if(!_defaultImageLoaded) { return; }
                OpenFileDialog openFileDialog = new() {
                    Filter = "Image Files|*.jpg;*.jpeg;*.png;",
                    Title = "Select an Image"
                };
                bool? dialog = openFileDialog.ShowDialog();
                if(dialog != true) { return; }
                SetImage(openFileDialog.FileName);
            }
        }

        private void AddPoint(Point mousePosition) {
            Ellipse point = new();
            SelectPoint(point);
            Canvas.SetLeft(point, mousePosition.X - point.Width / 2);
            Canvas.SetTop(point, mousePosition.Y - point.Height / 2);
            ImageProcessCanvas.Children.Add(point);
            _points.Add(point);
        }

        public void ConvertToCoordinate(double x1, double x2, double y1, double y2, ref List<Point> p) {
            ConversionUtilities.CoordinateConverter converter = new(
                XMinLine.X1,
                XMaxLine.X1,
                YMinLine.Y1,
                YMaxLine.Y1,
                x1, x2,
                y1, y2);
            foreach(Ellipse point in _points) {
                double xc = Canvas.GetLeft(point) + point.Width / 2;
                double yc = Canvas.GetTop(point) + point.Height / 2;
                p.Add(converter.Convert(xc, yc));
            }
        }

        private void ImageProcessCanvas_OnDrop(object sender, DragEventArgs e) {
            if(!e.Data.GetDataPresent(DataFormats.FileDrop)) { return; }
            if(e.Data.GetData(DataFormats.FileDrop) is not string[] files || files.Length == 0) { return; }
            string path = files[0];
            if(!VerificationUtilities.IsImage(path)) { return; }
            SetImage(path);
        }
        public void ClearPoints() {
            _selectedPoint = null;
            foreach(Ellipse point in _points) {
                ImageProcessCanvas.Children.Remove(point);
            }
            _points.Clear();
        }
        public void ConnectPoints(int sortMethod) {
            if(sortMethod == 1) {
                _points.Sort(
                    (a, b) => Canvas.GetLeft(a).CompareTo(Canvas.GetLeft(b))
                        );
            }
            else if(sortMethod == 2) {
                _points.Sort(
                        (a, b) => Canvas.GetTop(a).CompareTo(Canvas.GetTop(b))
                    );
            }
            for(int i = 0; i < _points.Count - 1; ++i) {
                Line l = new() {
                    X1 = Canvas.GetLeft(_points[i]) + _points[i].Width / 2,
                    Y1 = Canvas.GetTop(_points[i]) + _points[i].Height / 2,
                    X2 = Canvas.GetLeft(_points[i + 1]) + _points[i + 1].Width / 2,
                    Y2 = Canvas.GetTop(_points[i + 1]) + _points[i + 1].Height / 2,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };
                _lines.Add(l);
                ImageProcessCanvas.Children.Add(l);
            }
        }
        public void ClearLines() {
            foreach(Line line in _lines) {
                ImageProcessCanvas.Children.Remove(line);
            }
            _lines.Clear();
        }

        public void ShowSplitLines(int nRow, int nCol, int tabularType) {
            if(!AddSplitPoints(nRow, nCol, tabularType)) { return; }
            AddXLine(0);
            AddYLine(0);
            for(int i = 1; i < nRow - 1; ++i) { AddXLine(i); }
            for(int i = nRow - 1; i < nCol + nRow - 3; ++i) { AddYLine(i); }

            foreach(Line line in _lines) {
                ImageProcessCanvas.Children.Add(line);
            }
        }

        public bool AddSplitPoints(int nRow, int nCol, int tabularType) {
            _splitePoints.Clear();
            if(tabularType == 0) {
                double dx = (XMaxLine.X1 - XMinLine.X1) / nCol;
                double dy = (YMinLine.Y1 - YMaxLine.Y1) / nRow;
                for(int i = 1; i < nRow; ++i) {
                    _splitePoints.Add(new Point(
                            XMinLine.X1 + dx,
                            YMaxLine.Y1 + i * dy)
                        );
                }

                for(int i = 2; i < nCol; ++i) {
                    _splitePoints.Add(new Point(
                            XMinLine.X1 + i * dx,
                            YMaxLine.Y1 + dy)
                        );
                }
            }
            else if(tabularType == 1) {
                if(_points.Count != (nRow + nCol - 3)) {
                    InteractionUtilities.ShowAndHideTooltip("选取的分割点数量不对.");
                    return false;
                }
                foreach(Ellipse point in _points) {
                    _splitePoints.Add(new Point(
                            Canvas.GetLeft(point) + point.Width / 2,
                            Canvas.GetTop(point) + point.Height / 2
                        ));
                }
            }
            return true;
        }

        public void SplitImage(int nRow, int nCol, ref BitmapSource[,] images, int border = 0) {
            List<double> splitY = [
                YMaxLine.Y1
            ];
            for(int i = 0; i < nRow - 1; ++i) {
                splitY.Add(_splitePoints[i].Y);
            }
            splitY.Add(YMinLine.Y1);
            List<double> splitX = [
                XMinLine.X1,
                _splitePoints[0].X
            ];
            for(int i = nRow - 1; i < nRow + nCol - 3; ++i) {
                splitX.Add(_splitePoints[i].X);
            }
            splitX.Add(XMaxLine.X1);

            double scaleX = LoadedImage.ActualWidth / LoadedImage.Source.Width;
            double scaleY = LoadedImage.ActualHeight / LoadedImage.Source.Height;
            TransformedBitmap scaledBitmap = new(
                    (LoadedImage.Source as BitmapSource)!,
                    new ScaleTransform(scaleX, scaleY)
                );
            var imageDX = (ImageProcessCanvas.ActualWidth - LoadedImage.ActualWidth) / 2;
            var imageDY = (ImageProcessCanvas.ActualHeight - LoadedImage.ActualHeight) / 2;
            for(int i = 0; i < nRow; i++) {
                double height = splitY[i + 1] - splitY[i] - 2 * border;
                double ys = splitY[i] + border;
                for(int j = 0; j < nCol; j++) {
                    double width = splitX[j + 1] - splitX[j] - 2 * border;
                    double xs = splitX[j] + border;
                    var rect = new Int32Rect(
                            (int)(xs - imageDX),
                            (int)(ys - imageDY),
                            (int)(width),
                            (int)(height)
                        );
                    if(rect.X < 0) {
                        rect.Width += rect.X;
                        rect.X = 0;
                    }

                    if(rect.X + rect.Width > scaledBitmap.Width) {
                        rect.Width = (int)(scaledBitmap.Width - rect.X);
                    }

                    if(rect.Y < 0) {
                        rect.Height += rect.Y;
                        rect.Y = 0;
                    }
                    if(rect.Y + rect.Height > scaledBitmap.Height) {
                        rect.Height = (int)(scaledBitmap.Height - rect.Y);
                    }
                    images[i, j] = new CroppedBitmap(scaledBitmap, rect);
                }
            }
        }

        private void AddYLine(int i) {
            _lines.Add(new Line {
                X1 = _splitePoints[i].X,
                X2 = _splitePoints[i].X,
                Y1 = 0,
                Y2 = ImageProcessCanvas.ActualHeight,
                Style = Resources["LineBaseStyle"] as Style,
                Stroke = Brushes.Black
            });
        }

        private void AddXLine(int index) {
            _lines.Add(new Line {
                X1 = 0,
                X2 = ImageProcessCanvas.ActualWidth,
                Y1 = _splitePoints[index].Y,
                Y2 = _splitePoints[index].Y,
                Style = Resources["LineBaseStyle"] as Style,
                Stroke = Brushes.Black
            });
        }
    }
}
