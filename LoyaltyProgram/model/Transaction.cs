using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyProgram
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? DeparturePlace { get; set; }
        public string? ArrivalePlace { get; set; }
        public string? FlightNumber { get; set; }
        public double? Price { get; set; }
        public bool? IsVerified { get; set; }
    }
}
