using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class EventTicketReportSpecificationForSearch : ISpecification<EventTicket>
    {
        EventTicketReportSearchCriteria _criteria;

        public EventTicketReportSpecificationForSearch(EventTicketReportSearchCriteria criteria)
        {

            _criteria = criteria;

        }
        public Expression<Func<EventTicket, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<EventTicket>();


                if (_criteria.StartDate != DateTime.MinValue && _criteria.EndDate != DateTime.MinValue && _criteria.adminId == Guid.Empty && _criteria.IsMobile == false && _criteria.IsPayatLocation == false && _criteria.IsPayOnline == false && _criteria.IsWeb == false && _criteria.PaymentCompleted == false)
                {
                    builder = builder.And(x => x.TicketDate >= _criteria.StartDate && x.TicketDate <= _criteria.EndDate).And(x => x.CreatedBy == null);
                }

                if (_criteria.StartDate != DateTime.MinValue && _criteria.EndDate != DateTime.MinValue && _criteria.PaymentCompleted == true && _criteria.adminId == Guid.Empty && _criteria.IsMobile == false && _criteria.IsPayatLocation == false && _criteria.IsPayOnline == false && _criteria.IsWeb == false)
                {
                    builder = builder.And(x => x.TicketDate >= _criteria.StartDate && x.TicketDate <= _criteria.EndDate).And(x => x.PaymentCompleted).And(x => x.CreatedBy == null);
                }

                if (_criteria.StartDateForSearch != DateTime.MinValue && _criteria.EndDateForSearch != DateTime.MinValue && _criteria.PaymentCompleted == true && _criteria.adminId == Guid.Empty && _criteria.IsMobile == false && _criteria.IsPayatLocation == false && _criteria.IsPayOnline == false && _criteria.IsWeb == false)
                {
                    TimeSpan ts = new TimeSpan(00, 00, 00);
                    DateTime StartDateForSearch = _criteria.StartDateForSearch.Date + ts;

                    TimeSpan timeSpan = new TimeSpan(23, 59, 59);
                    DateTime EndDateForSearch = _criteria.EndDateForSearch.Date + timeSpan;

                    builder = builder.And(x => x.CreatedOn >= StartDateForSearch && x.CreatedOn <= EndDateForSearch).And(x => x.PaymentCompleted).And(x => x.CreatedBy == null);
                }

                if (_criteria.StartDate != DateTime.MinValue && _criteria.EndDate != DateTime.MinValue && _criteria.PaymentCompleted == true && _criteria.adminId == Guid.Empty && _criteria.IsMobile == true && _criteria.IsPayatLocation == false && _criteria.IsPayOnline == false && _criteria.IsWeb == false)
                {
                    builder = builder.And(x => x.TicketDate >= _criteria.StartDate && x.TicketDate <= _criteria.EndDate).And(x => x.PaymentCompleted).And(x => x.Visitor.isMobileNoVerified == true).And(x => x.CreatedBy == null);
                }

                if (_criteria.StartDate != DateTime.MinValue && _criteria.EndDate != DateTime.MinValue && _criteria.PaymentCompleted == true && _criteria.adminId == Guid.Empty && _criteria.IsMobile == false && _criteria.IsPayatLocation == false && _criteria.IsPayOnline == false && _criteria.IsWeb == true)
                {
                    builder = builder.And(x => x.TicketDate >= _criteria.StartDate && x.TicketDate <= _criteria.EndDate).And(x => x.PaymentCompleted).And(x => x.Visitor.isMobileNoVerified == false).And(x => x.CreatedBy == null);
                }

                if (_criteria.StartDate != DateTime.MinValue && _criteria.EndDate != DateTime.MinValue && _criteria.PaymentCompleted == true && _criteria.adminId == Guid.Empty && _criteria.IsMobile == false && _criteria.IsPayatLocation == false && _criteria.IsPayOnline == true && _criteria.IsWeb == false)
                {
                    builder = builder.And(x => x.TicketDate >= _criteria.StartDate && x.TicketDate <= _criteria.EndDate).And(x => x.PaymentCompleted).And(x => x.IsPayOnLocation == false).And(x => x.CreatedBy == null);
                }

                if (_criteria.StartDate != DateTime.MinValue && _criteria.EndDate != DateTime.MinValue && _criteria.PaymentCompleted == true && _criteria.adminId == Guid.Empty && _criteria.IsMobile == false && _criteria.IsPayatLocation == true && _criteria.IsPayOnline == false && _criteria.IsWeb == false)
                {
                    builder = builder.And(x => x.TicketDate >= _criteria.StartDate && x.TicketDate <= _criteria.EndDate).And(x => x.PaymentCompleted).And(x => x.IsPayOnLocation).And(x => x.CreatedBy == null);
                }


                if (_criteria.StartDate != DateTime.MinValue && _criteria.EndDate != DateTime.MinValue && _criteria.adminId != Guid.Empty && _criteria.IsMobile == false && _criteria.IsPayatLocation == false && _criteria.IsPayOnline == false && _criteria.IsWeb == false && _criteria.PaymentCompleted == false)
                {
                    builder = builder.And(x => x.TicketDate >= _criteria.StartDate && x.TicketDate <= _criteria.EndDate).And(x => x.CreatedBy == _criteria.adminId);
                }

                if (_criteria.SingleDate != DateTime.MinValue && _criteria.StartDate == DateTime.MinValue && _criteria.EndDate == DateTime.MinValue && _criteria.adminId == Guid.Empty && _criteria.IsMobile == true && _criteria.IsPayatLocation == false && _criteria.IsPayOnline == false && _criteria.IsWeb == false && _criteria.PaymentCompleted == false)
                {
                    TimeSpan ts = new TimeSpan(00, 00, 00);
                    DateTime ticketDate = _criteria.SingleDate.Date + ts;
                    builder = builder.And(x => x.TicketDate == ticketDate).And(x => x.CreatedBy == null).And(x => x.PaymentCompleted == true);
                    //.And(x => x.PaymentCompleted == true).And(x => x.Visitor.isMobileNoVerified == true);
                }

                if (_criteria.SingleDate != DateTime.MinValue && _criteria.StartDate == DateTime.MinValue && _criteria.EndDate == DateTime.MinValue && _criteria.adminId == Guid.Empty && _criteria.IsMobile == false && _criteria.IsPayatLocation == true && _criteria.IsPayOnline == false && _criteria.IsWeb == false && _criteria.PaymentCompleted == false)
                {
                    TimeSpan ts = new TimeSpan(23, 59, 59);
                    DateTime EndDate = _criteria.SingleDate.Date + ts;
                    builder = builder.And(x => x.TicketDate >= _criteria.SingleDate && x.TicketDate <= EndDate).And(x => x.CreatedBy == null).And(x => x.PaymentCompleted == true).And(x => x.Visitor.isMobileNoVerified == false);
                }

                if (_criteria.SingleDate != DateTime.MinValue && _criteria.StartDate == DateTime.MinValue && _criteria.EndDate == DateTime.MinValue && _criteria.adminId == Guid.Empty && _criteria.IsMobile == false && _criteria.IsPayatLocation == false && _criteria.IsPayOnline == true && _criteria.IsWeb == false && _criteria.PaymentCompleted == false)
                {
                    TimeSpan ts = new TimeSpan(00, 00, 00);
                    DateTime EndDate = _criteria.SingleDate.Date + ts;
                    builder = builder.And(x => x.TicketDate == _criteria.SingleDate && x.TicketDate <= EndDate).And(x => x.CreatedBy == null).And(x => x.PaymentCompleted == true).And(x => x.IsPayOnLocation == false);
                }

                if (_criteria.SingleDate != DateTime.MinValue && _criteria.StartDate == DateTime.MinValue && _criteria.EndDate == DateTime.MinValue && _criteria.adminId == Guid.Empty && _criteria.IsMobile == false && _criteria.IsPayatLocation == false && _criteria.IsPayOnline == false && _criteria.IsWeb == true && _criteria.PaymentCompleted == false)
                {
                    TimeSpan ts = new TimeSpan(23, 59, 59);
                    DateTime EndDate = _criteria.SingleDate.Date + ts;
                    builder = builder.And(x => x.TicketDate >= _criteria.SingleDate && x.TicketDate <= EndDate).And(x => x.CreatedBy == null).And(x => x.PaymentCompleted == true).And(x => x.IsPayOnLocation == true);
                }
                if (_criteria.EventId != Guid.Empty && (_criteria.Pincode != null || _criteria.Pincode != 0))
                {
                    builder = builder.And(x => x.EventTicketType.Event.Id == _criteria.EventId).And(x => x.CreatedBy == null).And(x => x.Visitor.Pincode == _criteria.Pincode);
                }

                return builder;
            }
        }
    }
}
