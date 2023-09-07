using Hello.Data.Interface;
using System;
using System.Data.Entity;

namespace Hello.Data
{
    class DatabaseContext : DbContext, IDataContext
    {
        public DatabaseContext()
            : base("name=HelloRentDbContext")
        {
            this.Database.CommandTimeout = 180;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public Database GetDbContext()
        {
            return this.Database;
        }
    }
}
