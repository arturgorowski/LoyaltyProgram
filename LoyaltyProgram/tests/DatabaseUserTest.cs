using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LoyaltyProgram.tests
{
    public class DatabaseUserTest
    {

        private DatabaseConnection _databaseConnection;

        [SetUp]
        public void Setup()
        {
            _databaseConnection = new DatabaseConnection();
        }


        [Test]
        public void create_NewUser()
        {
            var user = new User("username", "password", "firstName", "lastName", "role");
            Assert.Greater(_databaseConnection.addUser(user), 0);
            var users = _databaseConnection.getUsers();
            _databaseConnection.deleteUser(users[users.Count - 1].id);
        }

        [Test]
        public void get_UsersList()
        {
            var user = new User("username", "password", "firstName", "lastName", "role");
            _databaseConnection.addUser(user);
            var users = _databaseConnection.getUsers();
            Assert.Greater(users.Count, 0);
            _databaseConnection.deleteUser(users[users.Count - 1].id);
        }


        [Test]
        public void edit_User()
        {
            var user = new User("username", "password", "firstName", "lastName", "role");
            _databaseConnection.addUser(user);
            var users = _databaseConnection.getUsers();
            var lastUser = users[users.Count - 1];
            lastUser.firstName = "updatedFirstName";
            Assert.Greater(_databaseConnection.updateUser(lastUser), 0);
            _databaseConnection.deleteUser(lastUser.id);
        }


        [Test]
        public void delete_User()
        {
            var user = new User("username", "password", "firstName", "lastName", "role");
            _databaseConnection.addUser(user);
            var users = _databaseConnection.getUsers();
            Assert.Greater(_databaseConnection.deleteUser(users[users.Count - 1].id), 0);
        }


    }
}
