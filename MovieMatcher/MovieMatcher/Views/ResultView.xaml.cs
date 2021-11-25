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

namespace MovieMatcher.Views
{
    /// <summary>
    /// Interaction logic for ResultView.xaml
    /// </summary>
    public partial class ResultView : UserControl
    {
        public ResultView()
        {
            InitializeComponent();

            Button Button = new Button();
            Grid Grid = new Grid();
            Image Image = new Image();
            TextBlock TextBlock = new TextBlock();
            Grid.Children.Add(Image);
            Grid.Children.Add(TextBlock);
            Button.HorizontalAlignment = HorizontalAlignment.Left;
            Button.VerticalAlignment = VerticalAlignment.Top;
            Button.Width = 130;
            Button.Background = (Brush)(new BrushConverter().ConvertFromString("#3cb9f4"));
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("../testFilmPoster.jpg", UriKind.Relative);
            bi.EndInit();
            Image.Stretch = Stretch.Fill;
            Image.Source = bi;
            Image.Width = 130;
            TextBlock.VerticalAlignment = VerticalAlignment.Center;
            TextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            TextBlock.Text = "The Joker";
            ListItems.Items.Add(Grid);
        }
    }
}
