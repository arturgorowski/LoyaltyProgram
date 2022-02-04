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
    /// Logika interakcji dla klasy NewFlightForm.xaml
    /// </summary>
    public partial class NewFlightForm : Window
    {
        private User user;
        private NewFlightService newFlightService;
        public NewFlightForm(User user)
        {
            InitializeComponent();
            this.newFlightService = new NewFlightService();
            this.user = user;
        }

        public void BackToUserWindow()
        {
            UserWindow userWindow = new UserWindow(this.user);
            userWindow.Show();
            Close();
        }

        private void AddToVerify_Click(object sender, RoutedEventArgs e)
        {
            var departurePlace = DeparturePlaceTextBox.Text;
            var arrivalPlace = ArrivalPlaceTextBox.Text;
            var flightNumber = FlightNumberTextBox.Text;
            var price = PriceTextBox.Text;

            if (!newFlightService.ValidateFields(departurePlace, arrivalPlace, flightNumber, price))
            {
                MessageBox.Show("All fields are required!");
            } 
            else
            {
                if (!newFlightService.ValidatePrice(price))
                {
                    MessageBox.Show("The price is in the wrong format!");
                } 
                else
                {
                    newFlightService.AddNewTransaction(this.user.Id, departurePlace, arrivalPlace, flightNumber, price);
                    BackToUserWindow();
                }
            }
        }
    }
}
