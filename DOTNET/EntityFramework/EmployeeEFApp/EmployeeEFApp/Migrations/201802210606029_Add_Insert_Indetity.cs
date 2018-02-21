namespace EmployeeEFApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Insert_Indetity : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Departments");
            DropPrimaryKey("dbo.Employees");
            AlterColumn("dbo.Departments", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Departments", "Id");
            AddPrimaryKey("dbo.Employees", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Employees");
            DropPrimaryKey("dbo.Departments");
            AlterColumn("dbo.Employees", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Departments", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Employees", "Id");
            AddPrimaryKey("dbo.Departments", "Id");
        }
    }
}
