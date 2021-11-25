﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MovieMatcher
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            Database D = new Database();
            var result = D.CreateUser(Username.Text.ToString(), Password.Password.ToString(), Email.Text.ToString(), Age.Text.ToString());
            if (String.IsNullOrEmpty(result))
            {
                MessageBox.Show($"FOUT1 {result}");
            }

            throw new NotImplementedException();
        }
        
        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
