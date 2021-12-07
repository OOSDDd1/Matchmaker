
using System.Windows.Input;
using MovieMatcher.Commands;
using MovieMatcher.Services;
using MovieMatcher.Stores;

namespace MovieMatcher.ViewModels
{
    public class ResultViewModel : ViewModelBase
    {
        public ICommand NavigateDetailCommand { get; }

        public ResultViewModel(NavigationStore navigationStore)
        {
            NavigateDetailCommand = new NavigateCommand<DetailViewModel>(
                new NavigationService<DetailViewModel>(
                    navigationStore,() => new DetailViewModel(12, navigationStore)
                    )
                );
        }
    }
}
