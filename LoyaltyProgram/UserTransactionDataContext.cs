using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyProgram
{
    public  class UserTransactionDataContext:DbContext
    {
        public UserTransactionDataContext()
        {
        }

        public UserTransactionDataContext(DbContextOptions<UserTransactionDataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data source = database.db");
            }
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Transaction>? Transactions { get; set; }

    }
}
