namespace AutomotiveShop.model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitV2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductByCars",
                c => new
                    {
                        ProductByCarId = c.Guid(nullable: false, identity: true),
                        ProductId = c.Guid(nullable: false),
                        CarId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ProductByCarId)
                .ForeignKey("dbo.CarDetails", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.CarDetails",
                c => new
                    {
                        CarDetailsId = c.Guid(nullable: false, identity: true),
                        Producent = c.String(),
                        Model = c.String(),
                        YearOfProduction = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarDetailsId);
            
            CreateTable(
                "dbo.ProductCopies",
                c => new
                    {
                        ProductCopyId = c.Guid(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        ProductId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ProductCopyId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductInOrders",
                c => new
                    {
                        ProductInOrderId = c.Guid(nullable: false, identity: true),
                        ProductCopyId = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ProductInOrderId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.ProductCopies", t => t.ProductCopyId, cascadeDelete: true)
                .Index(t => t.ProductCopyId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Guid(nullable: false, identity: true),
                        IsCompleted = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Subcategories",
                c => new
                    {
                        SubcategoryId = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.SubcategoryId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.Products", "SubcategoryId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Products", "SubcategoryId");
            AddForeignKey("dbo.Products", "SubcategoryId", "dbo.Subcategories", "SubcategoryId", cascadeDelete: true);
            DropColumn("dbo.Products", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Price", c => c.Double(nullable: false));
            DropForeignKey("dbo.Products", "SubcategoryId", "dbo.Subcategories");
            DropForeignKey("dbo.Subcategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.ProductCopies", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductInOrders", "ProductCopyId", "dbo.ProductCopies");
            DropForeignKey("dbo.ProductInOrders", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductByCars", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductByCars", "CarId", "dbo.CarDetails");
            DropIndex("dbo.Subcategories", new[] { "CategoryId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.ProductInOrders", new[] { "OrderId" });
            DropIndex("dbo.ProductInOrders", new[] { "ProductCopyId" });
            DropIndex("dbo.ProductCopies", new[] { "ProductId" });
            DropIndex("dbo.ProductByCars", new[] { "CarId" });
            DropIndex("dbo.ProductByCars", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "SubcategoryId" });
            DropColumn("dbo.Products", "SubcategoryId");
            DropTable("dbo.Categories");
            DropTable("dbo.Subcategories");
            DropTable("dbo.Orders");
            DropTable("dbo.ProductInOrders");
            DropTable("dbo.ProductCopies");
            DropTable("dbo.CarDetails");
            DropTable("dbo.ProductByCars");
        }
    }
}
