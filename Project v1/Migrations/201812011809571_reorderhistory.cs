namespace Project_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reorderhistory : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrderHistory");
            AddColumn("dbo.OrderHistory", "PayId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.OrderHistory", new[] { "OrderId", "PayId", "ItemId" });
            CreateIndex("dbo.OrderHistory", "PayId");
            AddForeignKey("dbo.OrderHistory", "PayId", "dbo.Payment", "PayId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderHistory", "PayId", "dbo.Payment");
            DropIndex("dbo.OrderHistory", new[] { "PayId" });
            DropPrimaryKey("dbo.OrderHistory");
            DropColumn("dbo.OrderHistory", "PayId");
            AddPrimaryKey("dbo.OrderHistory", new[] { "OrderId", "ItemId" });
        }
    }
}
