namespace Project_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 2048),
                        ImageUrl = c.String(nullable: false),
                        ProductTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductType", t => t.ProductTypeId,cascadeDelete:true)
                .Index(t => t.ProductTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "ProductTypeId", "dbo.ProductType");
            DropIndex("dbo.Product", new[] { "ProductTypeId" });
            DropTable("dbo.Product");
        }
    }
}
