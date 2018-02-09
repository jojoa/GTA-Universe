namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventorys",
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
                .ForeignKey("dbo.Inventorys", t => t.InvID, cascadeDelete: true)
                .Index(t => t.InvID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "InvID", "dbo.Inventorys");
            DropIndex("dbo.Items", new[] { "InvID" });
            DropTable("dbo.Items");
            DropTable("dbo.Inventorys");
        }
    }
}
