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
        private Dictionary<int, int> MaxAmount = new();


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
            Loaded += AfterLoading;
        }

        public void AfterLoading(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox s in GenreCheckList.Children)
            {
                ClmVis(s, new RoutedEventArgs());
            }
            foreach (CheckBox s in ActorCheckList.Children)
            {
                ClmVis(s, new RoutedEventArgs());
            }

            MaxAmount[0] = 0;
            MaxAmount[1] = 0;
        }

        public void SetPropertiesGenres()
        {
            MaxAmount.Add(0, 0);
            List<Tuple<int, string>> WatchedGenres = DatabaseService.GetWatchedCountGenres(UserStore.id ?? 0);
            foreach (var property in WatchedGenres)
            {
                DynamicSeries = new ExpandoObject();
                DynamicSeries.Title = property.Item2;
                DynamicSeries.Size = property.Item1;
                DynamicSeries.Position = 0;
                DynamicSeries.Visible = false;
                CheckBoxSet.Add(DynamicSeries);
            }
        }

        public void SetPropertiesActors()
        {
            MaxAmount.Add(1, 0);
            List<Tuple<int, string>> WatchedGenres = DatabaseService.GetWatchedCountActors(UserStore.id ?? 0);
            foreach (var property in WatchedGenres)
            {
                DynamicSeries = new ExpandoObject();
                DynamicSeries.Title = property.Item2;
                DynamicSeries.Size = property.Item1;
                DynamicSeries.Position = 1;
                DynamicSeries.Visible = false;
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
                switch (item.Position)
                {
                    case 0:
                        GenreCheckList.Children.Add(chkBx);
                        break;
                    case 1:
                        ActorCheckList.Children.Add(chkBx);
                        break;
                    default:
                        StkPnl.Children.Add(chkBx);
                        break;
                }
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
            ChkBx.Foreground = Brushes.White;
            ChkBx.Checked += ClmVis;
            ChkBx.Unchecked += ClmVis;
            ChkBx.IsChecked = visible;
            return ChkBx;
        }

        public void AddColumnSeries(CheckBox chkBx, string Title, int num, int pos)
        {
            ColumnSeries ClmnSrs = new ColumnSeries();
            ClmnSrs.Title = Title;
            ClmnSrs.DataContext = pos;
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
            int pos = (int)clmnSrs.DataContext;
            if ((bool)chkBx.IsChecked)
            {
                MaxAmount[pos]++;
            }
            else
            {
                MaxAmount[pos]--;
            }

            if (MaxAmount[pos] <= 10)
            {

                clmnSrs.Visibility = ((bool)chkBx.IsChecked) ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                chkBx.IsChecked = ((bool)chkBx.IsChecked) ? false : true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GenreButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (GenreCheckList.Visibility == Visibility.Visible)
            {
                GenreCheckList.Visibility = Visibility.Collapsed;
                ((Button)sender).Content = "▼";
            }
            else
            {
                GenreCheckList.Visibility = Visibility.Visible;
                ((Button)sender).Content = "▲";
            }
        }

        private void ActorButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (ActorCheckList.Visibility == Visibility.Visible)
            {
                ActorCheckList.Visibility = Visibility.Collapsed;
                ((Button)sender).Content = "▼";
            }
            else
            {
                ActorCheckList.Visibility = Visibility.Visible;
                ((Button)sender).Content = "▲";
            }
        }
    }
}
