namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bann : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.banns",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        is_banned = c.Boolean(nullable: false),
                        last_seen = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
            DropColumn("dbo.accounts", "banned");
            DropColumn("dbo.accounts", "banned_type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.accounts", "banned_type", c => c.Byte(nullable: false));
            AddColumn("dbo.accounts", "banned", c => c.Boolean(nullable: false));
            DropTable("dbo.banns");
        }
    }
}
