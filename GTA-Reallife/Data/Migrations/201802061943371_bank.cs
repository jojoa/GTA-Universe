namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bank : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Banns", "ban_type", c => c.Byte(nullable: false));
            AddColumn("dbo.Banns", "ban_time", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Banns", "ban_user", c => c.String(nullable: false, unicode: false));
            AddColumn("dbo.Banns", "ban_reason", c => c.String(nullable: false, unicode: false));
            AddColumn("dbo.Banns", "ban_issued", c => c.DateTime(nullable: false, precision: 0));
            DropColumn("dbo.Banns", "last_seen");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Banns", "last_seen", c => c.DateTime(nullable: false, precision: 0));
            DropColumn("dbo.Banns", "ban_issued");
            DropColumn("dbo.Banns", "ban_reason");
            DropColumn("dbo.Banns", "ban_user");
            DropColumn("dbo.Banns", "ban_time");
            DropColumn("dbo.Banns", "ban_type");
            DropTable("dbo.BankAccounts");
        }
    }
}
