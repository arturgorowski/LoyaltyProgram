using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyProgram
{
    public class User
    {
        public int id;
        public string username;
        public string password;
        public string firstName;
        public string lastName;
        public string role;

        public User(string _username, string _password, string _firstName, string _lastName, string _role)
        {
            username = _username;
            password = _password;
            firstName = _firstName;
            lastName = _lastName;
            role = _role;
        }

        public User(int _id, string _username, string _password, string _firstName, string _lastName, string _role)
        {
            id = _id;
            username = _username;
            password = _password;
            firstName = _firstName;
            lastName = _lastName;
            role = _role;
        }
    }
}
