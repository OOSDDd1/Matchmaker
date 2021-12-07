using System.Windows.Input;
using MovieMatcher.Commands;
using MovieMatcher.Services;
using MovieMatcher.Stores;

namespace MovieMatcher.ViewModels
{
    public class DetailViewModel : ViewModelBase
    {
        private readonly int _id;
        public int Id => _id;
        public ICommand NavigateHomeCommand { get; }

        public DetailViewModel(int id, NavigationStore navigationStore)
        {
            _id = id;
            NavigateHomeCommand = new NavigateCommand<ResultViewModel>(
                new NavigationService<ResultViewModel>(
                    navigationStore, () => new ResultViewModel(navigationStore)
                )
            );
        }
    }
}