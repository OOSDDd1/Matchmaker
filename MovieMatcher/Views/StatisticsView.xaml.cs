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
using Services;
using Stores;

namespace MovieMatcher.Views
{
    public partial class StatisticsView : UserControl, INotifyPropertyChanged
    {
        private Visibility vs = Visibility.Hidden;
        private BooleanToVisibilityConverter converter;
        public dynamic DynamicSeries;


        private HashSet<dynamic> CheckBoxSet = new();

        public SeriesCollection ChartSeries { get; set; }

        public StatisticsView()
        {
            ChartSeries = new SeriesCollection();
            InitializeComponent();
            SetPropertiesGenres();
            SetPropertiesActors();
            GenerateChart();
            DataContext = this;
        }

        public void SetPropertiesGenres()
        {
            List<Tuple<int,string>> WatchedGenres = DatabaseService.GetWatchedCountGenres(UserStore.id ?? 0);
            foreach (var property in WatchedGenres)
            {
                DynamicSeries = new ExpandoObject();
                DynamicSeries.Title = property.Item2;
                DynamicSeries.Size = property.Item1;
                DynamicSeries.Position = 0;
                DynamicSeries.Visible = true;
                CheckBoxSet.Add(DynamicSeries);
            }
        }

        public void SetPropertiesActors()
        {
            List<Tuple<int, string>> WatchedGenres = DatabaseService.GetWatchedCountActors(UserStore.id ?? 0);
            foreach (var property in WatchedGenres)
            {
                DynamicSeries = new ExpandoObject();
                DynamicSeries.Title = property.Item2;
                DynamicSeries.Size = property.Item1;
                DynamicSeries.Position = 1;
                DynamicSeries.Visible = true;
                CheckBoxSet.Add(DynamicSeries);
            }
        }

        public void GenerateChart()
        {
            StackPanel StkPnl = new StackPanel();
            StkPnl.Orientation = Orientation.Horizontal;
            List<string> ls = new List<string>();
            foreach (dynamic item in CheckBoxSet)
            {
                CheckBox chkBx = GenerateCheckBox((string)item.Title, (bool)item.Visible);
                StkPnl.Children.Add(chkBx);
                AddColumnSeries(chkBx, (string)item.Title, (int)item.Size, (int)item.Position);
            }
            ls.Add("Genre");
            ls.Add("Actor");
            XBar.Labels = ls;

            Grid.Children.Add(StkPnl);
        }

        public CheckBox GenerateCheckBox(string content, bool visible)
        {
            CheckBox ChkBx = new CheckBox();
            ChkBx.Content = content;
            ChkBx.IsChecked = visible;
            ChkBx.Checked += ClmVis;
            ChkBx.Unchecked += ClmVis;
            return ChkBx;
        }

        public void AddColumnSeries(CheckBox chkBx, string Title, int num, int pos)
        {
            ColumnSeries ClmnSrs = new ColumnSeries();
            ClmnSrs.Title = Title;
            List<int> LsValues = new List<int>();
            for (int i = 0; i < pos; i++)
            {
                LsValues.Add(0);
            }

            LsValues.Add(num);
            ClmnSrs.Values = LsValues.AsChartValues();
            chkBx.DataContext = ClmnSrs;
            ClmnSrs.MaxWidth = 1000;
            ClmnSrs.ColumnPadding = 0;
            ChartSeries.Add(ClmnSrs);
        }

        public void ClmVis(object sender, RoutedEventArgs e)
        {
            CheckBox chkBx = (CheckBox)sender;
            ColumnSeries clmnSrs = (ColumnSeries)chkBx.DataContext;

            clmnSrs.Visibility = ((bool)chkBx.IsChecked) ? Visibility.Visible : Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
