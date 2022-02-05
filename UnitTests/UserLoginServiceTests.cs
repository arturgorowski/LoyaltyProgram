using LoyaltyProgram;
using LoyaltyProgram.service;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UserLoginServiceTests
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
        public void EmptyUsernameTest()
        {
            UserLoginService userLoginService = new UserLoginService();
            var currentState = userLoginService.ValidateFields("", "something");
            Assert.IsFalse(currentState); 
        }

        [TestMethod]
        public void EmptyPasswordTest()
        {
            UserLoginService userLoginService = new UserLoginService();
            var currentState = userLoginService.ValidateFields("something", "");
            Assert.IsFalse(currentState);
        }

        [TestMethod]
        public void ValidateFieldsTest()
        {
            UserLoginService userLoginService = new UserLoginService();
            var currentState = userLoginService.ValidateFields("something", "something");
            Assert.IsTrue(currentState);
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
    }
}