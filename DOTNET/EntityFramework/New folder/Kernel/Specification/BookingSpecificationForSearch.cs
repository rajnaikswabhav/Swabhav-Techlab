using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Modules.BookingManagement;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class BookingSpecificationForSearch : ISpecification<Booking>
    {
        private BookingSearchCriteria _criteria;

        public BookingSpecificationForSearch(BookingSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<Booking, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Booking>();

                if (!String.IsNullOrEmpty(_criteria.CompanyName))
                {
                    builder = builder.And(x => x.Exhibitor.CompanyName.Contains(_criteria.CompanyName) || x.Exhibitor.Name.Contains(_criteria.CompanyName));
                }

                if (!String.IsNullOrEmpty(_criteria.Category))
                {
                    builder = builder.And(x => x.Exhibitor.Categories.Any(y => y.Name.Contains(_criteria.Category)));
                }

                if (!String.IsNullOrEmpty(_criteria.Country))
                {
                    builder = builder.And(x => x.Exhibitor.Country.Name.Contains(_criteria.Country));
                }

                if (!String.IsNullOrEmpty(_criteria.State))
                {
                    builder = builder.And(x => x.Exhibitor.State.Name.Contains(_criteria.State));
                }

                if (!String.IsNullOrEmpty(_criteria.Pavilion))
                {
                    builder = builder.And(x => x.Section.Name.Contains(_criteria.Pavilion));
                }

                if (_criteria.PavilionId != Guid.Empty)
                {
                    builder = builder.And(x => x.Section.Id.Equals(_criteria.PavilionId));
                }

                if (_criteria.SalesPersonId != Guid.Empty)
                {
                    builder = builder.And(x => x.Login.Id.Equals(_criteria.SalesPersonId));
                }

                if (_criteria.ExhibitorIndustryId != Guid.Empty)
                {
                    builder = builder.And(x => x.Exhibitor.ExhibitorIndustryType.Id.Equals(_criteria.ExhibitorIndustryId));
                }

                if (_criteria.EventId != Guid.Empty && _criteria.StallId == Guid.Empty && _criteria.Status == null && _criteria.CountryId == Guid.Empty && _criteria.StartDate == DateTime.MinValue && _criteria.EndDate == DateTime.MinValue && string.IsNullOrEmpty(_criteria.BookingReqestId))
                {
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId));
                }

                if (_criteria.EventId != Guid.Empty && _criteria.StallId == Guid.Empty && _criteria.Status == null && _criteria.CountryId == Guid.Empty && _criteria.StartDate == DateTime.MinValue && _criteria.EndDate == DateTime.MinValue && !string.IsNullOrEmpty(_criteria.BookingReqestId))
                {
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.BookingId.ToUpper().Equals(_criteria.BookingReqestId.ToUpper()));
                }

                if (_criteria.ExhibitorId != Guid.Empty && _criteria.StallId == Guid.Empty && _criteria.Status == null && _criteria.CountryId == Guid.Empty && _criteria.StartDate == DateTime.MinValue && _criteria.EndDate == DateTime.MinValue)
                    builder = builder.And(x => x.Exhibitor.Id.Equals(_criteria.ExhibitorId));

                if (_criteria.EventId != Guid.Empty && _criteria.StallId == Guid.Empty && _criteria.Status != null && _criteria.CountryId == Guid.Empty && _criteria.StartDate == DateTime.MinValue && _criteria.EndDate == DateTime.MinValue)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.Status.ToUpper() == _criteria.Status.ToUpper());

                if (_criteria.EventId != Guid.Empty && _criteria.CountryId != Guid.Empty && _criteria.StallId == Guid.Empty && _criteria.Status == null && _criteria.StartDate == DateTime.MinValue && _criteria.EndDate == DateTime.MinValue)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.Exhibitor.Country.Id.Equals(_criteria.CountryId));

                if (_criteria.EventId != Guid.Empty && _criteria.CountryId == Guid.Empty && _criteria.StallId == Guid.Empty && _criteria.Status == null && _criteria.StartDate != DateTime.MinValue && _criteria.EndDate != DateTime.MinValue)
                {
                    TimeSpan ts = new TimeSpan(23, 59, 59);
                    DateTime EndDate = _criteria.EndDate.Date + ts;
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.CreatedOn >= _criteria.StartDate && x.CreatedOn <= EndDate);
                }
                return builder;
            }
        }
    }
}
