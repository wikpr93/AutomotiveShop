namespace AutomotiveShop.model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderState : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "DeliveryAddress_DeliveryAddressId", "dbo.DeliveryAddresses");
            DropPrimaryKey("dbo.DeliveryAddresses");
            AddColumn("dbo.Orders", "OrderState", c => c.Int(nullable: false));
            AlterColumn("dbo.DeliveryAddresses", "DeliveryAddressId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.DeliveryAddresses", "DeliveryAddressId");
            AddForeignKey("dbo.Orders", "DeliveryAddress_DeliveryAddressId", "dbo.DeliveryAddresses", "DeliveryAddressId");
            DropColumn("dbo.Orders", "IsCompleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "IsCompleted", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Orders", "DeliveryAddress_DeliveryAddressId", "dbo.DeliveryAddresses");
            DropPrimaryKey("dbo.DeliveryAddresses");
            AlterColumn("dbo.DeliveryAddresses", "DeliveryAddressId", c => c.Guid(nullable: false));
            DropColumn("dbo.Orders", "OrderState");
            AddPrimaryKey("dbo.DeliveryAddresses", "DeliveryAddressId");
            AddForeignKey("dbo.Orders", "DeliveryAddress_DeliveryAddressId", "dbo.DeliveryAddresses", "DeliveryAddressId");
        }
    }
}
