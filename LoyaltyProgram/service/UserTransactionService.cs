using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyProgram.service
{
    public class UserTransactionService
    {
        private UserTransactionDataContext context;

        public UserTransactionService()
        {
            context = new UserTransactionDataContext();
        }

        public void DeleteTransaction(List<Transaction> userTransactions, Transaction transaction)
        {
            int transactionId = transaction.Id;
            Transaction transactionToDelete = this.context.Transactions.Single(transaction => transaction.Id == transactionId);
            context.Remove(transactionToDelete);
            context.SaveChanges();
        }

        public List<Transaction> GetUserTransactions(User user)
        {
            List<Transaction> userTransactions = new List<Transaction>();
            bool transactionsFound = context.Transactions.Any();
            
            if (transactionsFound)
            {
                userTransactions = context.Transactions.Where(transaction => transaction.UserId == user.Id).ToList();
            }
            return userTransactions;
        }

        public double CountPoints(List<Transaction> userTransactions)
        {
            return (double)(userTransactions
                .Where(transaction => transaction.IsVerified == true)
                .Sum(transaction => transaction.Price) / 10);
        }
    }
}
