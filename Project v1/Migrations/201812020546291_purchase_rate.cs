namespace Project_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchase_rate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseItemHistory",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        PurchaseCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Item", t => t.ItemId,cascadeDelete:true)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.RateItem",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        rate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Item", t => t.ItemId,cascadeDelete:true)
                .Index(t => t.ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RateItem", "ItemId", "dbo.Item");
            DropForeignKey("dbo.PurchaseItemHistory", "ItemId", "dbo.Item");
            DropIndex("dbo.RateItem", new[] { "ItemId" });
            DropIndex("dbo.PurchaseItemHistory", new[] { "ItemId" });
            DropTable("dbo.RateItem");
            DropTable("dbo.PurchaseItemHistory");
        }
    }
}
