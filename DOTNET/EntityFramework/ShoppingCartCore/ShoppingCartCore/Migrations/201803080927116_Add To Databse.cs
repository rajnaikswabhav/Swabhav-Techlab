namespace ShoppingCartCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddToDatabse : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Addresses", "Version");
            DropColumn("dbo.LineItems", "Version");
            DropColumn("dbo.Products", "Version");
            DropColumn("dbo.Orders", "Version");
            DropColumn("dbo.Users", "Version");
            DropColumn("dbo.Wishlists", "Version");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wishlists", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.LineItems", "Version", c => c.Int(nullable: false));
            AddColumn("dbo.Addresses", "Version", c => c.Int(nullable: false));
        }
    }
}
