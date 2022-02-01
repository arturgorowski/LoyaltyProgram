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
    /// Logika interakcji dla klasy UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private User user;

        public UserWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            InitializeLoggedUsername(this.user);
        }

        public void LogoutUser()
        {
            UserLoginWindow userLoginWindow = new UserLoginWindow();
            userLoginWindow.Show();
            Close();
        }

        public void AddNewFlight()
        {
            NewFlightForm newFlightForm = new NewFlightForm(this.user);
            newFlightForm.Show();
            Close();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LogoutUser();
        }


        private void newFlightButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewFlight();
        }

        private void InitializeLoggedUsername(User user)
        {
            var currentUser = user.FirstName + " " + user.LastName;
            LoggedUserTextBlock.Text = currentUser;
        }
    }
}
