namespace ShoppingCartCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_To_Database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        StreetAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        PinCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LineItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        OrderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductName = c.String(),
                        ProductCatagory = c.String(),
                        ProductCost = c.Double(nullable: false),
                        Discount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNo = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.String(),
                        Role = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        ProfilePhoto = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wishlists",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Wishlists");
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.LineItems");
            DropTable("dbo.Addresses");
        }
    }
}
