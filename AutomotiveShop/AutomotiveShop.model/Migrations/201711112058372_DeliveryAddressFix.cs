namespace AutomotiveShop.model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeliveryAddressFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "DeliveryAddress_DeliveryAddressId", "dbo.DeliveryAddresses");
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "DeliveryAddress_DeliveryAddressId" });
            AddColumn("dbo.Orders", "DeliveryAddressId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Orders", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Orders", "DeliveryAddress_DeliveryAddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "DeliveryAddress_DeliveryAddressId", c => c.Guid());
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "UserId" });
            AlterColumn("dbo.Orders", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.Orders", "DeliveryAddressId");
            CreateIndex("dbo.Orders", "DeliveryAddress_DeliveryAddressId");
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Orders", "DeliveryAddress_DeliveryAddressId", "dbo.DeliveryAddresses", "DeliveryAddressId");
        }
    }
}
