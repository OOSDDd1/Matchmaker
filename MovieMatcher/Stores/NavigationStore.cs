using System;
using System.Windows.Documents;
using MovieMatcher.ViewModels;

namespace MovieMatcher.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;
        
        private ViewModelBase _CurrentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _CurrentViewModel;
            set
            {
                _CurrentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}