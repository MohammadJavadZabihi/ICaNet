using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ICaNet.WindowsApp.Views.DashboardViews
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Dashboard()
        {
            InitializeComponent();
            LoadChart();
        }

        private void LoadChart()
        {
            positiveProfit.AxisX.Add(new Axis
            {
                Labels = new List<string>
                {
                    "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
                    "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
                },
                ShowLabels = true,
                Foreground = Brushes.White,
            });

            positiveProfit.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<double> { 1000000, 1500000, 1200000, 2000000, 2500000, 2300000, 1900000, 2100000, 2400000, 2200000, 2700000, 2900000 },
                    Title = null,
                    Fill = new SolidColorBrush(Color.FromRgb(70, 130, 180))
                }
            };

            negativeProfit.AxisX.Add(new Axis
            {
                Labels = new List<string>
                {
                    "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
                    "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
                },
                ShowLabels = true,
                Foreground = Brushes.White,
            });

            negativeProfit.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<double> { 1000000, 1500000, 1200000, 2000000, 2500000, 2300000, 1900000, 2100000, 2400000, 2200000, 2700000, 2900000 },
                    Title = null,
                    Fill = new SolidColorBrush(Color.FromRgb(70, 130, 180))
                }
            };

            totalProfit.AxisX.Add(new Axis
            {
                Labels = new List<string>
                {
                    "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
                    "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
                },
                ShowLabels = true,
                Foreground = Brushes.White,
            });

            totalProfit.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<double> { 1000000, 1500000, 1200000, 2000000, 2500000, 2300000, 1900000, 2100000, 2400000, 2200000, 2700000, 2900000 },
                    Title = null,
                    Fill = new SolidColorBrush(Color.FromRgb(70, 130, 180))
                }
            };
        }
    }
}
