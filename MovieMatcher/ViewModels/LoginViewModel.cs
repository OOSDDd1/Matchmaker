using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using MovieMatcher.Models.Api;

namespace MovieMatcher.ViewModels
{
    public class LoginViewModel
    {
        private ICommand loginCommand;  
        public ICommand LoginCommand  
        {  
            get  
            {  
                if (loginCommand == null)  
                    loginCommand = new LoginUpdater();  
                return loginCommand;  
            }  
            set  
            {  
                loginCommand = value;  
            }  
        }  
  
        private class LoginUpdater : ICommand  
        {  
            #region ICommand Members  
  
            public bool CanExecute(object? parameter)  
            {  
                return true;  
            }  
  
            public event EventHandler? CanExecuteChanged;  
  
            public void Execute(object parameter)
            {
                MessageBox.Show(parameter.ToString());
                // Login
                var values = (object[])parameter;
                var userName = (string)values[0];
                var password = (string)values[1];
            }  
 
            #endregion  
        }  
    }
}