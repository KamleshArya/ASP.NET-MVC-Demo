using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MvcAuction.Models
{
    public class AuctionsDataContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }

        //Drop and Create database when models is modified-not recommended for production sites
        //switch code to entity framework code first migration for productive environment
        static AuctionsDataContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AuctionsDataContext>());
        }
    }
}