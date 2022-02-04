using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyProgram.service
{
    public class PendingTransactionService
    {
        private UserTransactionDataContext context;

        public PendingTransactionService()
        {
            context = new UserTransactionDataContext();
        }

        public List<Transaction> GetPendingTransactions()
        {
            List<Transaction> pendingTransactions = new List<Transaction>();
            bool transactionsFound = context.Transactions.Any();

            if (transactionsFound)
            {
                pendingTransactions = context.Transactions.Where(transaction => transaction.IsVerified == false).ToList();
            }
            return pendingTransactions;
        }

        public void ApproveTransaction(Transaction transaction)
        {
            int transactionId = transaction.Id;
            Transaction transactionToApprove = this.context.Transactions.Single(transaction => transaction.Id == transactionId);
            transactionToApprove.IsVerified = true;
            this.context.SaveChanges();
        }

        public void DiscardTransaction(Transaction transaction)
        {
            int transactionId = transaction.Id;
            Transaction transactionToDelete = this.context.Transactions.Single(transaction => transaction.Id == transactionId);
            this.context.Remove(transactionToDelete);
            this.context.SaveChanges();
        }
    }
}
