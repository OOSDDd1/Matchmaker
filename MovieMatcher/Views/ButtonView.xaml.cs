using System.Windows;
using System.Windows.Controls;
using Models.Api;
using Services;
using Stores;

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

            bool? liked = DatabaseService.CheckForLiked(Result.id, UserStore.id ?? 0, Result.mediaType.Equals("tv"));
            bool? watched = DatabaseService.CheckForWatched(Result.id, UserStore.id ?? 0, Result.mediaType.Equals("tv"));

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
                DatabaseService.ChangeItem(Result.id, UserStore.id ?? 0, true, (bool) SeenBox.IsChecked);
            }
        }

        private void Dislike_Clicked(object sender, RoutedEventArgs e)
        {
            if ((bool) LikeButton.DataContext == true)
            {
                DatabaseService.ChangeItem(Result.id, UserStore.id ?? 0, false, (bool) SeenBox.IsChecked);

                LikeButton.DataContext = false;
                ((Image) LikeButton.Content).Opacity = 1;

                DislikeButton.DataContext = false;
                ((Image) DislikeButton.Content).Opacity = 1;
            }

            if ((bool) DislikeButton.DataContext == false && (bool) LikeButton.DataContext == false)
            {
                DislikeButton.DataContext = true;
                ((Image) DislikeButton.Content).Opacity = 0.5;
                if (Result.mediaType == "tv")
                {
                    DatabaseService.InsertItem(Result.id, UserStore.id ?? 0, false, (bool) SeenBox.IsChecked, true);
                }
                else
                {
                    DatabaseService.InsertItem(Result.id, UserStore.id ?? 0, false, (bool) SeenBox.IsChecked, false);
                }
            }
        }

        private void Like_Clicked(object sender, RoutedEventArgs e)
        {
            if ((bool) DislikeButton.DataContext == true)
            {
                DatabaseService.ChangeItem(Result.id, UserStore.id ?? 0, true, (bool) SeenBox.IsChecked);

                DislikeButton.DataContext = false;
                ((Image) DislikeButton.Content).Opacity = 1;

                LikeButton.DataContext = true;
                ((Image) LikeButton.Content).Opacity = 0.5;
            }
            else if ((bool) LikeButton.DataContext == false && (bool) DislikeButton.DataContext == false)
            {
                LikeButton.DataContext = true;
                ((Image) LikeButton.Content).Opacity = 0.5;
                if (Result.mediaType == "tv")
                {
                    DatabaseService.InsertItem(Result.id, UserStore.id ?? 0, true, (bool) SeenBox.IsChecked, true);
                }
                else
                {
                    DatabaseService.InsertItem(Result.id, UserStore.id ?? 0, true, (bool) SeenBox.IsChecked, false);
                }
            }
        }
    }
}