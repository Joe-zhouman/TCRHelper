using Model.Config;
using Model.Db;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Utilities.RefQuery;
using Utilities;
using System.Windows.Threading;

namespace TCRHelper.Desktop.Win.Tab.Import;
/// <summary>
/// ImportTabPage.xaml 的交互逻辑
/// </summary>
public partial class ImportTabPage : UserControl {
    private AppConfig _appConfig;
    public AppConfig Config { get => _appConfig; set => _appConfig = value; }
    private Material _material = new Material();
    private Reference _ref = new Reference();
    public ImportTabPage() {
        InitializeComponent();
        RefGroupbox.DataContext = _ref;
        MatGroupBox.DataContext = _material;
    }
    private void ImportFromPlotButton_OnClick(object sender, RoutedEventArgs e) {
        PlotDataCollectionWindows w = new();
        w.ShowDialog();
    }

    private void ImportFromTabularButton_OnClick(object sender, RoutedEventArgs e) {
        TabularDataCollectionWindow w = new(Config);
        w.ShowDialog();
    }

    private async void SearchButton_OnClick(object sender, RoutedEventArgs e) {
        try {
            IRefQueryProduct query = RefQueryFactory.Create(RefQueryApi.CROSSREF);
            Reference reference = await query.GetRef(DoiTextBox.Text);
            TitleTextBox.Text = reference.Title;
            YearTextBox.Text = reference.Year;
            AuthorTextBox.Text = reference.Author;
            JournalTextBox.Text = reference.Journal;
        }
        catch(Exception exception) {
            Paragraph paragraph = new();
            Run run = new(exception.Message) {
                Foreground = Brushes.Red
            };
            paragraph.Inlines.Add(run);
            DescriptionRichTextBox.Document.Blocks.Add(paragraph);
        }
    }

    private void MatRefTextBox_OnLoad(object sender, EventArgs e) {
        if(sender is TextBox textBox) {
            textBox.DataContext = _material;
        }
    }
 

    private void TestButton_Click(object sender, RoutedEventArgs e) {
        InteractionUtilities.ShowAndHideTooltip(_ref.DOI, 3);
    }
}
