namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.accounts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        sc_name = c.String(nullable: false, maxLength: 255, fixedLength: true, unicode: false, storeType: "char"),
                        password = c.String(nullable: false, maxLength: 72, fixedLength: true, unicode: false, storeType: "char"),
                        role = c.Byte(nullable: false),
                        last_ip = c.String(nullable: false, maxLength: 50, fixedLength: true, unicode: false, storeType: "char"),
                        last_seen = c.DateTime(nullable: false, precision: 0),
                        banned = c.Boolean(nullable: false),
                        banned_type = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.sc_name, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.accounts", new[] { "sc_name" });
            DropTable("dbo.accounts");
        }
    }
}
