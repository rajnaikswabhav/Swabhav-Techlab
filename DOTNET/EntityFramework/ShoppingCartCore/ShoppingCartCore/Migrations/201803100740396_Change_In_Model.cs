namespace ShoppingCartCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_In_Model : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Gender", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "Role", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Role", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Gender", c => c.Int(nullable: false));
        }
    }
}
