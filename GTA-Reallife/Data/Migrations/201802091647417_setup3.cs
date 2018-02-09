namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setup3 : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Inventorys", "AccountModel_AccountId", c => c.Int());
            CreateIndex("dbo.Inventorys", "AccountModel_AccountId");
            AddForeignKey("dbo.Inventorys", "AccountModel_AccountId", "dbo.Accounts", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skins", "AccountID", "dbo.Accounts");
            DropForeignKey("dbo.Inventorys", "AccountModel_AccountId", "dbo.Accounts");
            DropIndex("dbo.Skins", new[] { "AccountID" });
            DropIndex("dbo.Inventorys", new[] { "AccountModel_AccountId" });
            DropColumn("dbo.Inventorys", "AccountModel_AccountId");
            DropTable("dbo.Skins");
        }
    }
}
