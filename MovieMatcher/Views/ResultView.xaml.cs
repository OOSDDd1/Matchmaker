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

            GenerateLikedList();
            GenerateInterestedList();
            GenerateTrendingMovieList();

            void GenerateLikedList()
            {
                List<dynamic> LikedItems = Database.GetLikedContent(Models.Database.UserInfo.Id);
                List<Content> ContentList = new List<Content>();

                foreach (dynamic LikedItem in LikedItems)
                {
                    Content contentItem;
                    if (LikedItem.isShow == 1)
                    {
                        contentItem = Api.GetShow(LikedItem.content);
                    }
                    else
                    {
                        contentItem = Api.GetMovie(LikedItem.content);
                    }
                    ContentList.Add(contentItem);
                }
                GenerateList(ContentList, "liked");
            }

            void GenerateInterestedList()
            {
                List<dynamic> InterestedItems = Database.GetInterestedContent(Models.Database.UserInfo.Id);
                List<Content> ContentList = new List<Content>();

                foreach (dynamic InterestedItem in InterestedItems)
                {
                    Content contentItem;
                    if (InterestedItem.isShow == 1)
                    {
                        contentItem = Api.GetShow(InterestedItem.content);
                    }
                    else
                    {
                        contentItem = Api.GetMovie(InterestedItem.content);
                    }
                    ContentList.Add(contentItem);
                }
                GenerateList(ContentList, "interested");
            }

            void GenerateTrendingMovieList()
            {
                MultiSearch TrendingItems = Api.GetTrending("week");
                List<Content> ContentList = new List<Content>();
                if (TrendingItems.results != null && TrendingItems.results.Count > 0)
                {
                    foreach (MultiSearchResult TrendingItem in TrendingItems.results)
                    {
                        if (TrendingItem.media_type.Equals("movie"))
                        {
                            ContentList.Add(new Movie(TrendingItem.poster_path));
                        } else if (TrendingItem.media_type.Equals("tv"))
                        {
                            ContentList.Add(new Show(TrendingItem.poster_path));
                        }
                    }
                }
                GenerateList(ContentList, "recommended");
            }

            void GenerateList(List<Content> Content, string type)
            {
                foreach (Content content in Content)
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
                    bi.UriSource = new Uri("https://image.tmdb.org/t/p/w500/" + content.poster_path, UriKind.Absolute);
                    bi.EndInit();
                    Image.Stretch = Stretch.Fill;
                    Image.Source = bi;
                    Image.Width = 130;
                    TextBlock.VerticalAlignment = VerticalAlignment.Center;
                    TextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    if (type.Equals("liked"))
                    {
                        ListItemsLiked.Items.Add(Grid);
                    }
                    else if (type.Equals("interested"))
                    {
                        ListItemsInterested.Items.Add(Grid);
                    }
                    else if (type.Equals("recommended"))
                    {
                        ListItemsRecommended.Items.Add(Grid);
                    }
                }
            }
        }
    }
}
