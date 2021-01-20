using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskWebApp1.Models;

namespace TestTaskWebApp1
{
	public class DbEntities : DbContext
	{
		public static readonly string connectionstring = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog= ExchangesDb;";

        public DbSet<EchangeRecord> EchangeRecords { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbEntities(DbContextOptions<DbEntities> options) : base(options)
        {
            Database.EnsureCreated();
            _options = options;
        }
        public DbContextOptions<DbEntities> _options;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionstring);
        }

    }
}
