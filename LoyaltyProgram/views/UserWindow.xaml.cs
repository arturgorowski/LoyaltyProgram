using LoyaltyProgram.service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private List<Transaction> userTransactions;
        private UserTransactionService userTransactionService;

        public UserWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            this.userTransactionService = new UserTransactionService();
            InitializeLoggedUsername();
            FetchUserTransactions();
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

        public void DeleteFlight()
        {
            Transaction transaction = (Transaction)this.transactionsGrid.SelectedItem;
            userTransactionService.deleteTransaction(this.userTransactions, transaction);
            FetchUserTransactions();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LogoutUser();
        }


        private void newFlightButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewFlight();
        }

        private void deleteFlightButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteFlight();
        }

        private void InitializeLoggedUsername()
        {   
            var currentUser = this.user.FirstName + " " + this.user.LastName;
            LoggedUserLabel.Content = "Hello " + currentUser;
        }

        private void FetchUserTransactions()
        {
            this.userTransactions = userTransactionService.getUserTransactions(this.user);
            RefreshViewSource();
            RefreshPointsLabel();
        }

        private void RefreshPointsLabel()
        {
            double points = this.userTransactionService.CountPoints(this.userTransactions);
            PointsLabel.Content = "You have " + points + " points";
        }

        private void transactionsGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Transaction transaction = (Transaction)transactionsGrid.SelectedItem;
            UpdateFlightDetails(transaction);
            deleteFlightButton.IsEnabled = (bool)!transaction.IsVerified;
        }

        private void RefreshViewSource()
        {
            CollectionViewSource itemCollectionViewSource;
            itemCollectionViewSource = (CollectionViewSource)(FindResource("ItemCollectionViewSource"));
            itemCollectionViewSource.Source = this.userTransactions;
        }

        private void UpdateFlightDetails(Transaction transaction)
        {
            flightNumberLabel.Content = transaction.FlightNumber;
            arrivalLabel.Content = transaction.ArrivalePlace;
            departureLabel.Content = transaction.DeparturePlace;
            pointsLabel.Content = transaction.Price / 10;
            statusLabel.Content = (bool)transaction.IsVerified ? "Granted" : "Verification";
            statusLabel.Foreground = (bool)transaction.IsVerified ? Brushes.Green: Brushes.Red;
        }
    }
}
