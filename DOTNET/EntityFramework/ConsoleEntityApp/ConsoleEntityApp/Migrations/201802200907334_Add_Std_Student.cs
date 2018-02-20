namespace ConsoleEntityApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Std_Student : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Standard", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Standard");
        }
    }
}
