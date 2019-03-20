namespace Project_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removediscount : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Item", "Discount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Item", "Discount", c => c.Double(nullable: false));
        }
    }
}
