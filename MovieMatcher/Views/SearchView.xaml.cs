using MovieMatcher.Models.Api.Components;
using MovieMatcher.ViewModels;
using Newtonsoft.Json;
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
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
        }

        //when the searchbutton is clicked, this function will fire filling the listbox with items
        private void SearchButton_Clicked(object sender, RoutedEventArgs e)
        {
            Grid.SetRow(SearchBar, 0);
            ResultBox.Items.Clear();

            var getMovieResult = Api.Search(searchTxt.Text);

            if (getMovieResult?.results == null || getMovieResult.results.Count == 0)
            {
                Label lbl = new Label();
                lbl.Content = "No Results Found";
                lbl.FontFamily = new FontFamily("Verdana");
                lbl.FontSize = 35;
                lbl.Foreground = Brushes.White;
                ResultBox.Items.Add(lbl);
                lbl.VerticalAlignment = VerticalAlignment.Center;
            }
            else
            {
                foreach (MultiSearchResult s in getMovieResult.results)
                {
                    var bc = new BrushConverter();
                    ListBoxItem itm = new ListBoxItem();

                    Button btn = new Button();
                    btn.HorizontalAlignment = HorizontalAlignment.Left;
                    btn.VerticalAlignment = VerticalAlignment.Top;
                    btn.FontFamily = new FontFamily("Verdana");
                    btn.Width = 130;
                    btn.Background = (Brush)bc.ConvertFrom("#3cb9f4");
                    btn.Margin = new Thickness(0, -4, 0, 0);

                    Grid grd = new Grid();

                    Image img = new Image();
                    if (s.poster_path == null && s.profile_path == null)
                    {
                        img.Source = new BitmapImage(new Uri(@"/Image/SamplePoster.png", UriKind.Relative));
                    }
                    else if (s.poster_path == null)
                    {
                        img.Source = new BitmapImage(new Uri($"{Api.ImageBase}{Api.W185}{s.profile_path}"));
                    }
                    else
                    {
                        img.Source = new BitmapImage(new Uri($"{Api.ImageBase}{Api.W185}{s.poster_path}"));
                    }
                    img.Stretch = Stretch.Fill;

                    grd.Children.Add(img);

                    btn.Content = grd;
                    btn.DataContext = s;
                    btn.MouseEnter += MovieHover_Entered;
                    btn.MouseLeave += MovieHover_Left;
                    btn.Click += DetailScreen_Clicked;

                    itm.Content = btn;
                    ResultBox.Items.Add(itm);
                }
            }
        }

        //When the Button hovers over the Button, This Function will fire and make grid with the movies title inside as a label
        private void MovieHover_Entered(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            Grid grd = (Grid)btn.Content;
            MultiSearchResult msr = (MultiSearchResult)btn.DataContext;

            SolidColorBrush brush = new SolidColorBrush();
            brush.Opacity = 0.5;
            brush.Color = Colors.White;

            Grid tGrd = new Grid();
            tGrd.Background = brush;
            tGrd.Height = grd.ActualHeight;
            tGrd.Width = grd.ActualWidth;

            TextBlock txt = new TextBlock();
            if (msr.title == null)
            {
                txt.Text = msr.name;
            }
            else
            {
                txt.Text = msr.title;
            }
            txt.Height = double.NaN;
            txt.Width = tGrd.Width;
            txt.TextWrapping = TextWrapping.Wrap;
            txt.VerticalAlignment = VerticalAlignment.Center;
            txt.HorizontalAlignment = HorizontalAlignment.Center;
            txt.TextAlignment = TextAlignment.Center;
            txt.FontFamily = new FontFamily("Verdana");

            tGrd.Children.Add(txt);
            grd.Children.Add(tGrd);



        }

        //Makes sure that the movie title with it's background dissapears when the mouse stops hovering above the button
        private void MovieHover_Left(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            Grid grd = (Grid)btn.Content;
            grd.Children.RemoveAt(1);

        }

        private void DetailScreen_Clicked(object sender, RoutedEventArgs e)
        {
            //WIP
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                SearchButton_Clicked(this, e);
            }
        }
    }
}
