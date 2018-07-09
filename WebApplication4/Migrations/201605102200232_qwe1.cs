namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qwe1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Price", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Price", c => c.Single(nullable: false));
        }
    }
}
