using MovieMatcher.Models.Api;
using MovieMatcher.Models.Database;
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
    /// Interaction logic for ButtonView.xaml
    /// </summary>
    public partial class ButtonView : UserControl
    {
        IResult Result { get; set; }

        public ButtonView()
        {
            InitializeComponent();

            if (!ApiService.GetShow(5, out var result))
                return;
            if (result == null)
                return;
            Result = result;

            bool? liked = Database.CheckForLiked(Result.id, UserInfo.Id, Result.media_type.Equals("tv"));
            bool? watched = Database.CheckForWatched(Result.id, UserInfo.Id, Result.media_type.Equals("tv"));

            switch (liked)
            {
                case true:
                    LikeButton.DataContext = true;
                    DislikeButton.DataContext = false;
                    ((Image) LikeButton.Content).Opacity = 0.5;
                    break;
                case false:
                    LikeButton.DataContext = false;
                    DislikeButton.DataContext = true;
                    ((Image) DislikeButton.Content).Opacity = 0.5;
                    break;
                case null:
                    LikeButton.DataContext = false;
                    DislikeButton.DataContext = false;
                    break;
            }

            if (watched == true)
            {
                SeenBox.IsChecked = true;
            }
        }

        private void Seen(object sender, RoutedEventArgs e)
        {
            if ((bool) DislikeButton.DataContext != false || (bool) LikeButton.DataContext != false)
            {
                Database.ChangeItem(Result.id, UserInfo.Id, true, (bool) SeenBox.IsChecked);
            }
        }

        private void Dislike_Clicked(object sender, RoutedEventArgs e)
        {
            if ((bool) LikeButton.DataContext == true)
            {
                Database.ChangeItem(Result.id, UserInfo.Id, false, (bool) SeenBox.IsChecked);

                LikeButton.DataContext = false;
                ((Image) LikeButton.Content).Opacity = 1;

                DislikeButton.DataContext = false;
                ((Image) DislikeButton.Content).Opacity = 1;
            }

            if ((bool) DislikeButton.DataContext == false && (bool) LikeButton.DataContext == false)
            {
                DislikeButton.DataContext = true;
                ((Image) DislikeButton.Content).Opacity = 0.5;
                if (Result.media_type == "tv")
                {
                    Database.InsertItem(Result.id, UserInfo.Id, false, (bool) SeenBox.IsChecked, true);
                }
                else
                {
                    Database.InsertItem(Result.id, UserInfo.Id, false, (bool) SeenBox.IsChecked, false);
                }
            }
        }

        private void Like_Clicked(object sender, RoutedEventArgs e)
        {
            if ((bool) DislikeButton.DataContext == true)
            {
                Database.ChangeItem(Result.id, UserInfo.Id, true, (bool) SeenBox.IsChecked);

                DislikeButton.DataContext = false;
                ((Image) DislikeButton.Content).Opacity = 1;

                LikeButton.DataContext = true;
                ((Image) LikeButton.Content).Opacity = 0.5;
            }
            else if ((bool) LikeButton.DataContext == false && (bool) DislikeButton.DataContext == false)
            {
                LikeButton.DataContext = true;
                ((Image) LikeButton.Content).Opacity = 0.5;
                if (Result.media_type == "tv")
                {
                    Database.InsertItem(Result.id, UserInfo.Id, true, (bool) SeenBox.IsChecked, true);
                }
                else
                {
                    Database.InsertItem(Result.id, UserInfo.Id, true, (bool) SeenBox.IsChecked, false);
                }
            }
        }
    }
}