namespace Project_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        CategoryId = c.Int(),
                        ImageUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete:true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductType", "CategoryId", "dbo.Category");
            DropIndex("dbo.ProductType", new[] { "CategoryId" });
            DropTable("dbo.ProductType");
        }
    }
}
