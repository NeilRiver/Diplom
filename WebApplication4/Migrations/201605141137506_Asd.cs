namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Asd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Description", c => c.String());
            AddColumn("dbo.Items", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Type");
            DropColumn("dbo.Items", "Description");
        }
    }
}
