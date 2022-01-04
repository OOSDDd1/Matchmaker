using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Models.Api;
using Models.Api.Components;
using Models.Database;
using Services;
using Stores;

namespace MovieMatcher.Views
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();

            GenerateLikedList();
            GenerateInterestedList();
            GenerateTrendingMovieList();
        }

        public void GenerateLikedList()
        {
            List<LikedContent> LikedItems = DatabaseService.GetLikedContent(UserStore.id ?? 0);
            List<Content> ContentList = new List<Content>();

            foreach (LikedContent LikedItem in LikedItems)
            {
                Content contentItem;
                if (LikedItem.IsShow)
                {
                    if (!ApiService.GetShow(LikedItem.Content, out Show? show))
                        continue;
                    if (show == null)
                        continue;
                    contentItem = (Content) show;
                }
                else
                {
                    if (!ApiService.GetMovie(LikedItem.Content, out Movie? movie))
                        continue;
                    if (movie == null) 
                        continue;
                    contentItem = (Content) movie;
                }

                ContentList.Add(contentItem);
            }

            GenerateList(ContentList, "liked");
        }

        public void GenerateInterestedList()
        {
            List<InterestedContent> InterestedItems = DatabaseService.GetInterestedContent(UserStore.id ?? 0);
            List<Content> ContentList = new List<Content>();

            foreach (InterestedContent InterestedItem in InterestedItems)
            {
                Content contentItem;
                if (InterestedItem.IsShow)
                {
                    if (!ApiService.GetShow(InterestedItem.Content, out Show? show))
                        continue;
                    if (show == null)
                        continue;
                    contentItem = (Content) show;
                }
                else
                {
                    if (!ApiService.GetMovie(InterestedItem.Content, out Movie? movie))
                        continue;
                    if (movie == null) 
                        continue;
                    contentItem = (Content) movie;
                }

                ContentList.Add(contentItem);
            }

            GenerateList(ContentList, "interested");
        }

        public void GenerateTrendingMovieList()
        {
            if (!ApiService.GetTrending("week", out MultiSearch? trendingItems))
                return;
            
            List<Content> contentList = new List<Content>();
            if (trendingItems.results != null && trendingItems.results.Count > 0)
            {
                foreach (MultiSearchResult trendingItem in trendingItems.results)
                {
                    contentList.Add(trendingItem);
                }
            }

            GenerateList(contentList, "recommended");
        }

        public void GenerateList(List<Content> Content, string type)
        {
            if (Content.Count == 0)
            {
                Label lbl = new Label();

                lbl.Content = "No Series or movies found of this type, make a change using our matcher.";
                lbl.Foreground = Brushes.White;
                
                if (type.Equals("liked"))
                {
                    ListItemsLiked.Items.Add(lbl);
                }
                else if (type.Equals("interested"))
                {
                    ListItemsInterested.Items.Add(lbl);
                }
                else if (type.Equals("recommended"))
                {
                    ListItemsRecommended.Items.Add(lbl);
                }
            }
            else
            {
                foreach (Content content in Content)
                {
                    Button Btn = new Button();
                    Btn.Click += ButtonDetailPage;
                    Btn.DataContext = content;
                    Grid Grd = new Grid();
                    Image Img = new Image();
                    TextBlock TextBlock = new TextBlock();
                    Grd.Children.Add(Img);
                    Grd.Children.Add(TextBlock);
                    Btn.HorizontalAlignment = HorizontalAlignment.Left;
                    Btn.VerticalAlignment = VerticalAlignment.Top;
                    Btn.Width = 130;
                    Btn.Background = (Brush) (new BrushConverter().ConvertFromString("#3cb9f4"));
                    Btn.Content = Grd;
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    if (content.posterPath != null)
                    {
                        bi.UriSource = new Uri("https://image.tmdb.org/t/p/w500/" + content.posterPath,
                            UriKind.Absolute);
                    }
                    else
                    {
                        bi.UriSource = new Uri(@"/Images/SamplePoster.png", UriKind.Relative);
                    }

                    bi.EndInit();
                    Img.Stretch = Stretch.Fill;
                    Img.Source = bi;
                    Img.Width = 130;
                    TextBlock.VerticalAlignment = VerticalAlignment.Center;
                    TextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    if (type.Equals("liked"))
                    {
                        ListItemsLiked.Items.Add(Btn);
                    }
                    else if (type.Equals("interested"))
                    {
                        ListItemsInterested.Items.Add(Btn);
                    }
                    else if (type.Equals("recommended"))
                    {
                        ListItemsRecommended.Items.Add(Btn);
                    }
                }
            }
        }
        
        public void ButtonMatcherPage(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].DataContext = new MatcherView();
        }

        public void ButtonDetailPage(object sender, RoutedEventArgs e)
        {
            Button RealButton = (Button) sender;
            var tmp = (Content) RealButton.DataContext;
            DetailViewStore.Id = tmp.id;
            DetailViewStore.MediaType = tmp.mediaType;

            Application.Current.Windows[0].DataContext = new DetailView();
        }
    }
}