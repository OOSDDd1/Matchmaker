using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using LiveCharts;
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

        public StaticsView()
        {
            InitializeComponent();

            MariaSeriesVisibility = true;
            CharlesSeriesVisibility = true;
            JohnSeriesVisibility = false;

            DataContext = this;
        }

        public bool MariaSeriesVisibility
        {
            get { return _mariaSeriesVisibility; }
            set
            {
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
