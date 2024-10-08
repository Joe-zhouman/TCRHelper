﻿using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Utilities;
using Utilities.Ocr;

namespace TCRHelper.Desktop.Win.Tab.Import;
/// <summary>
/// IdentifyResultWindows.xaml 的交互逻辑
/// </summary>
public partial class IdentifyResultWindows : Window {
    private BitmapSource[,] _bitmapSources;
    private DataTable _identifiedResult;
    private IOcrProduct _ocr;
    private TabularDataCollectionWindow _parent;
    public IdentifyResultWindows(BitmapSource[,] bitmapSources, IOcrProduct ocr, TabularDataCollectionWindow parent) {
        InitializeComponent();
        _bitmapSources = bitmapSources;
        _ocr = ocr;
        InitIdentifyResultDataTable();
        IdentifyProgressBar.Maximum = _bitmapSources.Length;
        IdentifyProgressBar.Value = 0;
        _parent = parent;
    }
    private async void InitIdentify() {
        for(int rowIndex = 0; rowIndex < _bitmapSources.GetLength(0); rowIndex++) {
            for(int colIndex = 0; colIndex < _bitmapSources.GetLength(1); colIndex++) {
                await IdentifyCell(rowIndex, colIndex);
            }
        }
    }

    private void InitIdentifyResultDataTable() {
        _identifiedResult = new DataTable();
        var nCol = _bitmapSources.GetLength(1);
        var nRow = _bitmapSources.GetLength(0);
        for(int i = 0; i < nCol; i++) {
            _identifiedResult.Columns.Add($"Column{i}");
        }

        for(int i = 0; i < nRow; i++) {
            DataRow row = _identifiedResult.NewRow();
            for(int j = 0; j < nCol; j++) {
                row[j] = "-";
            }
            _identifiedResult.Rows.Add(row);
        }
        IdentifiedResultDataGrid.ItemsSource = _identifiedResult.DefaultView;
        foreach(DataGridColumn column in IdentifiedResultDataGrid.Columns) {
            column.Width =
                new DataGridLength(100);

            if(column is DataGridTemplateColumn templateColumn) {
                //Access the DataGridTemplateColumn
                //Create a CellEditingTemplate
                FrameworkElementFactory textBlockFactory = new(typeof(TextBlock));
                textBlockFactory.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
                DataTemplate dataTemplate = new() { VisualTree = textBlockFactory };
                templateColumn.CellTemplate = dataTemplate;
            }
        }
    }

    private async Task IdentifyCell(int rowIndex, int colIndex) {
        _identifiedResult.Rows[rowIndex][colIndex] = await _ocr.Identify(_bitmapSources[rowIndex, colIndex]);
        await Task.Delay(TimeSpan.FromSeconds(new Random().NextDouble() * 0.9 + 0.1));
        IdentifyProgressBar.Value++;
    }

    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
        var selectedCells = IdentifiedResultDataGrid.SelectedCells;
        IdentifyProgressBar.Maximum = selectedCells.Count;
        IdentifyProgressBar.Value = 0;
        foreach(DataGridCellInfo cellInfo in selectedCells) {
            int rowIndex = IdentifiedResultDataGrid.Items.IndexOf(cellInfo.Item);
            int colIndex = cellInfo.Column.DisplayIndex;
            if(rowIndex >= _identifiedResult.Rows.Count || colIndex >= _identifiedResult.Columns.Count) {
                InteractionUtilities.ShowAndHideTooltip("请选择范围内的单元格!", 2);
                return;
            }

            await IdentifyCell(rowIndex, colIndex);
        }
    }

    private async void IdentifyResultWindows_OnLoaded(object sender, RoutedEventArgs e) {
        InitIdentify();
    }

    private void UpdateDataButton_OnClick(object sender, RoutedEventArgs e) {
        Func<double, double, double> operation = OperationComboBox.Text switch {
            "+" => (a, b) => a + b,
            "-" => (a, b) => a - b,
            "*" => (a, b) => a * b,
            "/" => (a, b) => a / b,
            "^" => Math.Pow,
            "a/x" => (a, b) => b / a,
            "log2l" => (a, b) => Math.Pow(b, a),
            _ => throw new NotImplementedException("无发进行未定义的操作!")
        };
        if(!double.TryParse(OperationNumberTextBox.Text, out double operationNumber)) {
            InteractionUtilities.ShowAndHideTooltip("无法进行此操作!", 2);
            return;
        }
        if(!int.TryParse(ColumnTextBox.Text, out int n)) {
            InteractionUtilities.ShowAndHideTooltip("无法进行此操作!", 2);
            return;
        }
        for(int i = 0; i < _identifiedResult.Rows.Count; i++) {
            if(!double.TryParse(_identifiedResult.Rows[i][n].ToString(), out double num)) {
                InteractionUtilities.ShowAndHideTooltip("无法进行此操作!", 2);
                return;
            }
            _identifiedResult.Rows[i][n] = operation(num, operationNumber);
        }
    }

    private void SendDataButton_OnClick(object sender, RoutedEventArgs e) {
        int col1 = int.Parse(FirstColumnTextBox.Text);
        int col2 = int.Parse(SecondColumnTextBox.Text);
        List<Point> p = (from DataRow row in _identifiedResult.Rows select new Point(double.Parse(row[col1].ToString()), double.Parse(row[col2].ToString()))).ToList();
        _parent.parent.ShowPoints(p);
    }
}

