namespace AutomotiveShop.model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelieveryAdresses : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.DelieveryAddressId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Orders", "DelieveryAdress_DelieveryAddressId", c => c.Guid());
            CreateIndex("dbo.Orders", "DelieveryAdress_DelieveryAddressId");
            AddForeignKey("dbo.Orders", "DelieveryAdress_DelieveryAddressId", "dbo.DelieveryAddresses", "DelieveryAddressId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "DelieveryAdress_DelieveryAddressId", "dbo.DelieveryAddresses");
            DropForeignKey("dbo.DelieveryAddresses", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.DelieveryAddresses", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "DelieveryAdress_DelieveryAddressId" });
            DropColumn("dbo.Orders", "DelieveryAdress_DelieveryAddressId");
            DropTable("dbo.DelieveryAddresses");
        }
    }
}
