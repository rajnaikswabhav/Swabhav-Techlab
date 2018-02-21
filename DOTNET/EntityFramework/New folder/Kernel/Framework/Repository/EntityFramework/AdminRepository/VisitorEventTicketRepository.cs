using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using LinqKit;
using System.Linq.Expressions;
using Techlabs.Euphoria.Kernel.Model;
using Data = System.Collections.Generic.KeyValuePair<string, int>;
using Techlabs.Euphoria.Kernel.Modules.DiscountManagement;
using Modules.SecurityManagement;

namespace Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework.AdminRepository
{
    public class VisitorEventTicketRepository<T> where T : AggregateEntity
    {
        public Guid Add(EventTicket entity)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                unitOfWork.DbContext.Set<EventTicket>().Add(entity);
                //  unitOfWork.DbContext.SaveChanges();
                unitOfWork.SaveChanges();
                return entity.Id;
            }
        }

        public virtual void Update(EventTicket entity)
        {
            //Nothing to do here. Entity framework will automatically save changed entities
        }

        public void Delete(Guid entityId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                unitOfWork.DbContext.Set<EventTicket>().Remove(GetById(entityId));
                //  unitOfWork.DbContext.SaveChanges();

                unitOfWork.SaveChanges();
            }
        }

        public EventTicket GetById(Guid entityId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>().SingleOrDefault(x => x.Id == entityId);
            }
        }

        public IList<EventTicket> Find(ISpecification<EventTicket> specification)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryable = unitOfWork.DbContext.Set<EventTicket>().AsExpandable().Where(specification.Expression);
                return queryable.ToList();
            }
        }

        public IList<EventTicket> Get()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>().ToList();
            }
        }

        //public int Count(ISpecification<T> specifivation)
        //{
        //    using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
        //    {
        //        return unitOfWork.DbContext.Set<T>().Count();
        //    }
        //}

        public int CountOfVisitors(ISpecification<EventTicket> specifivation, Expression<Func<EventTicket, object>> distinctExpression)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryable = unitOfWork.DbContext.Set<EventTicket>().AsExpandable().Where(specifivation.Expression).Select(distinctExpression).AsQueryable().Distinct();

                return queryable.Count();
            }
        }
        public IList<Visitor> TotalVisitorsList(ISpecification<EventTicket> specifivation, Expression<Func<EventTicket, object>> distinctExpression)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryable = unitOfWork.DbContext.Set<EventTicket>().AsExpandable().Where(specifivation.Expression).Select(x => x.Visitor).AsQueryable().Distinct();

                return queryable.ToList();
            }
        }

        public int DiscountVisitorsCount(ISpecification<VisitorDiscountCouponMap> specifivation, Expression<Func<VisitorDiscountCouponMap, object>> distinctExpression)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryable = unitOfWork.DbContext.Set<VisitorDiscountCouponMap>().AsExpandable().Where(specifivation.Expression).Select(distinctExpression).AsQueryable().Distinct();

                return queryable.Count();
            }
        }

        public int VisitorsCount(ISpecification<EventTicket> specifivation, Expression<Func<EventTicket, object>> distinctExpression)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryable = unitOfWork.DbContext.Set<EventTicket>().AsExpandable().Where(specifivation.Expression).Select(distinctExpression).AsQueryable().Distinct();

                return queryable.Count();
            }
        }
        public int? SumOfDiscountCouponTickets(ISpecification<VisitorDiscountCouponMap> specification)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<VisitorDiscountCouponMap>()
                    .AsExpandable()
                    .Where(specification.Expression)
                    .Where(x => x.EventTicket.PaymentCompleted == true)
                    .AsQueryable().Sum(x => (int?)x.EventTicket.NumberOfTicket);
            }
        }

        public IDictionary<string, int> VisitorsCardPayment(ISpecification<VisitorCardPayment> specifivation, Expression<Func<VisitorCardPayment, string>> distinctExpression)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryable = unitOfWork.DbContext.Set<VisitorCardPayment>().AsExpandable().Where(specifivation.Expression).Select(e => e.MobileNo).AsQueryable().Distinct();
                var queryables = unitOfWork.DbContext.Set<VisitorCardPayment>().AsExpandable().Where(specifivation.Expression)
                    .GroupBy(x => x.MobileNo).Select(y => new
                    {
                        mobile = y.Key,
                        totalAmount = y.Sum(t => t.Amount)
                    }).OrderByDescending(i => i.totalAmount);
                //return queryable.ToList().ForEach(x=>x.Key);
                return queryables.ToDictionary(x => x.mobile, x => x.totalAmount);
            }
        }

        public IDictionary<Guid, int> ExhibitorsCardPayment(ISpecification<VisitorCardPayment> specifivation, Expression<Func<VisitorCardPayment, Guid>> distinctExpression)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryables = unitOfWork.DbContext.Set<VisitorCardPayment>().AsExpandable().Where(specifivation.Expression)
                    .GroupBy(x => x.Exhibitor.Id).Select(y => new
                    {
                        mobile = y.Key,
                        totalAmount = y.Sum(t => t.Amount)
                    }).OrderByDescending(i => i.totalAmount);
                //return queryable.ToList().ForEach(x=>x.Key);
                return queryables.ToDictionary(x => x.mobile, x => x.totalAmount);
            }
        }

        public int CardPaymentCount(ISpecification<VisitorCardPayment> specifivation, Expression<Func<VisitorCardPayment, object>> distinctExpression)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryable = unitOfWork.DbContext.Set<VisitorCardPayment>().AsExpandable().Where(specifivation.Expression).Select(distinctExpression).AsQueryable().Distinct();

                return queryable.Count();
            }
        }

        public int ExhibitorCardPaymentCount(ISpecification<VisitorCardPayment> specifivation, Expression<Func<VisitorCardPayment, Guid>> distinctExpression)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryable = unitOfWork.DbContext.Set<VisitorCardPayment>().AsExpandable().Where(specifivation.Expression).Select(distinctExpression).AsQueryable().Distinct();

                return queryable.Count();
            }
        }

        public IList<Guid> CardPaymentDistinctLogin(Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryable = unitOfWork.DbContext.Set<VisitorCardPayment>()
                                .AsExpandable()
                                .Where(x => x.Event.Id.Equals(eventId))
                                .Select(x => x.Login.Id).AsQueryable().Distinct().ToList();

                return queryable;
            }
        }

        public int GetTotalVisitorsOfValidatedPincode(Guid eventId, int pincode)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryable = unitOfWork.DbContext.Set<EventTicket>()
                                .AsExpandable()
                                .Where(x => x.EventTicketType.Event.Id.Equals(eventId))
                                .Where(x => x.Visitor.Pincode.Equals(pincode))
                                .Where(x => x.ValidityDayCount == 0)
                                .Select(x => x.Visitor.MobileNo).AsQueryable().Distinct().Count();

                return queryable;
            }
        }

        public int GetTotalVisitorsOfValidatedPincode(Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryable = unitOfWork.DbContext.Set<EventTicket>()
                                .AsExpandable()
                                .Where(x => x.EventTicketType.Event.Id.Equals(eventId))
                                .Where(x => x.ValidityDayCount == 0)
                                .Select(x => x.Visitor.MobileNo).AsQueryable().Distinct().Count();

                return queryable;
            }
        }


        public int? GetTotalEventTicketCountOfValidatedPincode(Guid eventId, int pincode)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryable = unitOfWork.DbContext.Set<EventTicket>()
                                .AsExpandable()
                                .Where(x => x.EventTicketType.Event.Id.Equals(eventId))
                                .Where(x => x.Visitor.Pincode.Equals(pincode))
                                .Where(x => x.ValidityDayCount == 0)
                                .Sum(x => (int?)x.NumberOfTicket);

                return queryable;
            }
        }

        public int? GetTotalEventTicketCountOfValidatedPincode(Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryable = unitOfWork.DbContext.Set<EventTicket>()
                                .AsExpandable()
                                .Where(x => x.EventTicketType.Event.Id.Equals(eventId))
                                .Where(x => x.ValidityDayCount == 0)
                                .Sum(x => (int?)x.NumberOfTicket);

                return queryable;
            }
        }

        public IList<int> GetPincodeListOfValidatedTicket(Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>()
                                .AsExpandable()
                                .Where(x => x.EventTicketType.Event.Id.Equals(eventId))
                                .Where(x => x.ValidityDayCount == 0)
                                .Select(x => x.Visitor.Pincode).Distinct().ToList();
            }
        }

        public int? TotalCardPaymentOfDay(Guid eventId, DateTime transactionDate, Guid loginId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                TimeSpan ts = new TimeSpan(00, 00, 00);
                DateTime StartDateForSearch = transactionDate.Date + ts;

                TimeSpan timeSpan = new TimeSpan(23, 59, 59);
                DateTime EndDateForSearch = transactionDate.Date + timeSpan;

                return unitOfWork.DbContext.Set<VisitorCardPayment>()
                    .AsExpandable()
                    .Where(x => x.Event.Id.Equals(eventId))
                    .Where(x => x.TransactionDate >= StartDateForSearch && x.TransactionDate <= EndDateForSearch)
                    .Where(x => x.Login.Id.Equals(loginId))
                    .AsQueryable().Sum(x => (int?)x.Amount);

            }
        }

        public int? TotalCardPaymentOfAllDay(Guid eventId, DateTime transactionDate)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                TimeSpan ts = new TimeSpan(00, 00, 00);
                DateTime StartDateForSearch = transactionDate.Date + ts;

                TimeSpan timeSpan = new TimeSpan(23, 59, 59);
                DateTime EndDateForSearch = transactionDate.Date + timeSpan;

                return unitOfWork.DbContext.Set<VisitorCardPayment>()
                    .AsExpandable()
                    .Where(x => x.Event.Id.Equals(eventId))
                    .Where(x => x.TransactionDate >= StartDateForSearch && x.TransactionDate <= EndDateForSearch)
                    .AsQueryable().Sum(x => (int?)x.Amount);
            }
        }

        public int? TotalCardPaymentOfEventByLogin(Guid eventId, Guid loginId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<VisitorCardPayment>()
                    .AsExpandable()
                    .Where(x => x.Event.Id.Equals(eventId))
                    .Where(x => x.Login.Id.Equals(loginId))
                    .AsQueryable().Sum(x => (int?)x.Amount);

            }
        }


        public int? TotalCardPaymentOfEvent(Guid eventId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<VisitorCardPayment>()
                    .AsExpandable()
                    .Where(x => x.Event.Id.Equals(eventId))
                    .AsQueryable().Sum(x => (int?)x.Amount);

            }
        }

        public double? SumOfTicketsPrice(ISpecification<EventTicket> specifivation, Expression<Func<EventTicket, double?>> distinctExpression)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>()
                    .AsExpandable()
                    .Where(specifivation.Expression)
                    .Where(x => x.PaymentCompleted)
                    .AsQueryable().Sum(distinctExpression);
            }
        }

        public int? SumOfTickets(ISpecification<EventTicket> specification)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>()
                    .AsExpandable()
                    .Where(specification.Expression)
                    .Where(x => x.PaymentCompleted == true)
                    .AsQueryable().Sum(x => (int?)x.NumberOfTicket);
            }
        }


        public int? SumOfSingleDiscountCouponAddedTickets(ISpecification<VisitorDiscountCouponMap> specification)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<VisitorDiscountCouponMap>()
                    .AsExpandable()
                    .Where(specification.Expression)
                    .Where(x => x.EventTicket.PaymentCompleted == true)
                    .AsQueryable().Sum(x => (int?)x.EventTicket.NumberOfTicket);
            }
        }

        public int? SumOfMobileTickets(ISpecification<EventTicket> specification)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>()
                    .AsExpandable()
                    .Where(specification.Expression)
                    .Where(x => x.PaymentCompleted)
                    .Where(x => x.Device == 2 || x.Device == 3)
                    //.Where()
                    .AsQueryable()
                    .Sum(x => (int?)x.NumberOfTicket);
            }
        }

        public double? SumOfMobileTicketsPrice(ISpecification<EventTicket> specifivation, Expression<Func<EventTicket, double?>> distinctExpression)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>()
                    .AsExpandable()
                    .Where(specifivation.Expression)
                    .Where(x => x.PaymentCompleted)
                    .Where(x => x.Device == 2 || x.Device == 3)
                    .AsQueryable().Sum(distinctExpression);
            }
        }


        public int? SumOfWebTickets(ISpecification<EventTicket> specification)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>()
                    .AsExpandable()
                    .Where(specification.Expression)
                    .Where(x => x.Device == 1 && x.PaymentCompleted == true)
                    .AsQueryable().Sum(x => (int?)x.NumberOfTicket);
            }
        }

        public double? SumOfWebTicketsPrice(ISpecification<EventTicket> specifivation, Expression<Func<EventTicket, double?>> distinctExpression)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>()
                    .AsExpandable()
                    .Where(specifivation.Expression)
                    .Where(x => x.PaymentCompleted)
                    .Where(x => x.Device == 1)
                    .AsQueryable().Sum(distinctExpression);
            }
        }

        public int? SumOfOnlineTickets(ISpecification<EventTicket> specification)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>()
                    .AsExpandable()
                    .Where(specification.Expression)
                    .Where(x => x.IsPayOnLocation == false && x.PaymentCompleted == true)
                    .AsQueryable().Sum(x => (int?)x.NumberOfTicket);
            }
        }

        public double? SumOfOnlineTicketsPrice(ISpecification<EventTicket> specifivation, Expression<Func<EventTicket, double?>> distinctExpression)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>()
                    .AsExpandable()
                    .Where(specifivation.Expression)
                    .Where(x => x.PaymentCompleted)
                    .Where(x => x.IsPayOnLocation == false)
                    .AsQueryable().Sum(distinctExpression);
            }
        }

        public int? SumOfPayatLocationTickets(ISpecification<EventTicket> specification)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>()
                    .AsExpandable()
                    .Where(specification.Expression)
                    .Where(x => x.IsPayOnLocation == true && x.PaymentCompleted == true)
                    .AsQueryable().Sum(x => (int?)x.NumberOfTicket);
            }
        }
        public double? SumOfPayAtLocationTicketsPrice(ISpecification<EventTicket> specifivation, Expression<Func<EventTicket, double?>> distinctExpression)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>()
                    .AsExpandable()
                    .Where(specifivation.Expression)
                    .Where(x => x.PaymentCompleted)
                    .Where(x => x.IsPayOnLocation == true)
                    .AsQueryable().Sum(distinctExpression);
            }
        }

        public int? SumOfIphoneTickets(ISpecification<EventTicket> specification)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>()
                    .AsExpandable()
                    .Where(specification.Expression)
                    .Where(x => x.Device == 3 && x.PaymentCompleted == true)
                    .AsQueryable().Sum(x => (int?)x.NumberOfTicket);
            }
        }

        public double? SumOfIphoneTicketsPrice(ISpecification<EventTicket> specifivation, Expression<Func<EventTicket, double?>> distinctExpression)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>()
                    .AsExpandable()
                    .Where(specifivation.Expression)
                    .Where(x => x.PaymentCompleted)
                    .Where(x => x.Device == 3)
                    .AsQueryable().Sum(distinctExpression);
            }
        }
        public int? SumOfAndroidTickets(ISpecification<EventTicket> specification)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>()
                    .AsExpandable()
                    .Where(specification.Expression)
                    .Where(x => x.Device == 2 && x.PaymentCompleted == true)
                    .AsQueryable().Sum(x => (int?)x.NumberOfTicket);
            }
        }

        public double? SumOfAndroidTicketsPrice(ISpecification<EventTicket> specifivation, Expression<Func<EventTicket, double?>> distinctExpression)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<EventTicket>()
                    .AsExpandable()
                    .Where(specifivation.Expression)
                    .Where(x => x.PaymentCompleted)
                    .Where(x => x.Device == 2)
                    .AsQueryable().Sum(distinctExpression);
            }
        }

        public IList<Category> DistinctCategory(ISpecification<VisitorFeedback> specifivation)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<VisitorFeedback>()
                    .AsExpandable()
                    .Where(specifivation.Expression)
                    .SelectMany(row => row.Categories).Distinct().ToList();
            }
        }

        public IList<Country> DistinctCountry(ISpecification<VisitorFeedback> specifivation)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<VisitorFeedback>()
                    .AsExpandable()
                    .Where(specifivation.Expression)
                    .SelectMany(row => row.Countries).Distinct().ToList();
            }
        }
        public IList<ExhibitorType> DistinctExhibitorType(ISpecification<VisitorFeedback> specifivation)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<VisitorFeedback>()
                    .AsExpandable()
                    .Where(specifivation.Expression)
                    .SelectMany(row => row.ExhibitorTypes).Distinct().ToList();
            }
        }

    }
}
