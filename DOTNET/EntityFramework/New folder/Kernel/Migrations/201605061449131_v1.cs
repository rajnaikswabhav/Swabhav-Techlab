namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gsmktg.CATEGORY",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Organizer_Id = c.Guid(),
                        Organizer_TenantId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.ORGANIZER", t => new { t.Organizer_Id, t.Organizer_TenantId })
                .Index(t => new { t.Organizer_Id, t.Organizer_TenantId });
            
            CreateTable(
                "gsmktg.EXHIBITOR",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Organizer_Id = c.Guid(),
                        Organizer_TenantId = c.Int(),
                        Country_Id = c.Guid(),
                        State_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.ORGANIZER", t => new { t.Organizer_Id, t.Organizer_TenantId })
                .ForeignKey("gsmktg.COUNTRY", t => t.Country_Id)
                .ForeignKey("gsmktg.STATE", t => t.State_Id)
                .Index(t => new { t.Organizer_Id, t.Organizer_TenantId })
                .Index(t => t.Country_Id)
                .Index(t => t.State_Id);
            
            CreateTable(
                "gsmktg.COUNTRY",
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
                        Exhibitons_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EXHIBITION", t => t.Exhibitons_Id)
                .Index(t => t.Exhibitons_Id);
            
            CreateTable(
                "gsmktg.EXHIBITION",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        BannerImage = c.String(),
                        Bannertext = c.String(),
                        Logo = c.String(),
                        BgImage = c.String(),
                        TicketBookingStatus = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Organizer_Id = c.Guid(),
                        Organizer_TenantId = c.Int(),
                        Venue_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.ORGANIZER", t => new { t.Organizer_Id, t.Organizer_TenantId })
                .ForeignKey("gsmktg.VENUE", t => t.Venue_Id)
                .Index(t => new { t.Organizer_Id, t.Organizer_TenantId })
                .Index(t => t.Venue_Id);
            
            CreateTable(
                "gsmktg.ORGANIZER",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TenantId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.TenantId });
            
            CreateTable(
                "gsmktg.VENUE",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        City = c.String(),
                        Address = c.String(),
                        State = c.String(),
                        Order = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Organizer_Id = c.Guid(),
                        Organizer_TenantId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.ORGANIZER", t => new { t.Organizer_Id, t.Organizer_TenantId })
                .Index(t => new { t.Organizer_Id, t.Organizer_TenantId });
            
            CreateTable(
                "gsmktg.TICKETTYPE",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        NumberOfDaysIncluded = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Venue_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.VENUE", t => t.Venue_Id)
                .Index(t => t.Venue_Id);
            
            CreateTable(
                "gsmktg.TICKET",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TokenNumber = c.String(),
                        TicketDate = c.DateTime(nullable: false),
                        NumberOfTicket = c.Int(nullable: false),
                        TotalPriceOfTicket = c.Int(nullable: false),
                        TxnIdForPaymentGateWay = c.String(),
                        PaymentCompleted = c.Boolean(nullable: false),
                        IsPayOnLocation = c.Boolean(nullable: false),
                        ValidityDayCount = c.Int(nullable: false),
                        Status = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        TicketType_Id = c.Guid(),
                        Visitor_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.TICKETTYPE", t => t.TicketType_Id)
                .ForeignKey("gsmktg.VISITOR", t => t.Visitor_Id)
                .Index(t => t.TicketType_Id)
                .Index(t => t.Visitor_Id);
            
            CreateTable(
                "gsmktg.VISITOR",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Pincode = c.Int(nullable: false),
                        MobileNo = c.String(),
                        EmailId = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Educatoin = c.String(),
                        Income = c.String(),
                        OTPCode = c.String(),
                        isMobileNoVerified = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Organizer_Id = c.Guid(),
                        Organizer_TenantId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.ORGANIZER", t => new { t.Organizer_Id, t.Organizer_TenantId })
                .Index(t => new { t.Organizer_Id, t.Organizer_TenantId });
            
            CreateTable(
                "gsmktg.PAVILION",
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
                        Exhibition_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EXHIBITION", t => t.Exhibition_Id)
                .Index(t => t.Exhibition_Id);
            
            CreateTable(
                "gsmktg.STALL",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StallNo = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Exhibitor_Id = c.Guid(),
                        Pavilion_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EXHIBITOR", t => t.Exhibitor_Id)
                .ForeignKey("gsmktg.PAVILION", t => t.Pavilion_Id)
                .Index(t => t.Exhibitor_Id)
                .Index(t => t.Pavilion_Id);
            
            CreateTable(
                "gsmktg.STATE",
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
                        Exhibitons_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.EXHIBITION", t => t.Exhibitons_Id)
                .Index(t => t.Exhibitons_Id);
            
            CreateTable(
                "gsmktg.ROLE",
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
            
            CreateTable(
                "gsmktg.USER",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(),
                        Password = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                        Role_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gsmktg.ROLE", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "gsmktg.TRANSACTION",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TicketId = c.String(),
                        Status = c.String(),
                        Amount = c.Double(nullable: false),
                        PgTransactionNo = c.String(),
                        IssuerRefNo = c.String(),
                        AuthIdCode = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PgResponseCode = c.String(),
                        AddressZip = c.String(),
                        CitrusTxnId = c.String(),
                        TxMsg = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "gsmktg.ExhibitorCategories",
                c => new
                    {
                        Exhibitor_Id = c.Guid(nullable: false),
                        Category_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Exhibitor_Id, t.Category_Id })
                .ForeignKey("gsmktg.EXHIBITOR", t => t.Exhibitor_Id, cascadeDelete: true)
                .ForeignKey("gsmktg.CATEGORY", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Exhibitor_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "gsmktg.ExhibitionExhibitors",
                c => new
                    {
                        Exhibition_Id = c.Guid(nullable: false),
                        Exhibitor_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Exhibition_Id, t.Exhibitor_Id })
                .ForeignKey("gsmktg.EXHIBITION", t => t.Exhibition_Id, cascadeDelete: true)
                .ForeignKey("gsmktg.EXHIBITOR", t => t.Exhibitor_Id, cascadeDelete: true)
                .Index(t => t.Exhibition_Id)
                .Index(t => t.Exhibitor_Id);
            
            CreateTable(
                "gsmktg.TicketTypeExhibitions",
                c => new
                    {
                        TicketType_Id = c.Guid(nullable: false),
                        Exhibition_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.TicketType_Id, t.Exhibition_Id })
                .ForeignKey("gsmktg.TICKETTYPE", t => t.TicketType_Id, cascadeDelete: true)
                .ForeignKey("gsmktg.EXHIBITION", t => t.Exhibition_Id, cascadeDelete: true)
                .Index(t => t.TicketType_Id)
                .Index(t => t.Exhibition_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("gsmktg.USER", "Role_Id", "gsmktg.ROLE");
            DropForeignKey("gsmktg.EXHIBITOR", "State_Id", "gsmktg.STATE");
            DropForeignKey("gsmktg.EXHIBITOR", "Country_Id", "gsmktg.COUNTRY");
            DropForeignKey("gsmktg.STATE", "Exhibitons_Id", "gsmktg.EXHIBITION");
            DropForeignKey("gsmktg.STALL", "Pavilion_Id", "gsmktg.PAVILION");
            DropForeignKey("gsmktg.STALL", "Exhibitor_Id", "gsmktg.EXHIBITOR");
            DropForeignKey("gsmktg.PAVILION", "Exhibition_Id", "gsmktg.EXHIBITION");
            DropForeignKey("gsmktg.TICKETTYPE", "Venue_Id", "gsmktg.VENUE");
            DropForeignKey("gsmktg.TICKET", "Visitor_Id", "gsmktg.VISITOR");
            DropForeignKey("gsmktg.VISITOR", new[] { "Organizer_Id", "Organizer_TenantId" }, "gsmktg.ORGANIZER");
            DropForeignKey("gsmktg.TICKET", "TicketType_Id", "gsmktg.TICKETTYPE");
            DropForeignKey("gsmktg.TicketTypeExhibitions", "Exhibition_Id", "gsmktg.EXHIBITION");
            DropForeignKey("gsmktg.TicketTypeExhibitions", "TicketType_Id", "gsmktg.TICKETTYPE");
            DropForeignKey("gsmktg.VENUE", new[] { "Organizer_Id", "Organizer_TenantId" }, "gsmktg.ORGANIZER");
            DropForeignKey("gsmktg.EXHIBITION", "Venue_Id", "gsmktg.VENUE");
            DropForeignKey("gsmktg.EXHIBITOR", new[] { "Organizer_Id", "Organizer_TenantId" }, "gsmktg.ORGANIZER");
            DropForeignKey("gsmktg.EXHIBITION", new[] { "Organizer_Id", "Organizer_TenantId" }, "gsmktg.ORGANIZER");
            DropForeignKey("gsmktg.CATEGORY", new[] { "Organizer_Id", "Organizer_TenantId" }, "gsmktg.ORGANIZER");
            DropForeignKey("gsmktg.ExhibitionExhibitors", "Exhibitor_Id", "gsmktg.EXHIBITOR");
            DropForeignKey("gsmktg.ExhibitionExhibitors", "Exhibition_Id", "gsmktg.EXHIBITION");
            DropForeignKey("gsmktg.COUNTRY", "Exhibitons_Id", "gsmktg.EXHIBITION");
            DropForeignKey("gsmktg.ExhibitorCategories", "Category_Id", "gsmktg.CATEGORY");
            DropForeignKey("gsmktg.ExhibitorCategories", "Exhibitor_Id", "gsmktg.EXHIBITOR");
            DropIndex("gsmktg.TicketTypeExhibitions", new[] { "Exhibition_Id" });
            DropIndex("gsmktg.TicketTypeExhibitions", new[] { "TicketType_Id" });
            DropIndex("gsmktg.ExhibitionExhibitors", new[] { "Exhibitor_Id" });
            DropIndex("gsmktg.ExhibitionExhibitors", new[] { "Exhibition_Id" });
            DropIndex("gsmktg.ExhibitorCategories", new[] { "Category_Id" });
            DropIndex("gsmktg.ExhibitorCategories", new[] { "Exhibitor_Id" });
            DropIndex("gsmktg.USER", new[] { "Role_Id" });
            DropIndex("gsmktg.STATE", new[] { "Exhibitons_Id" });
            DropIndex("gsmktg.STALL", new[] { "Pavilion_Id" });
            DropIndex("gsmktg.STALL", new[] { "Exhibitor_Id" });
            DropIndex("gsmktg.PAVILION", new[] { "Exhibition_Id" });
            DropIndex("gsmktg.VISITOR", new[] { "Organizer_Id", "Organizer_TenantId" });
            DropIndex("gsmktg.TICKET", new[] { "Visitor_Id" });
            DropIndex("gsmktg.TICKET", new[] { "TicketType_Id" });
            DropIndex("gsmktg.TICKETTYPE", new[] { "Venue_Id" });
            DropIndex("gsmktg.VENUE", new[] { "Organizer_Id", "Organizer_TenantId" });
            DropIndex("gsmktg.EXHIBITION", new[] { "Venue_Id" });
            DropIndex("gsmktg.EXHIBITION", new[] { "Organizer_Id", "Organizer_TenantId" });
            DropIndex("gsmktg.COUNTRY", new[] { "Exhibitons_Id" });
            DropIndex("gsmktg.EXHIBITOR", new[] { "State_Id" });
            DropIndex("gsmktg.EXHIBITOR", new[] { "Country_Id" });
            DropIndex("gsmktg.EXHIBITOR", new[] { "Organizer_Id", "Organizer_TenantId" });
            DropIndex("gsmktg.CATEGORY", new[] { "Organizer_Id", "Organizer_TenantId" });
            DropTable("gsmktg.TicketTypeExhibitions");
            DropTable("gsmktg.ExhibitionExhibitors");
            DropTable("gsmktg.ExhibitorCategories");
            DropTable("gsmktg.TRANSACTION");
            DropTable("gsmktg.USER");
            DropTable("gsmktg.ROLE");
            DropTable("gsmktg.STATE");
            DropTable("gsmktg.STALL");
            DropTable("gsmktg.PAVILION");
            DropTable("gsmktg.VISITOR");
            DropTable("gsmktg.TICKET");
            DropTable("gsmktg.TICKETTYPE");
            DropTable("gsmktg.VENUE");
            DropTable("gsmktg.ORGANIZER");
            DropTable("gsmktg.EXHIBITION");
            DropTable("gsmktg.COUNTRY");
            DropTable("gsmktg.EXHIBITOR");
            DropTable("gsmktg.CATEGORY");
        }
    }
}
