namespace AutomotiveShop.model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarsDetails",
                c => new
                    {
                        CarDetailsId = c.Guid(nullable: false, identity: true),
                        Producent = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        YearOfProduction = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarDetailsId);
            
            CreateTable(
                "dbo.ProductsByCars",
                c => new
                    {
                        ProductByCarId = c.Guid(nullable: false, identity: true),
                        ProductId = c.Guid(nullable: false),
                        CarId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ProductByCarId)
                .ForeignKey("dbo.CarsDetails", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        ItemsAvailable = c.Int(nullable: false),
                        SubcategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Subcategories", t => t.SubcategoryId, cascadeDelete: true)
                .Index(t => t.SubcategoryId);
            
            CreateTable(
                "dbo.ProductsCopies",
                c => new
                    {
                        ProductCopyId = c.Guid(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ProductCopyId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Guid(nullable: false, identity: true),
                        OrderState = c.Int(nullable: false),
                        DateOfPurchase = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        DeliveryAddress_DeliveryAddressId = c.Guid(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.DeliveryAddresses", t => t.DeliveryAddress_DeliveryAddressId)
                .Index(t => t.UserId)
                .Index(t => t.DeliveryAddress_DeliveryAddressId);
            
            CreateTable(
                "dbo.DeliveryAddresses",
                c => new
                    {
                        DeliveryAddressId = c.Guid(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        StreetName = c.String(nullable: false),
                        Postcode = c.String(nullable: false),
                        City = c.String(nullable: false),
                        PhoneNumber = c.String(),
                        AdditionalInfo = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DeliveryAddressId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Subcategories",
                c => new
                    {
                        SubcategoryId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
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
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ProductsByCars", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "SubcategoryId", "dbo.Subcategories");
            DropForeignKey("dbo.Subcategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.ProductsCopies", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductsCopies", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "DeliveryAddress_DeliveryAddressId", "dbo.DeliveryAddresses");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DeliveryAddresses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductsByCars", "CarId", "dbo.CarsDetails");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Subcategories", new[] { "CategoryId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.DeliveryAddresses", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "DeliveryAddress_DeliveryAddressId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.ProductsCopies", new[] { "OrderId" });
            DropIndex("dbo.ProductsCopies", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "SubcategoryId" });
            DropIndex("dbo.ProductsByCars", new[] { "CarId" });
            DropIndex("dbo.ProductsByCars", new[] { "ProductId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Categories");
            DropTable("dbo.Subcategories");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.DeliveryAddresses");
            DropTable("dbo.Orders");
            DropTable("dbo.ProductsCopies");
            DropTable("dbo.Products");
            DropTable("dbo.ProductsByCars");
            DropTable("dbo.CarsDetails");
        }
    }
}
