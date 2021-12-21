using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;

namespace MovieMatcher.Views
{
    /// <summary>
    /// Interaction logic for StaticsView.xaml
    /// </summary>
    public partial class StaticsView : UserControl, INotifyPropertyChanged
    {
        private bool _mariaSeriesVisibility;
        private bool _charlesSeriesVisibility;
        private bool _johnSeriesVisibility;
        private bool _testSeriesVisibility;
        private Visibility vs = Visibility.Hidden;

        public SeriesCollection ChartSeries { get; set; }

        public StaticsView()
        {
            ChartSeries = new SeriesCollection();
            InitializeComponent();
            GenerateChart();
            MariaSeriesVisibility = false;
            CharlesSeriesVisibility = true;
            JohnSeriesVisibility = true;
            TestSeriesVisibility = false;

            DataContext = this;
        }

        public void GenerateChart()
        {
            StackPanel StkPnl = new StackPanel();
            StkPnl.Orientation = Orientation.Horizontal;
            CheckBox ChkBx = new CheckBox();
            ChkBx.SetBinding(ToggleButton.IsCheckedProperty, "MariaSeriesVisibility"); 
            ChkBx.Content = "1";
            StkPnl.Children.Add(ChkBx);
            CheckBox ChkBx2 = new CheckBox();
            ChkBx2.SetBinding(ToggleButton.IsCheckedProperty, "CharlesSeriesVisibility");
            ChkBx2.Content = "2";
            StkPnl.Children.Add(ChkBx2);
            CheckBox ChkBx3 = new CheckBox();
            ChkBx3.SetBinding(ToggleButton.IsCheckedProperty, "JohnSeriesVisibility");
            ChkBx3.Content = "3";
            StkPnl.Children.Add(ChkBx3);
            CheckBox ChkBx4 = new CheckBox();
            ChkBx4.SetBinding(ToggleButton.IsCheckedProperty, "TestSeriesVisibility");
            ChkBx4.Content = "4";
            StkPnl.Children.Add(ChkBx4);
            Grid.Children.Add(StkPnl);
            List<string> ls = new List<string>();
            /*ls.Add("Maria");
            ls.Add("Charles");
            ls.Add("John");*/
            ls.Add("XasItem1");
            XBar.Labels = ls;
            ColumnSeries ClmnSrs = new ColumnSeries();
            ClmnSrs.Title = "test";
            List<int> LsValues = new List<int> { 5 };
            ClmnSrs.Values = LsValues.AsChartValues();
            ClmnSrs.SetBinding(VisibilityProperty, "MariaSeriesVisibility");
            ClmnSrs.MaxWidth = 1000;
            ClmnSrs.ColumnPadding = 0;
            ChartSeries.Add(ClmnSrs);

            ClmnSrs = new ColumnSeries();
            ClmnSrs.Title = "Maria";
            LsValues = new List<int> { 4 };
            ClmnSrs.Values = LsValues.AsChartValues();
            ClmnSrs.SetBinding(ToggleButton.IsCheckedProperty, "CharlesSeriesVisibility");
            ClmnSrs.MaxWidth = 1000;
            ClmnSrs.ColumnPadding = 0;
            ChartSeries.Add(ClmnSrs);

            ClmnSrs = new ColumnSeries();
            ClmnSrs.Title = "Charles";
            LsValues = new List<int> { 2 };
            ClmnSrs.Values = LsValues.AsChartValues();
            ClmnSrs.Visibility = Visibility.Visible;
            ClmnSrs.MaxWidth = 1000;
            ClmnSrs.ColumnPadding = 0;
            ChartSeries.Add(ClmnSrs);

            ClmnSrs = new ColumnSeries();
            ClmnSrs.Title = "John";
            LsValues = new List<int> { 8 };
            ClmnSrs.Values = LsValues.AsChartValues();
            ClmnSrs.Visibility = Visibility.Visible;
            ClmnSrs.MaxWidth = 1000;
            ClmnSrs.ColumnPadding = 0;
            ChartSeries.Add(ClmnSrs);

        }

        public bool MariaSeriesVisibility
        {
            get { return _mariaSeriesVisibility; }
            set
            {
                /*if (value)
                {
                    _mariaSeriesVisibility = Visibility.Visible;
                } else
                {
                    _mariaSeriesVisibility = Visibility.Hidden;
                }*/

                _mariaSeriesVisibility = value;
                OnPropertyChanged("MariaSeriesVisibility");
            }
        }

        public bool CharlesSeriesVisibility
        {
            get { return _charlesSeriesVisibility; }
            set
            {
                _charlesSeriesVisibility = value;
                OnPropertyChanged("CharlesSeriesVisibility");
            }
        }

        public bool JohnSeriesVisibility
        {
            get { return _johnSeriesVisibility; }
            set
            {
                _johnSeriesVisibility = value;
                OnPropertyChanged("JohnSeriesVisibility");
            }
        }

        public bool TestSeriesVisibility
        {
            get { return _testSeriesVisibility; }
            set
            {
                _testSeriesVisibility = value;
                OnPropertyChanged("TestSeriesVisibility");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
