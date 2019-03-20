namespace Project_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        PostCode = c.String(),
                        Province = c.String(),
                        Phone = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderDate = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Account", t => t.Email,cascadeDelete:true)
                .Index(t => t.Email);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        OrderId = c.Int(),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.OrderDetailId, t.ItemId })
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderId,cascadeDelete:true)
                .Index(t => t.ItemId)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetail", "OrderId", "dbo.Order");
            DropForeignKey("dbo.OrderDetail", "ItemId", "dbo.Item");
            DropForeignKey("dbo.Order", "Email", "dbo.Account");
            DropIndex("dbo.OrderDetail", new[] { "OrderId" });
            DropIndex("dbo.OrderDetail", new[] { "ItemId" });
            DropIndex("dbo.Order", new[] { "Email" });
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Order");
        }
    }
}
