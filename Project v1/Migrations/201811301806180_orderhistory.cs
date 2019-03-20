namespace Project_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderhistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderHistory",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        Email = c.String(maxLength: 128),
                        UnitPrice = c.Double(nullable: false),
                        ItemQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ItemId })
                .ForeignKey("dbo.Account", t => t.Email)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ItemId)
                .Index(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderHistory", "OrderId", "dbo.Order");
            DropForeignKey("dbo.OrderHistory", "ItemId", "dbo.Item");
            DropForeignKey("dbo.OrderHistory", "Email", "dbo.Account");
            DropIndex("dbo.OrderHistory", new[] { "Email" });
            DropIndex("dbo.OrderHistory", new[] { "ItemId" });
            DropIndex("dbo.OrderHistory", new[] { "OrderId" });
            DropTable("dbo.OrderHistory");
        }
    }
}
