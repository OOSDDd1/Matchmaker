using System.Windows.Input;
using MovieMatcher.Commands;
using MovieMatcher.Stores;

namespace MovieMatcher.ViewModels
{
    public class DetailViewModel : ViewModelBase
    { 
        public ICommand NavigateHomeCommand { get; }

        public DetailViewModel(NavigationStore navigationStore)
        {
            NavigateHomeCommand = new NavigateCommand<ResultViewModel>(navigationStore, () => new ResultViewModel(navigationStore));
        }
    }
}