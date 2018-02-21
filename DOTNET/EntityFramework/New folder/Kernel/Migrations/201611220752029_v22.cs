namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.EVENT", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("gsmktg.EVENT", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("gsmktg.ACCESS", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.ACCESS", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.SECTION", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.SECTION", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.EXHIBITION", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.EXHIBITION", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.COUNTRY", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.COUNTRY", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.EXHIBITOR", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.EXHIBITOR", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.CATEGORY", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.CATEGORY", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.ORGANIZER", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.ORGANIZER", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.VENUE", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.VENUE", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.TICKETTYPE", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.TICKETTYPE", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.TICKET", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.TICKET", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.VISITOR", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.VISITOR", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.STALL", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.STALL", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.PAVILION", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.PAVILION", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.STATE", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.STATE", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.LAYOUTPLAN", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.LAYOUTPLAN", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.EVENT", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.EVENT", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.SECTIONTYPE", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.SECTIONTYPE", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.EVENTEXHIBITIONMAP", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.EVENTEXHIBITIONMAP", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.EVENTTICKETTYPE", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.EVENTTICKETTYPE", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.EXHIBITORMAP", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.EXHIBITORMAP", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.LOGIN", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.LOGIN", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.ROLE", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.ROLE", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.USER", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.USER", "LastModifiedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.TRANSACTION", "CreatedBy", c => c.Guid(nullable: true));
            AlterColumn("gsmktg.TRANSACTION", "LastModifiedBy", c => c.Guid(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("gsmktg.TRANSACTION", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.TRANSACTION", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.USER", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.USER", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.ROLE", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.ROLE", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.LOGIN", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.LOGIN", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.EXHIBITORMAP", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.EXHIBITORMAP", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.EVENTTICKETTYPE", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.EVENTTICKETTYPE", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.EVENTEXHIBITIONMAP", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.EVENTEXHIBITIONMAP", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.SECTIONTYPE", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.SECTIONTYPE", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.EVENT", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.EVENT", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.LAYOUTPLAN", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.LAYOUTPLAN", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.STATE", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.STATE", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.PAVILION", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.PAVILION", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.STALL", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.STALL", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.VISITOR", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.VISITOR", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.TICKET", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.TICKET", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.TICKETTYPE", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.TICKETTYPE", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.VENUE", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.VENUE", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.ORGANIZER", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.ORGANIZER", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.CATEGORY", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.CATEGORY", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.EXHIBITOR", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.EXHIBITOR", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.COUNTRY", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.COUNTRY", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.EXHIBITION", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.EXHIBITION", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.SECTION", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.SECTION", "CreatedBy", c => c.String());
            AlterColumn("gsmktg.ACCESS", "LastModifiedBy", c => c.String());
            AlterColumn("gsmktg.ACCESS", "CreatedBy", c => c.String());
            DropColumn("gsmktg.EVENT", "EndDate");
            DropColumn("gsmktg.EVENT", "StartDate");
        }
    }
}
