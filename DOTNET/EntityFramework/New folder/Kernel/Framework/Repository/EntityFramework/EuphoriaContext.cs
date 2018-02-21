using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Model;
using Modules.EventManagement;
using Modules.LayoutManagement;
using Modules.SecurityManagement;
using Techlabs.Euphoria.Kernel.Modules.BookingManagement;
using Techlabs.Euphoria.Kernel.Modules.PaymentManagement;
using Techlabs.Euphoria.Kernel.Modules.SecurityManagement;
using Techlabs.Euphoria.Kernel.Modules.LeadGeneration;
using Techlabs.Euphoria.Kernel.Modules.LayoutManagement;
using Techlabs.Euphoria.Kernel.Modules.DiscountManagement;
using Techlabs.Euphoria.Kernel.Modules.MailManagement;

namespace Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework
{
    public class EuphoriaContext : DbContext
    {
        public EuphoriaContext() : base("EuphoriaContext")
        {
            //Database.SetInitializer<EuphoriaContext>(new DropCreateDatabaseIfModelChanges<EuphoriaContext>());
            Database.SetInitializer<EuphoriaContext>(null);

            //this.Database.Log = s => File.AppendAllLines(@"c:\DBQueries.txt",new string[] { s + Environment.NewLine });
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("gsmktg");
            //  modelBuilder.HasDefaultSchema("swabhav");
            //modelBuilder.HasDefaultSchema("azure");
        }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Exhibitor> Exhibitors { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventExhibitionMap> EventExhibitionMap { get; set; }
        public DbSet<EventTicketType> EventTicketType { get; set; }
        public DbSet<ExhibitorMap> ExhibitorMap { get; set; }
        public DbSet<Access> Access { get; set; }
        public DbSet<LayoutPlan> LayoutPlan { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<SectionType> SectionType { get; set; }
        public DbSet<Stall> Stall { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingRequest> BookingRequest { get; set; }
        public DbSet<BookingRequestStall> BookingRequestStall { get; set; }
        public DbSet<StallBooking> StallBooking { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<StallTransaction> StallTransaction { get; set; }
        public DbSet<EventTicket> EventTicket { get; set; }
        public DbSet<LoginSession> LoginSession { get; set; }
        public DbSet<LuckyDrawVisitor> LuckyDrawVisitor { get; set; }
        public DbSet<LuckyDrawEventTicket> LuckyDrawEventTicket { get; set; }
        public DbSet<GeneralMaster> GeneralMaster { get; set; }
        public DbSet<VisitorFeedback> VisitorFeedback { get; set; }
        public DbSet<BookingSalesPersonMap> BookingSalesPersonMap { get; set; }
        public DbSet<VisitorCardPayment> VisitorCardPayment { get; set; }
        public DbSet<ExhibitorType> ExhibitorType { get; set; }
        public DbSet<ExhibitorIndustryType> ExhibitorIndustryType { get; set; }
        public DbSet<ExhibitorFeedback> ExhibitorFeedback { get; set; }
        public DbSet<VisitorPaymentDetails> VisitorPaymentDetails { get; set; }
        public DbSet<EventLeadExhibitorMap> EventLeadExhibitorMaps { get; set; }
        public DbSet<BookingRequestSalesPersonMap> BookingRequestSalesPersonMaps { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<ChangePasswordRequest> ChangePasswordRequest { get; set; }
        public DbSet<BookingRequestInstallment> BookingRequestInstallments { get; set; }
        public DbSet<BookingInstallment> BookingInstallments { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<Barrier> Barriers { get; set; }
        public DbSet<SalesTarget> SalesTargets { get; set; }
        public DbSet<ExhibitorStatus> ExhibitorStatuses { get; set; }
        public DbSet<ExhibitorRegistrationType> ExhibitorRegistrationType { get; set; }
        public DbSet<ExhibitorEnquiry> ExhibitorEnquires { get; set; }
        public DbSet<BookingExhibitorDiscount> BookingExhibitorDiscount { get; set; }
        public DbSet<DiscountCoupon> DiscountCoupons { get; set; }
        public DbSet<DiscountType> DiscountTypes { get; set; }
        public DbSet<VisitorBookingExhibitorDiscount> VisitorBookingExhibitorDiscounts { get; set; }
        public DbSet<VisitorDiscountCouponMap> VisitorDiscountCouponMaps { get; set; }
        public DbSet<EmailType> EmailTypes { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
    }
}
