namespace Project_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class account : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 16),
                        LastName = c.String(nullable: false, maxLength: 16),
                        Password = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Account");
        }
    }
}
