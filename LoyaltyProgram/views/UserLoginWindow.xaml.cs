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
        public UserLoginWindow()
        {
            InitializeComponent();
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
            var Username = UsernameText.Text;
            var Password = PasswordText.Password;

            if (Username == "" || Password == "")
            {
                MessageBox.Show("Username and Password are required!");
                return;
            }

            using (UserDataContext context = new UserDataContext())
            {
                bool userFound = context.Users.Any(user => user.Username == Username && user.Password == Password);

                if (userFound)
                {
                    User loggedUser = context.Users.FirstOrDefault(user => user.Username == Username);

                    if (loggedUser.Role == "admin")
                    {
                        GrantAdminAccess(loggedUser);
                    }
                    else
                    {
                        GrantUserAccess(loggedUser);
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("User Not Found!");
                }
            }
        }
    }
}
