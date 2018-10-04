using System.Windows;
using DevExpress.Xpf.Charts;
using TestSeriesDataMemberId.ViewModels;

namespace TestSeriesDataMemberId
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChartControl_OnCustomDrawSeries(object sender, CustomDrawSeriesEventArgs e)
        {
            e.LegendText = (e.Series.Tag as ComplexId)?.Key1 + (e.Series.Tag as ComplexId)?.Key2;
        }
    }
}
