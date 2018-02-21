namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v74 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "gsmktg.BOOKING", name: "LoginId_Id", newName: "Login_Id");
            RenameIndex(table: "gsmktg.BOOKING", name: "IX_LoginId_Id", newName: "IX_Login_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "gsmktg.BOOKING", name: "IX_Login_Id", newName: "IX_LoginId_Id");
            RenameColumn(table: "gsmktg.BOOKING", name: "Login_Id", newName: "LoginId_Id");
        }
    }
}
