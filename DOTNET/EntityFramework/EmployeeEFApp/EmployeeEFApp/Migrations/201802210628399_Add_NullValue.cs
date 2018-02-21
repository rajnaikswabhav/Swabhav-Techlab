namespace EmployeeEFApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_NullValue : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "MGR", c => c.Int());
            AlterColumn("dbo.Employees", "Commision", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Commision", c => c.Double(nullable: false));
            AlterColumn("dbo.Employees", "MGR", c => c.Int(nullable: false));
        }
    }
}
