using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MovieMatcher.Models.Api;
using MovieMatcher.Models.Database;

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
                // Login
                var values = (object[]) parameter;
                var userName = (string) values[0];
                var password = values[1] as PasswordBox;
                var window = values[2] as Window;
                window.Close();
                
                MessageBox.Show(password.Password);

                if (Database.GetUserInfo(userName))
                {
                    MessageBox.Show("user");
                    if (UserInfo.Password != null && PasswordHasher.Verify(password.Password, UserInfo.Password))
                    {
                        MessageBox.Show("yeet");

                        // doorverwijzig naar het homescherm
                        var appMainWindow = new MainWindow();
                        appMainWindow.Show();
                        
                    }

                }
            }

            #endregion  
        }  
    }
}