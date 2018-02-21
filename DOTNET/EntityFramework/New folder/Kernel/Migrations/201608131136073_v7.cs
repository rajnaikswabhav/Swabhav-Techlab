namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.Sections",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Height = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        X_Coordinate = c.Int(nullable: false),
                        Y_Coordinate = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Section_Id = c.Guid(),
                        SectionType_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.Sections", t => t.Section_Id)
                .ForeignKey("gsmktg.SectionTypes", t => t.SectionType_Id)
                .Index(t => t.Section_Id)
                .Index(t => t.SectionType_Id);
            
            CreateTable(
                "gsmktg.Accesses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsEntry = c.Boolean(nullable: false),
                        IsExit = c.Boolean(nullable: false),
                        IsEmergencyExit = c.Boolean(nullable: false),
                        Height = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        X_Coordinate = c.Int(nullable: false),
                        Y_Coordinate = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Section_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.Sections", t => t.Section_Id)
                .Index(t => t.Section_Id);
            
            CreateTable(
                "gsmktg.SectionTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("gsmktg.STALL", "Price", c => c.Int(nullable: false));
            AddColumn("gsmktg.STALL", "IsBooked", c => c.Boolean(nullable: false));
            AddColumn("gsmktg.STALL", "Height", c => c.Int(nullable: false));
            AddColumn("gsmktg.STALL", "Width", c => c.Int(nullable: false));
            AddColumn("gsmktg.STALL", "X_Coordinate", c => c.Int(nullable: false));
            AddColumn("gsmktg.STALL", "Y_Coordinate", c => c.Int(nullable: false));
            AddColumn("gsmktg.STALL", "Section_Id", c => c.Guid());
            CreateIndex("gsmktg.STALL", "Section_Id");
            AddForeignKey("gsmktg.STALL", "Section_Id", "gsmktg.Sections", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.STALL", "Section_Id", "gsmktg.Sections");
            DropForeignKey("gsmktg.Sections", "SectionType_Id", "gsmktg.SectionTypes");
            DropForeignKey("gsmktg.Sections", "Section_Id", "gsmktg.Sections");
            DropForeignKey("gsmktg.Accesses", "Section_Id", "gsmktg.Sections");
            DropIndex("gsmktg.Accesses", new[] { "Section_Id" });
            DropIndex("gsmktg.Sections", new[] { "SectionType_Id" });
            DropIndex("gsmktg.Sections", new[] { "Section_Id" });
            DropIndex("gsmktg.STALL", new[] { "Section_Id" });
            DropColumn("gsmktg.STALL", "Section_Id");
            DropColumn("gsmktg.STALL", "Y_Coordinate");
            DropColumn("gsmktg.STALL", "X_Coordinate");
            DropColumn("gsmktg.STALL", "Width");
            DropColumn("gsmktg.STALL", "Height");
            DropColumn("gsmktg.STALL", "IsBooked");
            DropColumn("gsmktg.STALL", "Price");
            DropTable("gsmktg.SectionTypes");
            DropTable("gsmktg.Accesses");
            DropTable("gsmktg.Sections");
        }
    }
}
