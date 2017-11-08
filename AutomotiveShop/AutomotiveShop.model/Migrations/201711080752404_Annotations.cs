namespace AutomotiveShop.model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Annotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarsDetails", "Producent", c => c.String(nullable: false));
            AlterColumn("dbo.CarsDetails", "Model", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.DeliveryAddresses", "StreetName", c => c.String(nullable: false));
            AlterColumn("dbo.DeliveryAddresses", "Postcode", c => c.String(nullable: false));
            AlterColumn("dbo.DeliveryAddresses", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Subcategories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Subcategories", "Name", c => c.String());
            AlterColumn("dbo.DeliveryAddresses", "City", c => c.String());
            AlterColumn("dbo.DeliveryAddresses", "Postcode", c => c.String());
            AlterColumn("dbo.DeliveryAddresses", "StreetName", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.CarsDetails", "Model", c => c.String());
            AlterColumn("dbo.CarsDetails", "Producent", c => c.String());
        }
    }
}
