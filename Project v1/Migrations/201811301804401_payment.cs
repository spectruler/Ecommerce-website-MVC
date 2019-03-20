namespace Project_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        PayId = c.Int(nullable: false, identity: true),
                        CardNumber = c.Int(),
                        Amount = c.Double(nullable: false),
                        PayType = c.String(),
                        OrderId = c.Int(),
                        Email = c.String(maxLength: 128),
                        PayDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PayId)
                .ForeignKey("dbo.Account", t => t.Email,cascadeDelete:true)
                .ForeignKey("dbo.Order", t => t.OrderId)
                .Index(t => t.OrderId)
                .Index(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payment", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Payment", "Email", "dbo.Account");
            DropIndex("dbo.Payment", new[] { "Email" });
            DropIndex("dbo.Payment", new[] { "OrderId" });
            DropTable("dbo.Payment");
        }
    }
}
