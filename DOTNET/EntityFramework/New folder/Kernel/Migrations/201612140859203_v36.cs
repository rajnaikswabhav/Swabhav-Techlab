namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v36 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "gsmktg.StallTransactions", newName: "STALLTRANSACTION");
            CreateTable(
                "gsmktg.LOGINSESSION",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Login_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.LOGIN", t => t.Login_Id)
                .Index(t => t.Login_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.LOGINSESSION", "Login_Id", "gsmktg.LOGIN");
            DropIndex("gsmktg.LOGINSESSION", new[] { "Login_Id" });
            DropTable("gsmktg.LOGINSESSION");
            RenameTable(name: "gsmktg.STALLTRANSACTION", newName: "StallTransactions");
        }
    }
}
