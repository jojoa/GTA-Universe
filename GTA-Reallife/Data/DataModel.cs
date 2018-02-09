
using Data.Models;
using MySql.Data.Entity;
using System.Data.Entity;

namespace GTAReallife.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DataModel : DbContext
    {

        public DataModel(string connectionString) : base(connectionString)
        {

        }

        public DataModel() : base("server=localhost;port=3306;database=GTMP-Data;uid=root;password=test1234")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DataModel>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<BanModel> Banns { get; set; }
        public DbSet<BankModel> Banks { get; set; }

        public DbSet<ItemModel> Items { get; set; }
        public DbSet<InventoryModel> Invs { get; set; }


    }
}
