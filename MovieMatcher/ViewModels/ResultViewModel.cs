
using System.Windows.Input;
using MovieMatcher.Commands;
using MovieMatcher.Stores;

namespace MovieMatcher.ViewModels
{
    public class ResultViewModel : ViewModelBase
    {
        public ICommand NavigateDetailCommand { get; }

        public ResultViewModel(NavigationStore navigationStore)
        {
            NavigateDetailCommand = new NavigateCommand<DetailViewModel>(navigationStore, () => new DetailViewModel(navigationStore));
        }
    }
}
