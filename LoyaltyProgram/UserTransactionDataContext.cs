﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyProgram
{
    public  class UserTransactionDataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source = database.db");
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Transaction>? Transactions { get; set; }

    }
}