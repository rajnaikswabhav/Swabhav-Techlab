namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v29 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.EXHIBITOR", "EmailId", c => c.String());
            AddColumn("gsmktg.EXHIBITOR", "PhoneNo", c => c.String());
            AddColumn("gsmktg.EXHIBITOR", "CompanyName", c => c.String());
            AddColumn("gsmktg.EXHIBITOR", "Designation", c => c.String());
            AddColumn("gsmktg.EXHIBITOR", "CompanyDescription", c => c.String());
            AddColumn("gsmktg.EXHIBITOR", "Address", c => c.String());
            AddColumn("gsmktg.EXHIBITOR", "PinCode", c => c.Int(nullable: false));
            AddColumn("gsmktg.EXHIBITOR", "Password", c => c.String());
            DropColumn("gsmktg.EXHIBITOR", "Description");
        }
        
        public override void Down()
        {
            AddColumn("gsmktg.EXHIBITOR", "Description", c => c.String());
            DropColumn("gsmktg.EXHIBITOR", "Password");
            DropColumn("gsmktg.EXHIBITOR", "PinCode");
            DropColumn("gsmktg.EXHIBITOR", "Address");
            DropColumn("gsmktg.EXHIBITOR", "CompanyDescription");
            DropColumn("gsmktg.EXHIBITOR", "Designation");
            DropColumn("gsmktg.EXHIBITOR", "CompanyName");
            DropColumn("gsmktg.EXHIBITOR", "PhoneNo");
            DropColumn("gsmktg.EXHIBITOR", "EmailId");
        }
    }
}
