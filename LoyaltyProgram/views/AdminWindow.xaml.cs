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
    /// Logika interakcji dla klasy AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private User user;
        private List<Transaction> pendingTransactions;
        private UserDataContext context;

        public AdminWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            this.context = new UserDataContext();
            InitializeLoggedUsername();
            FetchPendingTransactions();
        }

        public void LogoutUser()
        {
            UserLoginWindow userLoginWindow = new UserLoginWindow();
            userLoginWindow.Show();
            Close();
        }

        public void Approve()
        {
            Transaction transaction = (Transaction)this.pendingTransactionsGrid.SelectedItem;
            int transactionId = transaction.Id;
            Transaction transactionToApprove = this.context.Transactions.Single(transaction => transaction.Id == transactionId);
            transactionToApprove.IsVerified = true;
            this.context.SaveChanges();
            FetchPendingTransactions();
            UpdateSelectedRow();
        }

        public void Discard()
        {
            Transaction transaction = (Transaction)this.pendingTransactionsGrid.SelectedItem;
            int transactionId = transaction.Id;
            Transaction transactionToDelete = this.context.Transactions.Single(transaction => transaction.Id == transactionId);
            this.context.Remove(transactionToDelete);
            this.context.SaveChanges();
            FetchPendingTransactions();
            UpdateSelectedRow();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LogoutUser();
        }


        private void approveButton_Click(object sender, RoutedEventArgs e)
        {
            Approve();
        }

        private void discardButton_Click(object sender, RoutedEventArgs e)
        {
            Discard();
        }

        private void InitializeLoggedUsername()
        {
            var currentUser = this.user.FirstName + " " + this.user.LastName;
            LoggedUserLabel.Content = "Hello " + currentUser;
        }

        private void FetchPendingTransactions()
        {
            bool transactionsFound = this.context.Transactions.Any();
            if (transactionsFound)
            {
                this.pendingTransactions = context.Transactions.Where(transaction => transaction.IsVerified == false).ToList();
                RefreshViewSource();
            } 
        }

        private void pendingTransactionsGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            UpdateSelectedRow();        
        }

        private void RefreshViewSource()
        {
            CollectionViewSource itemCollectionViewSource;
            itemCollectionViewSource = (CollectionViewSource)(FindResource("ItemCollectionViewSource"));
            itemCollectionViewSource.Source = this.pendingTransactions;
        }

        private void UpdateSelectedRow()
        {
            if (pendingTransactionsGrid.SelectedItem != null)
            {
                UpdateFlightDetails();
            }
            else
            {
                ClearFlightDetails();
            }
        }

        private void UpdateFlightDetails()
        {
            Transaction transaction = (Transaction)pendingTransactionsGrid.SelectedItem;
            clientIdLabel.Content = transaction.UserId;
            flightNumberLabel.Content = transaction.FlightNumber;
            arrivalLabel.Content = transaction.ArrivalePlace;
            departureLabel.Content = transaction.DeparturePlace;
            pointsLabel.Content = transaction.Price / 10;
            EnableDecisionButtons();
        }

        private void ClearFlightDetails()
        {
            clientIdLabel.Content = "";
            flightNumberLabel.Content = "";
            arrivalLabel.Content = "";
            departureLabel.Content = "";
            pointsLabel.Content = "";
            DisableDecisionButtons();
        }

        private void EnableDecisionButtons()
        {
            approveButton.IsEnabled = true;
            discardButton.IsEnabled = true;
        }

        private void DisableDecisionButtons()
        {
            approveButton.IsEnabled = false;
            discardButton.IsEnabled = false;
        }
    }
}
