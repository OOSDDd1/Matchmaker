using System.ComponentModel;

namespace MovieMatcher.Models
{
    
    
    
    public class Login : INotifyPropertyChanged
    {
        // private string _userName;
        //
        // public string UserName
        // {
        //     get => _userName;
        //     set
        //     {
        //         _userName = value;
        //         OnPropertyChanged(nameof(UserName));
        //     }
        // }


        public event PropertyChangedEventHandler? PropertyChanged;  
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }  
    }
}