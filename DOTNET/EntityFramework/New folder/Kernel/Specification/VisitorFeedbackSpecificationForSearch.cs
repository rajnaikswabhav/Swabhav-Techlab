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
    public class VisitorFeedbackSpecificationForSearch : ISpecification<VisitorFeedback>
    {
        private VisitorFeedbackSearchCriteria _criteria;

        public VisitorFeedbackSpecificationForSearch(VisitorFeedbackSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<VisitorFeedback, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<VisitorFeedback>();

                if (_criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.VisitorId != Guid.Empty && _criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Visitor.Id.Equals(_criteria.VisitorId)).And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.VisitorId == Guid.Empty && _criteria.EventId != Guid.Empty && _criteria.SpendRange != 0)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.SpendRange == _criteria.SpendRange);

                if (_criteria.VisitorId == Guid.Empty && _criteria.EventId != Guid.Empty && _criteria.ReasonForVisiting != 0 && _criteria.ReasonForVisiting != 0)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.ReasonForVisiting == _criteria.ReasonForVisiting);

                if (_criteria.VisitorId == Guid.Empty && _criteria.EventId != Guid.Empty && _criteria.KnowAboutUs != 0 && _criteria.KnowAboutUs != 0)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.KnowAboutUs == _criteria.KnowAboutUs);

                if (_criteria.VisitorId == Guid.Empty && _criteria.EventId != Guid.Empty && _criteria.EventRating != 0 && _criteria.EventRating != 0)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.EventRating == _criteria.EventRating);

                if (_criteria.VisitorId == Guid.Empty && _criteria.EventId != Guid.Empty && _criteria.Recommendation.HasValue)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.RecommendToOther == _criteria.Recommendation);

                if (_criteria.CategoryId != Guid.Empty && _criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.Categories.Any(y => y.Id.Equals(_criteria.CategoryId)));

                if (_criteria.CountryId != Guid.Empty && _criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.Countries.Any(y => y.Id.Equals(_criteria.CountryId)));

                return builder;
            }
        }
    }
}
