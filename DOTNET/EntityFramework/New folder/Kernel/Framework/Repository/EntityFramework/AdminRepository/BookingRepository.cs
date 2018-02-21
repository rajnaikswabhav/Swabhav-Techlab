using LinqKit;
using Modules.EventManagement;
using Modules.LayoutManagement;
using Modules.SecurityManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Model;
using Techlabs.Euphoria.Kernel.Modules.BookingManagement;
using Techlabs.Euphoria.Kernel.Modules.LeadGeneration;
using Techlabs.Euphoria.Kernel.Modules.PaymentManagement;
using Techlabs.Euphoria.Kernel.Modules.SecurityManagement;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework.AdminRepository
{
    public class BookingRepository<T> where T : AggregateEntity
    {

        public int CountOfBookingStalls(ISpecification<StallBooking> specifivation)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<StallBooking>().AsExpandable()
                                .Where(specifivation.Expression)
                                .AsQueryable().Count();
            }
        }

        public decimal SumOfTotalAmount(ISpecification<StallBooking> specifivation, string status)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<StallBooking>()
                    .AsExpandable()
                    .Where(specifivation.Expression)
                    .Where(x => x.Booking.Status.ToUpper() == status.ToUpper())
                    .AsQueryable().Sum(x => Convert.ToDecimal(x.Booking.TotalAmount));
            }
        }

        public int SumOfTotalAmountBooking(Partner userWhoisManager, Event currentEvent, string status)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<BookingSalesPersonMap>().AsExpandable()
                                          .Where(x => x.Login.Partner.Id == userWhoisManager.Id)
                                          .Where(x => x.Booking.Status.ToUpper().Equals(status.ToUpper()))
                                          .Where(x => x.Booking.Event.Id == currentEvent.Id)
                                          .AsQueryable().Sum(x => Convert.ToInt32(x.Booking.FinalAmountTax));
            }
        }

        public int SumOfTotalRecivedAmount(Partner userWhoisManager, Event currentEvent)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<PaymentDetails>().AsExpandable()
                                          .Where(x => x.Login.Partner.Id == userWhoisManager.Id)
                                          .Where(x => x.Event.Id == currentEvent.Id)
                                          .Where(x => x.IsPaymentApprove == true)
                                          .AsQueryable().Sum(x => Convert.ToInt32(x.AmountPaid));
            }
        }

        public double SumOfTotalPaidAmountByBooking(Booking booking, Event currentEvent)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<PaymentDetails>().AsExpandable()
                                          .Where(x => x.Booking.Id == booking.Id)
                                          .Where(x => x.Event.Id == currentEvent.Id)
                                          .Where(x => x.IsPaymentApprove == true)
                                          .AsQueryable().Sum(x => (double?)(x.AmountPaid)) ?? 0;
            }
        }

        public int StallBookingCount(Booking booking)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var stallCount = unitOfWork.DbContext.Set<StallBooking>().AsExpandable()
                                            .Where(x => x.Booking.Id == booking.Id).Count();

                return stallCount;
            }
        }

        public List<BookingRequestSalesPersonMap> FindBookingRequestOfReportingSalespersons(Partner userWhoisManager, Event currentEvent, int pageNumber, int pageSize)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var salesPesonBookingMap = unitOfWork.DbContext.Set<BookingRequestSalesPersonMap>().AsExpandable()
                                           .Where(x => x.Login.Partner.Id == userWhoisManager.Id)
                                           //.Where(x => x.Login.Id == userWhoisManager.Id)
                                           .Where(x => x.BookingRequest.Booking == null)
                                           .Where(x => x.BookingRequest.Event.Id == currentEvent.Id)
                                           .OrderByDescending(x => x.CreatedOn).Skip((pageNumber - 1) * pageSize)
                                                          .Take(pageSize);

                //var salesPersonWithEventFilter = salesPesonBookingMap
                //                                 .Where

                return salesPesonBookingMap.ToList();

            }
        }
        public List<BookingRequestSalesPersonMap> FindBookingRequestOfReportingSalespersons(Partner userWhoisManager, Event currentEvent, string exhibitorCompanyName, int pageNumber, int pageSize)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var salesPesonBookingMap = unitOfWork.DbContext.Set<BookingRequestSalesPersonMap>().AsExpandable()
                                           .Where(x => x.Login.Partner.Id == userWhoisManager.Id)
                                           .Where(x => x.BookingRequest.Exhibitor.CompanyName.Contains(exhibitorCompanyName) || x.BookingRequest.Exhibitor.Name.Contains(exhibitorCompanyName))
                                           .Where(x => x.BookingRequest.Booking == null)
                                           .Where(x => x.BookingRequest.Event.Id == currentEvent.Id)
                                           .OrderByDescending(x => x.CreatedOn).Skip((pageNumber - 1) * pageSize)
                                                          .Take(pageSize);
                return salesPesonBookingMap.ToList();
            }
        }

        public List<BookingSalesPersonMap> FindBookingOfReportingSalespersons(Partner userWhoisManager, Event currentEvent, int pageNumber, int pageSize)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var salesPesonBookingMap = unitOfWork.DbContext.Set<BookingSalesPersonMap>().AsExpandable()
                                           .Where(x => x.Login.Partner.Id == userWhoisManager.Id)
                                           //.Where(x => x.Login.Id == userWhoisManager.Id)
                                           .Where(x => x.Booking.Event.Id == currentEvent.Id)
                                           .OrderByDescending(x => x.CreatedOn).Skip((pageNumber - 1) * pageSize)
                                                          .Take(pageSize);

                return salesPesonBookingMap.ToList();
            }
        }
        public List<BookingSalesPersonMap> FindBookingOfReportingSalespersons(Partner userWhoisManager, Event currentEvent, string exhibitorName, int pageNumber, int pageSize)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var salesPesonBookingMap = unitOfWork.DbContext.Set<BookingSalesPersonMap>().AsExpandable()
                                           .Where(x => x.Login.Partner.Id == userWhoisManager.Id)
                                           .Where(x => x.Booking.Exhibitor.Name.Contains(exhibitorName) || x.Booking.Exhibitor.CompanyName.Contains(exhibitorName))
                                           .Where(x => x.Booking.Event.Id == currentEvent.Id)
                                           .OrderByDescending(x => x.CreatedOn).Skip((pageNumber - 1) * pageSize)
                                                          .Take(pageSize);

                return salesPesonBookingMap.ToList();
            }
        }

        public List<BookingRequest> FindBookingRequestOfExhibitor(string userWhoisManager, Event currentEvent, int pageNumber, int pageSize)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var salesPesonBookingMap = unitOfWork.DbContext.Set<BookingRequest>().AsExpandable()
                                           .Where(x => x.Exhibitor.Name.Contains(userWhoisManager))
                                           //.Where(x => x.Login.Id == userWhoisManager.Id)
                                           .Where(x => x.Booking.Event.Id == currentEvent.Id)
                                           .OrderByDescending(x => x.CreatedOn).Skip((pageNumber - 1) * pageSize)
                                           .Take(pageSize);
                return salesPesonBookingMap.ToList();
            }
        }

        public int FindBookingRequestOfReportingSalespersonsCount(Partner userWhoisManager, Event currentEvent)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var salesPesonBookingMapCount = unitOfWork.DbContext.Set<BookingRequestSalesPersonMap>().AsExpandable()
                                            .Where(x => x.Login.Partner.Id == userWhoisManager.Id)
                                            //.Where(x => x.Login.Id == userWhoisManager.Id)
                                            .Where(x => x.BookingRequest.Booking == null)
                                            .Where(x => x.BookingRequest.Event.Id == currentEvent.Id).Count();
                return salesPesonBookingMapCount;
            }
        }

        public int FindBookingRequestOfReportingSalespersonsCount(Partner userWhoisManager, Event currentEvent, string exhibitorCompanyName)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var salesPesonBookingMapCount = unitOfWork.DbContext.Set<BookingRequestSalesPersonMap>().AsExpandable()
                                            .Where(x => x.Login.Partner.Id == userWhoisManager.Id)
                                            .Where(x => x.BookingRequest.Exhibitor.CompanyName.Contains(exhibitorCompanyName) || x.BookingRequest.Exhibitor.Name.Contains(exhibitorCompanyName))
                                            .Where(x => x.BookingRequest.Booking == null)
                                            .Where(x => x.BookingRequest.Event.Id == currentEvent.Id).Count();
                return salesPesonBookingMapCount;
            }
        }

        public int FindBookingOfReportingSalespersonsCount(Partner userWhoisManager, Event currentEvent)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var salesPesonBookingMapCount = unitOfWork.DbContext.Set<BookingSalesPersonMap>().AsExpandable()
                                            .Where(x => x.Login.Partner.Id == userWhoisManager.Id)
                                            //.Where(x => x.Login.Id == userWhoisManager.Id)
                                            .Where(x => x.Booking.Event.Id == currentEvent.Id).Count();
                return salesPesonBookingMapCount;
            }
        }

        public int FindBookingOfReportingSalespersonsCount(Partner userWhoisManager, Event currentEvent, string exhibitorName)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var salesPesonBookingMapCount = unitOfWork.DbContext.Set<BookingSalesPersonMap>().AsExpandable()
                                            .Where(x => x.Login.Partner.Id == userWhoisManager.Id)
                                            .Where(x => x.Booking.Exhibitor.Name.Contains(exhibitorName) || x.Booking.Exhibitor.CompanyName.Contains(exhibitorName))
                                            .Where(x => x.Booking.Event.Id == currentEvent.Id).Count();
                return salesPesonBookingMapCount;
            }
        }

        public int FindExhibitorListByEventLeadMapCount(Partner userWhoisManager, Event currentEvent)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var salesPesonBookingMapCount = unitOfWork.DbContext.Set<EventLeadExhibitorMap>().AsExpandable()
                                            .Where(x => x.Login.Partner.Id == userWhoisManager.Id)
                                            //.Where(x => x.Login.Id == userWhoisManager.Id)
                                            .Where(x => x.Event.Id == currentEvent.Id).Count();
                return salesPesonBookingMapCount;
            }
        }

        public List<EventLeadExhibitorMap> FindExhibitorListByEventLeadMap(Partner userWhoisManager, Event currentEvent, int pageNumber, int pageSize)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var salesPesonBookingMap = unitOfWork.DbContext.Set<EventLeadExhibitorMap>().AsExpandable()
                                           .Where(x => x.Login.Partner.Id == userWhoisManager.Id)
                                           //.Where(x => x.Login.Id == userWhoisManager.Id)
                                           .Where(x => x.Event.Id == currentEvent.Id)
                                           .OrderByDescending(x => x.CreatedOn).Skip((pageNumber - 1) * pageSize)
                                                          .Take(pageSize);

                return salesPesonBookingMap.ToList();
            }
        }

        public int CountOfBookingRequestStalls(ISpecification<BookingRequestStall> specifivation)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<BookingRequestStall>().AsExpandable()
                                .Where(specifivation.Expression)
                                .AsQueryable().Count();
            }
        }

        public int CountOfStallsBySize(ISpecification<Stall> specifivation)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<Stall>().AsExpandable()
                                .Where(specifivation.Expression)
                                .AsQueryable().Count();
            }
        }

        public List<int?> DistinctStallSize(Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Stall
                                .Where(x => x.Event.Id.Equals(eventId))
                                .Select(row => row.StallSize)
                                .Distinct().ToList();
            }

        }

        public List<ExhibitorType> DistinctIndustryType()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Exhibitors
                                .Select(row => row.ExhibitorType)
                                .Distinct().OrderByDescending(row => row.Type).ToList();
            }
        }
        public int NullExhibitorTypeExhibitors()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Exhibitors
                    .Where(row => row.ExhibitorType == null)
                    .Count();
            }
        }

        public List<Exhibitor> NullExhibitorTypeExhibitorDetails(int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Exhibitors
                    .Where(row => row.ExhibitorType == null)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
        }

        public List<Exhibitor> NullExhibitorIndustryTypeExhibitorDetails(int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Exhibitors
                    .Where(row => row.ExhibitorIndustryType == null)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
        }

        public List<ExhibitorIndustryType> DistinctExhibitorIndustryType()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Exhibitors
                                .Select(row => row.ExhibitorIndustryType)
                                .Distinct().OrderByDescending(row => row.IndustryType).ToList();
            }
        }

        public int NullExhibitorIndustryTypeExhibitors()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Exhibitors
                    .Where(row => row.ExhibitorIndustryType == null)
                    .Count();
            }
        }

        public List<Country> DistinctLocation()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Exhibitors
                                .Select(row => row.Country)
                                .Distinct().OrderByDescending(row => row.Name).ToList();
            }
        }
        public int NullLocationExhibitors()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Exhibitors
                    .Where(row => row.Country == null)
                    .Count();
            }
        }

        public List<Exhibitor> NullLocatiocationExhibitorDetails(int pageSize, int pageNumber)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Exhibitors
                    .Where(row => row.Country == null)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
        }

        public List<string> DistinctCountry()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Exhibitors
                                .Select(row => row.Country.Name)
                                .Distinct().ToList();
            }
        }

        public int countOfBookedStallsBySalesPerson(Guid salesPersonId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var qry = unitOfWork.DbContext.BookingSalesPersonMap
                    .Join(unitOfWork.DbContext.StallBooking,
                    post => post.Booking.Id,
                    meta => meta.Booking.Id,
                    (post, meta) => new { Post = post, Meta = meta.Stall })
                    .Where(postAndMeta => postAndMeta.Post.Login.Id == salesPersonId).Where(x => x.Post.Booking.Event.Id == eventId);

                return qry.Count();
            }
        }

        public int countOfBookedStallsByExhibitorIndustry(Guid industryId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var qry = unitOfWork.DbContext.BookingSalesPersonMap
                    .Join(unitOfWork.DbContext.StallBooking,
                    post => post.Booking.Id,
                    meta => meta.Booking.Id,
                    (post, meta) => new { Post = post, Meta = meta.Stall })
                    .Where(postAndMeta => postAndMeta.Post.Booking.Exhibitor.ExhibitorIndustryType.Id == industryId).Where(x => x.Post.Booking.Event.Id == eventId);

                return qry.Count();
            }
        }

        public int countOfBookedStallsByCountry(Guid countryId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var qry = unitOfWork.DbContext.BookingSalesPersonMap
                    .Join(unitOfWork.DbContext.StallBooking,
                    post => post.Booking.Id,
                    meta => meta.Booking.Id,
                    (post, meta) => new { Post = post, Meta = meta.Stall })
                    .Where(postAndMeta => postAndMeta.Post.Booking.Exhibitor.Country.Id == countryId).Where(x => x.Post.Booking.Event.Id == eventId);

                return qry.Count();
            }
        }

        public int countOfBookedStallsByState(Guid stateId, Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var qry = unitOfWork.DbContext.BookingSalesPersonMap
                    .Join(unitOfWork.DbContext.StallBooking,
                    post => post.Booking.Id,
                    meta => meta.Booking.Id,
                    (post, meta) => new { Post = post, Meta = meta.Stall })
                    .Where(postAndMeta => postAndMeta.Post.Booking.Exhibitor.State.Id == stateId).Where(x => x.Post.Booking.Event.Id == eventId);

                return qry.Count();
            }
        }

        public int CountOfReservedStalls(ISpecification<Stall> specifivation)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var qry = unitOfWork.DbContext.Stall.GroupJoin(
                            unitOfWork.DbContext.StallBooking,
                            st => st.Id,
                            stb => stb.Stall.Id,
                            (x, y) => new { Foo = x, Bars = y })
                            .SelectMany(
                            x => x.Bars.DefaultIfEmpty(),
                            (x, y) => new { Foo = x.Foo })
                            .Where(t => t.Foo.Id == null);

                return qry.Count();
            }
        }

        public int LayoutData(ISpecification<LayoutPlan> specifivation)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var qry = (from c in unitOfWork.DbContext.LayoutPlan
                           select new Section
                           {
                               //Sectionlist = c.SelectMany(),
                               //totalOrders = c.Orders.Count(),
                               //totalOrderItems = c.Orders.SelectMany(o => o.OrderItems).Count()
                           }).ToList();

                return qry.Count();
            }
        }
        public int StallDataByPayment(ISpecification<Booking> specifivation)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var qry = unitOfWork.DbContext.Set<Booking>().AsExpandable()
                    //.Select(x=>x.)            
                    .Where(specifivation.Expression)
                                .AsQueryable().Count();

                return qry;
            }
        }

        public int StallDataBySalesPerson(ISpecification<Booking> specifivation)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var qry = unitOfWork.DbContext.Set<Booking>().AsExpandable()
                    //.Select(x=>x.)            
                    .Where(specifivation.Expression)
                    .AsQueryable().Count();
                return qry;
            }
        }

        public IList<Venue> DistinctMarketIntrested(ISpecification<ExhibitorFeedback> specifivation)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<ExhibitorFeedback>()
                    .AsExpandable()
                    .Where(specifivation.Expression)
                    .SelectMany(row => row.MarketIntrested).Distinct().ToList();
            }
        }

        //public int CountOfReservedStallss(ISpecification<Stall> specifivation)
        //{
        //    using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
        //    {
        //        var qry = from c in unitOfWork.DbContext.Stall
        //                  join o in unitOfWork.DbContext.StallBooking
        //                     on c.Id equals o.Stall.Id into sr
        //                  from x in sr.DefaultIfEmpty()
        //                  .Where(st => st.Stall.Id == null);

        //        return qry.Count();
        //    }
        //}

    }
}
