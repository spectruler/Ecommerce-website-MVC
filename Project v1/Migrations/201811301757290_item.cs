namespace Project_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class item : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 2048),
                        ImageUrl = c.String(nullable: false),
                        ProductId = c.Int(),
                        Discount = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Availability = c.Boolean(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId,cascadeDelete:true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Item", "ProductId", "dbo.Product");
            DropIndex("dbo.Item", new[] { "ProductId" });
            DropTable("dbo.Item");
        }
    }
}
