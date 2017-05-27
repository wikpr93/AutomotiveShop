namespace AutomotiveShop.model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductCopyOrderRelationFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.ProductCopies", "OrderId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ProductCopies", "OrderId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductCopies", new[] { "OrderId" });
            DropColumn("dbo.ProductCopies", "OrderId");
            DropColumn("dbo.Products", "Price");
        }
    }
}
