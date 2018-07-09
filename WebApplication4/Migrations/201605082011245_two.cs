namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class two : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Name", c => c.String());
            AddColumn("dbo.Items", "Price", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Price");
            DropColumn("dbo.Items", "Name");
        }
    }
}
