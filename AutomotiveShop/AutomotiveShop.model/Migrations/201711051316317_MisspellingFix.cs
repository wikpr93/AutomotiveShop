namespace AutomotiveShop.model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MisspellingFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DelieveryAddresses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "DelieveryAdress_DelieveryAddressId", "dbo.DelieveryAddresses");
            DropIndex("dbo.Orders", new[] { "DelieveryAdress_DelieveryAddressId" });
            DropIndex("dbo.DelieveryAddresses", new[] { "UserId" });
            CreateTable(
                "dbo.DeliveryAddresses",
                c => new
                    {
                        DeliveryAddressId = c.Guid(nullable: false),
                        CompanyName = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        StreetName = c.String(),
                        Postcode = c.String(),
                        City = c.String(),
                        PhoneNumber = c.String(),
                        AdditionalInfo = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DeliveryAddressId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Orders", "DeliveryAddress_DeliveryAddressId", c => c.Guid());
            CreateIndex("dbo.Orders", "DeliveryAddress_DeliveryAddressId");
            AddForeignKey("dbo.Orders", "DeliveryAddress_DeliveryAddressId", "dbo.DeliveryAddresses", "DeliveryAddressId");
            DropColumn("dbo.Orders", "DelieveryAdress_DelieveryAddressId");
            DropTable("dbo.DelieveryAddresses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DelieveryAddresses",
                c => new
                    {
                        DelieveryAddressId = c.Guid(nullable: false),
                        CompanyName = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        StreetName = c.String(),
                        Postcode = c.String(),
                        City = c.String(),
                        PhoneNumber = c.String(),
                        AdditionalInfo = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DelieveryAddressId);
            
            AddColumn("dbo.Orders", "DelieveryAdress_DelieveryAddressId", c => c.Guid());
            DropForeignKey("dbo.Orders", "DeliveryAddress_DeliveryAddressId", "dbo.DeliveryAddresses");
            DropForeignKey("dbo.DeliveryAddresses", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.DeliveryAddresses", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "DeliveryAddress_DeliveryAddressId" });
            DropColumn("dbo.Orders", "DeliveryAddress_DeliveryAddressId");
            DropTable("dbo.DeliveryAddresses");
            CreateIndex("dbo.DelieveryAddresses", "UserId");
            CreateIndex("dbo.Orders", "DelieveryAdress_DelieveryAddressId");
            AddForeignKey("dbo.Orders", "DelieveryAdress_DelieveryAddressId", "dbo.DelieveryAddresses", "DelieveryAddressId");
            AddForeignKey("dbo.DelieveryAddresses", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
