using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyProgram
{
    public class DatabaseConnection
    {
        string _connectionString;

        public DatabaseConnection()
        {
            var _databaseLocation = "data.db";
            _databaseLocation = File.Exists(_databaseLocation) ? _databaseLocation : String.Format("../../../{0}", _databaseLocation);
            _connectionString = String.Format("Data Source={0}", _databaseLocation);
        }

        public int addUser(User user)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    INSERT INTO USER ( username, password, firstName, lastName, role )
                    VALUES( $username, $password, $firstName, $lastName, $role )
                ";

                command.Parameters.AddWithValue("$username", user.username);
                command.Parameters.AddWithValue("$password", user.password);
                command.Parameters.AddWithValue("$firstName", user.firstName);
                command.Parameters.AddWithValue("$lastName", user.lastName);
                command.Parameters.AddWithValue("$role", user.role);

                try
                {
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public int updateUser(User user)
        {
            if (user.id == 0)
                throw new Exception("User id cannot be 0");
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    UPDATE USER 
                    SET username = $username,
                        password = $password, 
                        firstName = $firstName, 
                        lastName = $lastName
                        role = $role
                    WHERE id = $id
                ";

                command.Parameters.AddWithValue("$username", user.username);
                command.Parameters.AddWithValue("$password", user.password);
                command.Parameters.AddWithValue("$firstName", user.firstName);
                command.Parameters.AddWithValue("$lastName", user.lastName);
                command.Parameters.AddWithValue("$role", user.role);
                command.Parameters.AddWithValue("$id", user.id);

                try
                {
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public int deleteUser(int id)
        {
            if (id == 0)
                throw new Exception("User id cannot be 0");

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    DELETE FROM USER 
                    WHERE $id = id
                ";

                command.Parameters.AddWithValue("$id", id);

                try
                {
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<User> getUsers()
        {
            List<User> users = new List<User>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT * FROM USER
                ";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var username = reader.GetString(1);
                        var password = reader.GetString(2);
                        var firstName = reader.GetString(3);
                        var lastName = reader.GetString(4);
                        var role = reader.GetString(5);
                        users.Add(new User(id, username, password, firstName, lastName, role));
                    }
                }
            }
            return users;
        }

        public User getUser(int _id)
        {
            User user = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT * FROM USER
                    WHERE id = $id
                ";
                command.Parameters.AddWithValue("$id", _id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var username = reader.GetString(1);
                        var password = reader.GetString(2);
                        var firstName = reader.GetString(3);
                        var lastName = reader.GetString(4);
                        var role = reader.GetString(5);
                        user = new User(id, username, password, firstName, lastName, role);
                    }
                }
            }
            return user;
        }
    }
}
