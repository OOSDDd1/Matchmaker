using MovieMatcher.ViewModels;
using Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Models.Api;
using Models.Api.Components;
using Stores;

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

            if (!ApiService.Search(searchTxt.Text, out var getMovieResult, GreaterThan18(UserStore.birthYear ?? DateTime.Now)))
                return;

            if (getMovieResult?.results == null || getMovieResult.results.Count == 0)
            {
                Label lbl = new Label();
                lbl.Content = "No Results Found";
                lbl.FontFamily = new FontFamily("Verdana");
                lbl.FontSize = 35;
                lbl.Foreground = Brushes.White;
                lbl.VerticalAlignment = VerticalAlignment.Center;
                ResultBox.Items.Add(lbl);
            }
            else
            {
                foreach (MultiSearchResult s in getMovieResult.results)
                {
                    switch (s.media_type)
                    {
                        case "movie":
                            if (!ApiService.GetProviders(ApiService.MovieBase, s.id, out var movieProviders))
                                continue;
                            if (movieProviders == null) 
                                continue;
                            s.Watch_Providers = movieProviders;
                            break;
                        case "tv":
                            if (!ApiService.GetProviders(ApiService.ShowBase, s.id, out var showProviders))
                                continue;
                            if (showProviders == null) 
                                continue;
                            s.Watch_Providers = showProviders;
                            break;
                        default:
                            break;
                    }

                    ListBoxItem itm = new ListBoxItem();

                    Button btn = new Button();
                    btn.HorizontalAlignment = HorizontalAlignment.Left;
                    btn.VerticalAlignment = VerticalAlignment.Top;
                    btn.FontFamily = new FontFamily("Verdana");
                    btn.Background = (Brush) (new BrushConverter().ConvertFromString("#3cb9f4"));

                    Grid grd = new Grid();

                    Image img = new Image();
                    if (s.poster_path == null && s.profile_path == null)
                    {
                        img.Source = new BitmapImage(new Uri(@"/Images/SamplePoster.png", UriKind.Relative));
                    }
                    else if (s.poster_path == null)
                    {
                        img.Source = new BitmapImage(new Uri($"{ApiService.ImageBase}{ApiService.W185}{s.profile_path}"));
                    }
                    else
                    {
                        img.Source = new BitmapImage(new Uri($"{ApiService.ImageBase}{ApiService.W185}{s.poster_path}"));
                    }

                    img.Stretch = Stretch.Fill;
                    img.Width = 130;

                    btn.DataContext = s;
                    btn.MouseEnter += MovieHover_Entered;
                    btn.MouseLeave += MovieHover_Left;
                    btn.Click += DetailScreen_Clicked;

                    grd.Children.Add(img);
                    btn.Content = grd;
                    itm.Content = btn;
                    ResultBox.Items.Add(itm);
                }
            }
        }

        //When the Button hovers over the Button, This Function will fire and make grid with the movies title inside as a label
        private void MovieHover_Entered(object sender, MouseEventArgs e)
        {
            Button btn = (Button) sender;
            Grid grd = (Grid) btn.Content;
            MultiSearchResult msr = (MultiSearchResult) btn.DataContext;
            if (grd.Children.Count > 1)
            {
                ((Grid) grd.Children[1]).Visibility = Visibility.Visible;
            }
            else
            {
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

                if (msr.Watch_Providers != null && msr.Watch_Providers.results.ContainsKey("NL"))
                {
                    Provider provider = msr.Watch_Providers.results["NL"];
                    WrapPanel wPanel = new WrapPanel();
                    wPanel.Orientation = Orientation.Horizontal;
                    wPanel.HorizontalAlignment = HorizontalAlignment.Left;
                    wPanel.VerticalAlignment = VerticalAlignment.Top;


                    Dictionary<int, string> logoSources = new Dictionary<int, string>();
                    if (provider.flatrate != null)
                    {
                        GetLogos<Flatrate>(provider.flatrate, logoSources);
                    }

                    if (provider.buy != null)
                    {
                        GetLogos<Buy>(provider.buy, logoSources);
                    }

                    if (provider.rent != null)
                    {
                        GetLogos<Rent>(provider.rent, logoSources);
                    }

                    if (provider.ads != null)
                    {
                        GetLogos<Ads>(provider.ads, logoSources);
                    }

                    foreach (string item in logoSources.Values)
                    {
                        wPanel.Children.Add(CreateLogo(item));
                    }

                    tGrd.Children.Add(wPanel);
                }

                grd.Children.Add(tGrd);
            }
        }

        //Makes sure that the movie title with it's background dissapears when the mouse stops hovering above the button
        private void MovieHover_Left(object sender, MouseEventArgs e)
        {
            Button btn = (Button) sender;
            Grid grd = (Grid) btn.Content;
            ((Grid) grd.Children[1]).Visibility = Visibility.Hidden;
        }

        //When Completed this will lead to the DetailScreen with more info about the movie
        private void DetailScreen_Clicked(object sender, RoutedEventArgs e)
        {
            Button RealButton = (Button) sender;
            var tmp = (Content) RealButton.DataContext;
            DetailViewStore.Id = tmp.id;
            DetailViewStore.MediaType = tmp.media_type;

            Application.Current.Windows[0].DataContext = new DetailViewModel();
        }

        //this checks for the enter key to be pressed when in the searchbar, it will then invoke the SearchButton_Clicked method(see more above)
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SearchButton_Clicked(this, e);
            }
        }

        //Adds logopaths to dictionary for later use, used generic to get all types(ads, buy, rent, flatrate) in one strain of code
        private void GetLogos<T>(List<T> provider, Dictionary<int, string> logoSources) where T : ProviderGegevens
        {
            foreach (T item in provider)
            {
                if (!logoSources.ContainsKey(item.provider_id))
                {
                    logoSources.Add(item.provider_id, item.logo_path);
                }
            }
        }

        //creates logo image and returns it
        private Image CreateLogo(string source)
        {
            Image pImg = new Image();
            pImg.Source = new BitmapImage(new Uri($"{ApiService.ImageBase}{ApiService.W185}{source}", UriKind.Absolute));
            pImg.VerticalAlignment = VerticalAlignment.Top;
            pImg.HorizontalAlignment = HorizontalAlignment.Left;
            pImg.Width = 25;
            pImg.Height = 25;
            return pImg;
        }

        private static bool GreaterThan18(DateTime bornIn)
        {
            return (bornIn.AddYears(18) <= DateTime.Now);
        }
    }
}