namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v8 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "gsmktg.Sections", newName: "SECTION");
            RenameTable(name: "gsmktg.Accesses", newName: "ACCESS");
            RenameTable(name: "gsmktg.SectionTypes", newName: "SECTIONTYPE");
        }
        
        public override void Down()
        {
            RenameTable(name: "gsmktg.SECTIONTYPE", newName: "SectionTypes");
            RenameTable(name: "gsmktg.ACCESS", newName: "Accesses");
            RenameTable(name: "gsmktg.SECTION", newName: "Sections");
        }
    }
}
