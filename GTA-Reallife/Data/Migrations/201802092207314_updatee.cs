namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        sc_name = c.String(nullable: false, maxLength: 255, fixedLength: true, unicode: false, storeType: "char"),
                        password = c.String(nullable: false, maxLength: 72, fixedLength: true, unicode: false, storeType: "char"),
                        role = c.Byte(nullable: false),
                        last_ip = c.String(nullable: false, maxLength: 50, fixedLength: true, unicode: false, storeType: "char"),
                        last_seen = c.DateTime(nullable: false, precision: 0),
                        InvID = c.Int(name: "Inv-ID", nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Inventory", t => t.InvID, cascadeDelete: true)
                .Index(t => t.sc_name, unique: true)
                .Index(t => t.InvID);
            
            CreateTable(
                "dbo.Inventory",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255, fixedLength: true, unicode: false, storeType: "char"),
                        Count = c.Int(nullable: false),
                        InvID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Inventory", t => t.InvID, cascadeDelete: true)
                .Index(t => t.InvID);
            
            CreateTable(
                "dbo.Propertys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InvID = c.Int(name: "Inv-ID", nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Inventory", t => t.InvID, cascadeDelete: true)
                .Index(t => t.InvID);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InvID = c.Int(name: "Inv-ID", nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Inventory", t => t.InvID, cascadeDelete: true)
                .Index(t => t.InvID);
            
            CreateTable(
                "dbo.Skins",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        AccountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Accounts", t => t.AccountID, cascadeDelete: true)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Banns",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        is_banned = c.Boolean(nullable: false),
                        ban_type = c.Byte(nullable: false),
                        ban_time = c.DateTime(nullable: false, precision: 0),
                        ban_user = c.String(nullable: false, unicode: false),
                        ban_reason = c.String(nullable: false, unicode: false),
                        ban_issued = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skins", "AccountID", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "Inv-ID", "dbo.Inventory");
            DropForeignKey("dbo.Vehicles", "Inv-ID", "dbo.Inventory");
            DropForeignKey("dbo.Propertys", "Inv-ID", "dbo.Inventory");
            DropForeignKey("dbo.Items", "InvID", "dbo.Inventory");
            DropIndex("dbo.Skins", new[] { "AccountID" });
            DropIndex("dbo.Vehicles", new[] { "Inv-ID" });
            DropIndex("dbo.Propertys", new[] { "Inv-ID" });
            DropIndex("dbo.Items", new[] { "InvID" });
            DropIndex("dbo.Accounts", new[] { "Inv-ID" });
            DropIndex("dbo.Accounts", new[] { "sc_name" });
            DropTable("dbo.Banns");
            DropTable("dbo.BankAccounts");
            DropTable("dbo.Skins");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Propertys");
            DropTable("dbo.Items");
            DropTable("dbo.Inventory");
            DropTable("dbo.Accounts");
        }
    }
}
