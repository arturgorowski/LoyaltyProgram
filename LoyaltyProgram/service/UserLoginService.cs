using System;
using System.Linq;

namespace LoyaltyProgram.service
{
    public class UserLoginService
    {
        private UserTransactionDataContext context;

        public UserLoginService()
        {
            this.context = new UserTransactionDataContext();
        }

        public bool ValidateFields(string username, string password)
        {
            return (username != "" && password != "");
        }

        public User? GetUser(string username, string password)
        {
            return context.Users.FirstOrDefault(user => user.Username == username);
        }
    }
}