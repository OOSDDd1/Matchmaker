using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Services;
using Stores;

namespace MovieMatcher.Views
{
    public partial class StatisticsView : UserControl, INotifyPropertyChanged
    {
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
            foreach (var s in GenreCheckList.Children)
            {
                if(s is not TextBlock)
                {
                    ClmVis((CheckBox)s, new RoutedEventArgs());
                }
            }
            foreach (var s in ActorCheckList.Children)
            {
                if(s is not TextBlock)
                {
                    ClmVis((CheckBox)s, new RoutedEventArgs());
                }
            }

            MaxAmount[0] = 0;
            MaxAmount[1] = 0;
        }

        public void SetPropertiesGenres()
        {
            MaxAmount.Add(0, 0);
            List<Tuple<int, string>> WatchedGenres = DatabaseService.GetWatchedCountGenres(UserStore.id ?? 0);
            if (WatchedGenres.Count == 0)
            {
                TextBlock EmptyInput = new TextBlock();
                EmptyInput.Text = "No Items found that have been marked as seen, try marking a film or series as seen by using our matcher or changing a pre-existing review on your history page";
                EmptyInput.Foreground = Brushes.White;
                EmptyInput.TextWrapping = TextWrapping.Wrap;
                EmptyInput.FontSize = 14;
                GenreCheckList.Children.Add(EmptyInput);
      
            }
            else
            {
                foreach (var property in WatchedGenres)
                {
                    DynamicSeries = new ExpandoObject();
                    DynamicSeries.Title = property.Item2;
                    DynamicSeries.Size = property.Item1;
                    DynamicSeries.Position = 0;
                    DynamicSeries.Visible = false;
                    DynamicSeries.IsActor = false;
                    CheckBoxSet.Add(DynamicSeries);
                }
            }
        }

        public void SetPropertiesActors()
        {
            MaxAmount.Add(1, 0);
            List<Tuple<int, string>> WatchedGenres = DatabaseService.GetWatchedCountActors(UserStore.id ?? 0);
            if (WatchedGenres.Count == 0)
            {
                TextBlock EmptyInput = new TextBlock();
                EmptyInput.Text = "No Items found that have been marked as seen, try marking a film or series as seen by using our matcher or changing a pre-existing review on your history page";
                EmptyInput.Foreground = Brushes.White;
                EmptyInput.TextWrapping = TextWrapping.Wrap;
                EmptyInput.FontSize = 14;
                ActorCheckList.Children.Add(EmptyInput);
            }
            else
            {
                foreach (var property in WatchedGenres)
                {
                    DynamicSeries = new ExpandoObject();
                    DynamicSeries.Title = property.Item2;
                    DynamicSeries.Size = property.Item1;
                    DynamicSeries.Position = 1;
                    DynamicSeries.Visible = false;
                    DynamicSeries.IsActor = true;
                    CheckBoxSet.Add(DynamicSeries);
                }
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
            XBar.FontSize = 14;
            Grid.Children.Add(StkPnl);
        }

        public CheckBox GenerateCheckBox(string content, bool visible)
        {
            CheckBox ChkBx = new CheckBox();
            ChkBx.Content = content;
            ChkBx.FontSize = 14;
            ChkBx.Foreground = Brushes.White;
            ChkBx.Checked += ClmVis;
            ChkBx.Unchecked += ClmVis;
            ChkBx.IsChecked = visible;
            return ChkBx;
        }

        public void AddColumnSeries(CheckBox chkBx, string Title, int num, int pos)
        {
            ColumnSeries ClmnSrs = new ColumnSeries()
            {
                Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(pos, num),
                    }
            };
            ClmnSrs.Title = Title;
            ClmnSrs.FontSize = 14;
            ClmnSrs.Foreground = Brushes.White;
            ClmnSrs.DataContext = pos;
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
                chkBx.IsChecked = !(bool)chkBx.IsChecked;
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

        private void ActorClear_Clicked(object sender, RoutedEventArgs e)
        {
            foreach(CheckBox chkBx in ActorCheckList.Children)
            {
                chkBx.IsChecked = false;
            }
        }

        private void GenreClear_Clicked(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox chkBx in GenreCheckList.Children)
            {
                chkBx.IsChecked = false;
            }
        }
    }
}
