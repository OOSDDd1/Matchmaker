using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Models.Api;
using Models.Api.Components;
using Models.Database;
using MovieMatcher.ViewModels;
using Services;
using Stores;

namespace MovieMatcher.Views
{
    public partial class ResultView : UserControl
    {
        public ResultView()
        {
            InitializeComponent();

            GenerateLikedList();
            GenerateInterestedList();
            GenerateTrendingMovieList();
        }

        public void GenerateLikedList()
        {
            List<dynamic> LikedItems = DatabaseService.GetLikedContent(UserInfo.Id);
            List<Content> ContentList = new List<Content>();

            foreach (dynamic LikedItem in LikedItems)
            {
                Content contentItem;
                if (LikedItem.isShow == 1)
                {
                    if (!ApiService.GetShow(LikedItem.content, out Show? show))
                        continue;
                    if (show == null)
                        continue;
                    contentItem = (Content) show;
                }
                else
                {
                    if (!ApiService.GetMovie(LikedItem.content, out Movie? movie))
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
            List<dynamic> InterestedItems = DatabaseService.GetInterestedContent(Models.Database.UserInfo.Id);
            List<Content> ContentList = new List<Content>();

            foreach (dynamic InterestedItem in InterestedItems)
            {
                Content contentItem;
                if (InterestedItem.isShow == 1)
                {
                    if (!ApiService.GetShow(InterestedItem.content, out Show? show))
                        continue;
                    if (show == null)
                        continue;
                    contentItem = (Content) show;
                }
                else
                {
                    if (!ApiService.GetMovie(InterestedItem.content, out Movie? movie))
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
                Button btn = new Button();
                Label lbl = new Label();

                btn.Background = null;
                btn.Click += ButtonMatcherPage;

                lbl.Content = "No Series or movies found of this type, make a change using our matcher";
                lbl.Foreground = Brushes.White;

                btn.Content = lbl;
                if (type.Equals("liked"))
                {
                    ListItemsLiked.Items.Add(btn);
                }
                else if (type.Equals("interested"))
                {
                    ListItemsInterested.Items.Add(btn);
                }
                else if (type.Equals("recommended"))
                {
                    ListItemsRecommended.Items.Add(btn);
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
                    if (content.poster_path != null)
                    {
                        bi.UriSource = new Uri("https://image.tmdb.org/t/p/w500/" + content.poster_path,
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
            Application.Current.Windows[0].DataContext = new MatcherViewModel();
        }

        public void ButtonDetailPage(object sender, RoutedEventArgs e)
        {
            Button RealButton = (Button) sender;
            var tmp = (Content) RealButton.DataContext;
            DetailViewStore.Id = tmp.id;
            DetailViewStore.MediaType = tmp.media_type;

            Application.Current.Windows[0].DataContext = new DetailViewModel();
        }
    }
}