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
using MovieMatcher.Models.Api;
using MovieMatcher.Models.Api.Components;

namespace MovieMatcher.Views
{
    public partial class ResultView : UserControl
    {
        public ResultView()
        {
            InitializeComponent();
            List<Show> Shows = new List<Show>();
            Show Show = Api.GetShow(62286);
            Shows.Add(Show);
            Show = Api.GetShow(106651);
            Shows.Add(Show);
            Show = Api.GetShow(60574);
            Shows.Add(Show);
            Show = Api.GetShow(94605);
            Shows.Add(Show);
            Show = Api.GetShow(1930);
            Shows.Add(Show);
            Show = Api.GetShow(1416);
            Shows.Add(Show);
            Show = Api.GetShow(90462);
            Shows.Add(Show);
            Show = Api.GetShow(71914);
            Shows.Add(Show);
            Show = Api.GetShow(119955);
            Shows.Add(Show);
            Show = Api.GetShow(93405);
            Shows.Add(Show);
            Show = Api.GetShow(60735);
            Shows.Add(Show);
            Show = Api.GetShow(93740);
            Shows.Add(Show);

            foreach (Show show in Shows)
            {
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
                bi.UriSource = new Uri("https://image.tmdb.org/t/p/w500/" + show.poster_path, UriKind.Absolute);
                bi.EndInit();
                Image.Stretch = Stretch.Fill;
                Image.Source = bi;
                Image.Width = 130;
                TextBlock.VerticalAlignment = VerticalAlignment.Center;
                TextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                ListItems.Items.Add(Grid);
            }
        }
    }
}
