using LoyaltyProgram;
using LoyaltyProgram.service;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class PendingTransactionServiceTests
    {
        [TestInitialize]
        public void Initialize()
        {
            var connection = new SqliteConnection("Data source = database.db");
            connection.Open();

            var options = new DbContextOptionsBuilder<UserTransactionDataContext>().UseSqlite(connection).Options;

            using (var context = new UserTransactionDataContext(options))
            {
                
                context.Users.Add(new User { Id = 72, Username = "1", Password = "1", FirstName = "1", LastName = "1", Role = "user" });
                context.SaveChanges();

                context.Transactions.Add(new Transaction { Id = 73, UserId = 72, DeparturePlace = "1", ArrivalePlace = "1", FlightNumber = "1", Price = 100.0, IsVerified = false });
                context.SaveChanges();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            var connection = new SqliteConnection("Data source = database.db");
            connection.Open();

            var options = new DbContextOptionsBuilder<UserTransactionDataContext>().UseSqlite(connection).Options;

            using (var context = new UserTransactionDataContext(options))
            {
                context.Users.Remove(new User { Id = 72, Username = "1", Password = "1", FirstName = "1", LastName = "1", Role = "user" });

                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetPendingTransactionsTest()
        {
            var connection = new SqliteConnection("Data source = database.db");
            connection.Open();

            var options = new DbContextOptionsBuilder<UserTransactionDataContext>().UseSqlite(connection).Options;


            using (var context = new UserTransactionDataContext(options))
            {
                PendingTransactionService pendingTransactionService = new PendingTransactionService();
                var current = pendingTransactionService.GetPendingTransactions();
                Assert.IsNotNull(current.Find(i => i.Id == 73));

                context.Transactions.Remove(new Transaction { Id = 73, UserId = 72, DeparturePlace = "1", ArrivalePlace = "1", FlightNumber = "1", Price = 100.0, IsVerified = false });
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void ApproveTransactionTest()
        {
            var connection = new SqliteConnection("Data source = database.db");
            connection.Open();

            var options = new DbContextOptionsBuilder<UserTransactionDataContext>().UseSqlite(connection).Options;


            using (var context = new UserTransactionDataContext(options))
            {
                PendingTransactionService pendingTransactionService = new PendingTransactionService();
                pendingTransactionService.ApproveTransaction(new Transaction { Id = 73, UserId = 72, DeparturePlace = "1", ArrivalePlace = "1", FlightNumber = "1", Price = 100.0, IsVerified = false });

                var current = context.Database.ExecuteSqlInterpolated($"DELETE FROM [Transactions] WHERE Id = 73 and IsVerified = true");
                Assert.IsTrue(current > 0);
                context.Database.ExecuteSqlInterpolated($"DELETE FROM [Transactions] WHERE Id = 73");
            }
        }

        [TestMethod]
        public void DiscardTransactionTest()
        {
            var connection = new SqliteConnection("Data source = database.db");
            connection.Open();

            var options = new DbContextOptionsBuilder<UserTransactionDataContext>().UseSqlite(connection).Options;


            using (var context = new UserTransactionDataContext(options))
            {
                PendingTransactionService pendingTransactionService = new PendingTransactionService();
                pendingTransactionService.DiscardTransaction(new Transaction { Id = 73, UserId = 72, DeparturePlace = "1", ArrivalePlace = "1", FlightNumber = "1", Price = 100.0, IsVerified = false });

                var current = context.Database.ExecuteSqlInterpolated($"DELETE FROM [Transactions] WHERE Id = 73");
                Assert.IsTrue(current == 0);
            }
        }
    }
}