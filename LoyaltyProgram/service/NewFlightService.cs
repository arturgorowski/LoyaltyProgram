using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyProgram.service
{
    public class NewFlightService
    {
        private UserTransactionDataContext context;

        public NewFlightService()
        {
            context = new UserTransactionDataContext();
        }

        public bool ValidateFields(string departurePlace, string arrivalPlace, string flightNumber, string price)
        {
            return (departurePlace != "" && arrivalPlace != "" && flightNumber != "" && price != "");
        }

        public bool ValidatePrice(string price)
        {
            return (double.TryParse(price, out _));
        }

        public void AddNewTransaction(int userId, string departurePlace, string arrivalPlace, string flightNumber, string price)
        {
            Transaction transaction = new Transaction();
            transaction.DeparturePlace = departurePlace;
            transaction.ArrivalePlace = arrivalPlace;
            transaction.FlightNumber = flightNumber;
            transaction.Price = Double.Parse(price);
            transaction.UserId = userId;
            transaction.IsVerified = false;

            context.Add<Transaction>(transaction);
            context.SaveChanges();
        }
    }
}
