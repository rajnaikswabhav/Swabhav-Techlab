namespace ShoppingCartCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ProfilePhoto_User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ProfilePhoto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ProfilePhoto");
        }
    }
}
