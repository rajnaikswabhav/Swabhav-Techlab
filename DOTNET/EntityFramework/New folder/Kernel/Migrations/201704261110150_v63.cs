namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v63 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("gsmktg.LOGIN", "ParentLogin_Id", "gsmktg.LOGIN");
            DropIndex("gsmktg.LOGIN", new[] { "ParentLogin_Id" });
            CreateTable(
                "gsmktg.BARRIER",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        X_Coordinate = c.Int(nullable: false),
                        Y_Coordinate = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Section_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.SECTION", t => t.Section_Id)
                .Index(t => t.Section_Id);
            
            AddColumn("gsmktg.BOOKING", "Section_Id", c => c.Guid());
            AddColumn("gsmktg.BOOKINGREQUEST", "DiscountPercent", c => c.Int());
            AddColumn("gsmktg.BOOKINGREQUEST", "DiscountAmount", c => c.Int());
            AddColumn("gsmktg.BOOKINGREQUEST", "Login_Id", c => c.Guid());
            AddColumn("gsmktg.BOOKINGREQUEST", "Section_Id", c => c.Guid());
            CreateIndex("gsmktg.BOOKING", "Section_Id");
            CreateIndex("gsmktg.BOOKINGREQUEST", "Login_Id");
            CreateIndex("gsmktg.BOOKINGREQUEST", "Section_Id");
            AddForeignKey("gsmktg.BOOKING", "Section_Id", "gsmktg.SECTION", "Id");
            AddForeignKey("gsmktg.BOOKINGREQUEST", "Login_Id", "gsmktg.LOGIN", "Id");
            AddForeignKey("gsmktg.BOOKINGREQUEST", "Section_Id", "gsmktg.SECTION", "Id");
            DropColumn("gsmktg.LOGIN", "ParentLogin_Id");
        }
        
        public override void Down()
        {
            AddColumn("gsmktg.LOGIN", "ParentLogin_Id", c => c.Guid());
            DropForeignKey("gsmktg.BOOKINGREQUEST", "Section_Id", "gsmktg.SECTION");
            DropForeignKey("gsmktg.BOOKINGREQUEST", "Login_Id", "gsmktg.LOGIN");
            DropForeignKey("gsmktg.BOOKING", "Section_Id", "gsmktg.SECTION");
            DropForeignKey("gsmktg.BARRIER", "Section_Id", "gsmktg.SECTION");
            DropIndex("gsmktg.BOOKINGREQUEST", new[] { "Section_Id" });
            DropIndex("gsmktg.BOOKINGREQUEST", new[] { "Login_Id" });
            DropIndex("gsmktg.BOOKING", new[] { "Section_Id" });
            DropIndex("gsmktg.BARRIER", new[] { "Section_Id" });
            DropColumn("gsmktg.BOOKINGREQUEST", "Section_Id");
            DropColumn("gsmktg.BOOKINGREQUEST", "Login_Id");
            DropColumn("gsmktg.BOOKINGREQUEST", "DiscountAmount");
            DropColumn("gsmktg.BOOKINGREQUEST", "DiscountPercent");
            DropColumn("gsmktg.BOOKING", "Section_Id");
            DropTable("gsmktg.BARRIER");
            CreateIndex("gsmktg.LOGIN", "ParentLogin_Id");
            AddForeignKey("gsmktg.LOGIN", "ParentLogin_Id", "gsmktg.LOGIN", "Id");
        }
    }
}
