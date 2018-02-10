
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

            modelBuilder.Entity<AccountModel>()
                       .HasRequired(p => p.Inventory)
                       .WithMany(t => t.AccountOwner)
                       .HasForeignKey(p => p.InvID);

            modelBuilder.Entity<VehicleModel>()
                      .HasRequired(p => p.Inventory)
                      .WithMany(t => t.VehicleOwner)
                      .HasForeignKey(p => p.InvID);

            modelBuilder.Entity<PropertyModel>()
                      .HasRequired(p => p.Inventory)
                      .WithMany(t => t.PropertyOwner)
                      .HasForeignKey(p => p.InvID);

            modelBuilder.Entity<VehicleModel>()
                      .HasRequired(p => p.Account)
                      .WithMany(t => t.Vehicles)
                      .HasForeignKey(p => p.AccID);

            modelBuilder.Entity<PropertyModel>()
                      .HasRequired(p => p.Account)
                      .WithMany(t => t.Props)
                      .HasForeignKey(p => p.AccID);



        }

        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<BanModel> Banns { get; set; }
        public DbSet<BankModel> Banks { get; set; }
        public DbSet<ItemModel> Items { get; set; }
        public DbSet<InventoryModel> Invs { get; set; }
        public DbSet<SkinModel> Skins { get; set; }
        public DbSet<PropertyModel> Propertys { get; set; }
        public DbSet<VehicleModel> Vehicles { get; set; }


    }
}
