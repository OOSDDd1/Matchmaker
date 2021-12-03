using System;
using System.Windows.Controls;
using MovieMatcher.Models.Api;

namespace MovieMatcher.Views
{
    public partial class DetailView<T>: UserControl where T: IRoot 
    {
        public DetailView(int id)
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            switch (default(T))
            {
                case Movie:
                    Console.WriteLine("Movie");
                    break;
                case Show:
                    Console.WriteLine("Movie");
                    break;
                default:
                    Console.WriteLine("None");
                    break;
            }
        }
    }
}