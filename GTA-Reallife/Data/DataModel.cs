
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

            modelBuilder.Entity<ItemModel>()
                        .HasRequired(p => p.Inventory)
                        .WithMany(t => t.Items)
                        .HasForeignKey(p => p.InvID);

            modelBuilder.Entity<SkinModel>()
                        .HasRequired(p => p.Account)
                        .WithMany(t => t.Skin)
                        .HasForeignKey(p => p.AccountID);

        }

        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<BanModel> Banns { get; set; }
        public DbSet<BankModel> Banks { get; set; }

        public DbSet<ItemModel> Items { get; set; }
        public DbSet<InventoryModel> Invs { get; set; }
        public DbSet<SkinModel> Skins { get; set; }


    }
}
