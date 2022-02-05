using LoyaltyProgram;
using LoyaltyProgram.service;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class NewFlightServiceTests
    {
        [TestInitialize]
        public void Initialize()
        {
            var connection = new SqliteConnection("Data source = database.db");
            connection.Open();

            var options = new DbContextOptionsBuilder<UserTransactionDataContext>().UseSqlite(connection).Options;

            using (var context = new UserTransactionDataContext(options))
            {
                context.Users.Add(new User { Id = 70, Username = "1", Password = "1", FirstName = "1", LastName = "1", Role = "user" });

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
                context.Users.Remove(new User { Id = 70, Username = "1", Password = "1", FirstName = "1", LastName = "1", Role = "user" });

                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetUserTest()
        {
            var connection = new SqliteConnection("Data source = database.db");
            connection.Open();

            var options = new DbContextOptionsBuilder<UserTransactionDataContext>().UseSqlite(connection).Options;


            using (var context = new UserTransactionDataContext(options))
            {
                UserLoginService userLoginService = new UserLoginService();
                var current = userLoginService.GetUser("1", "1");
                var expected = new User { Id = 70, Username = "1", Password = "1", FirstName = "1", LastName = "1", Role = "user" };
                Assert.AreEqual(expected.Id, current.Id);
                Assert.AreEqual(expected.Username, current.Username);
                Assert.AreEqual(expected.Password, current.Password);
                Assert.AreEqual(expected.FirstName, current.FirstName);
                Assert.AreEqual(expected.LastName, current.LastName);
                Assert.AreEqual(expected.Role, current.Role);
            }
        }

        [TestMethod]
        public void ValidateFieldsPassTest()
        {
            NewFlightService newFlightService = new NewFlightService();
            var current = newFlightService.ValidateFields("something", "something", "something", "something");
            Assert.IsTrue(current);
        }

        [TestMethod]
        public void ValidateFieldsDeparturePlaceEmptyTest()
        {
            NewFlightService newFlightService = new NewFlightService();
            var current = newFlightService.ValidateFields("", "something", "something", "something");
            Assert.IsFalse(current);
        }

        [TestMethod]
        public void ValidateFieldsArrivalPlaceEmptyTest()
        {
            NewFlightService newFlightService = new NewFlightService();
            var current = newFlightService.ValidateFields("something", "", "something", "something");
            Assert.IsFalse(current);
        }

        [TestMethod]
        public void ValidateFieldsPriceEmptyTest()
        {
            NewFlightService newFlightService = new NewFlightService();
            var current = newFlightService.ValidateFields("something", "something", "something", "");
            Assert.IsFalse(current);
        }

        [TestMethod]
        public void ValidatePricePassTest()
        {
            NewFlightService newFlightService = new NewFlightService();
            var current = newFlightService.ValidatePrice("100");
            Assert.IsTrue(current);
        }

        [TestMethod]
        public void ValidatePriceIncorrectTypeTest()
        {
            NewFlightService newFlightService = new NewFlightService();
            var current = newFlightService.ValidatePrice("100,something");
            Assert.IsFalse(current);
        }

        [TestMethod]
        public void AddNewTransactionTest()
        {
            NewFlightService newFlightService = new NewFlightService();
            var connection = new SqliteConnection("Data source = database.db");
            connection.Open();

            var options = new DbContextOptionsBuilder<UserTransactionDataContext>().UseSqlite(connection).Options;
            newFlightService.AddNewTransaction(70, "1", "1", "1", "1");
            using (var context = new UserTransactionDataContext(options))
            {

                var current = context.Database.ExecuteSqlInterpolated($"DELETE FROM [Transactions] WHERE UserId = 70");
                Assert.IsTrue(current>0);
            }

        }
    }
}