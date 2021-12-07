using System;
using MovieMatcher.Stores;
using MovieMatcher.ViewModels;

namespace MovieMatcher.Services
{
    public class NavigationService<TViewModel>
        where TViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}