namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v77 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.VISITORCARDPAYMENT", "Login_Id", c => c.Guid());
            CreateIndex("gsmktg.VISITORCARDPAYMENT", "Login_Id");
            AddForeignKey("gsmktg.VISITORCARDPAYMENT", "Login_Id", "gsmktg.LOGIN", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.VISITORCARDPAYMENT", "Login_Id", "gsmktg.LOGIN");
            DropIndex("gsmktg.VISITORCARDPAYMENT", new[] { "Login_Id" });
            DropColumn("gsmktg.VISITORCARDPAYMENT", "Login_Id");
        }
    }
}
