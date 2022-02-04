using LoyaltyProgram.service;
using System;
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

namespace LoyaltyProgram.views
{
    /// <summary>
    /// Logika interakcji dla klasy UserLoginWindow.xaml
    /// </summary>
    public partial class UserLoginWindow : Window
    {

        private UserLoginService userLoginService;
        public UserLoginWindow()
        {
            InitializeComponent();
            userLoginService = new UserLoginService();
        }

        public void GrantUserAccess(User user)
        {
            UserWindow userWindow = new UserWindow(user);
            userWindow.Show();
        }

        public void GrantAdminAccess(User user)
        {
            AdminWindow adminWindow = new AdminWindow(user);
            adminWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameText.Text;
            var password = PasswordText.Password;

            if (!userLoginService.ValidateFields(username, password))
            {
                MessageBox.Show("Username and Password are required!");
                return;
            }

            User user = userLoginService.GetUser(username, password);
            if (user == null)
            {
                MessageBox.Show("User not found");
            } 
            else if (user.Role.Equals("admin"))
            {
                GrantAdminAccess(user);
            } 
            else
            {
                GrantUserAccess(user);
            }
            Close();
        }
    }
}
