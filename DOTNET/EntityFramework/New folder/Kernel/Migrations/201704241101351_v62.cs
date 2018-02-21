namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v62 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.PARTNER",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        EmailId = c.String(),
                        PhoneNo = c.String(),
                        Color = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("gsmktg.LOGIN", "Partner_Id", c => c.Guid());
            CreateIndex("gsmktg.LOGIN", "Partner_Id");
            AddForeignKey("gsmktg.LOGIN", "Partner_Id", "gsmktg.PARTNER", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.LOGIN", "Partner_Id", "gsmktg.PARTNER");
            DropIndex("gsmktg.LOGIN", new[] { "Partner_Id" });
            DropColumn("gsmktg.LOGIN", "Partner_Id");
            DropTable("gsmktg.PARTNER");
        }
    }
}
