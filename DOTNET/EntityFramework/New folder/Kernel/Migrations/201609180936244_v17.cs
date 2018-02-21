namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v17 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "gsmktg.LayoutPlans", newName: "LAYOUTPLAN");
            RenameTable(name: "gsmktg.ExhibitorCategories", newName: "CategoryExhibitors");
            RenameTable(name: "gsmktg.ExhibitionExhibitors", newName: "ExhibitorExhibitions");
            
            CreateTable(
                "gsmktg.LOGIN",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropPrimaryKey("gsmktg.ExhibitorExhibitions");
            DropPrimaryKey("gsmktg.CategoryExhibitors");
            DropTable("gsmktg.LOGIN");
            AddPrimaryKey("gsmktg.ExhibitorExhibitions", new[] { "Exhibition_Id", "Exhibitor_Id" });
            AddPrimaryKey("gsmktg.CategoryExhibitors", new[] { "Exhibitor_Id", "Category_Id" });
            RenameTable(name: "gsmktg.ExhibitorExhibitions", newName: "ExhibitionExhibitors");
            RenameTable(name: "gsmktg.CategoryExhibitors", newName: "ExhibitorCategories");
            RenameTable(name: "gsmktg.LAYOUTPLAN", newName: "LayoutPlans");
        }
    }
}
