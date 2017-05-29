namespace AutomotiveShop.model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ItemsAvailable", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ItemsAvailable");
        }
    }
}
