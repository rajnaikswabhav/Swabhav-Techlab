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
    public class ExhibitorFeedbackSpecificationForSearch : ISpecification<ExhibitorFeedback>
    {
        private ExhibitorFeedbackSearchCriteria _criteria;

        public ExhibitorFeedbackSpecificationForSearch(ExhibitorFeedbackSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<ExhibitorFeedback, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<ExhibitorFeedback>();

                if (_criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.ExhibitorId != Guid.Empty && _criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Exhibitor.Id.Equals(_criteria.ExhibitorId)).And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.ExhibitorId == Guid.Empty && _criteria.EventId != Guid.Empty && _criteria.Objective != null && _criteria.Objective != 0)
                    builder = builder.And(x => x.Objective.Equals(_criteria.Objective)).And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.ExhibitorId == Guid.Empty && _criteria.EventId != Guid.Empty && _criteria.QualityofVisitor != null && _criteria.QualityofVisitor != 0)
                    builder = builder.And(x => x.QualityOfVisitor.Equals(_criteria.QualityofVisitor)).And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.ExhibitorId == Guid.Empty && _criteria.EventId != Guid.Empty && _criteria.Satisfaction != null && _criteria.Satisfaction != 0)
                    builder = builder.And(x => x.Satisfaction.Equals(_criteria.Satisfaction)).And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.ExhibitorId == Guid.Empty && _criteria.EventId != Guid.Empty && _criteria.TargetAudience.HasValue)
                    builder = builder.And(x => x.TargetAudience == _criteria.TargetAudience).And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.ExpectedBusiness.HasValue)
                    builder = builder.And(x => x.ExpectedBusiness == _criteria.ExpectedBusiness).And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.IIMTFSatisfaction != null && _criteria.IIMTFSatisfaction != 0)
                    builder = builder.And(x => x.IIMTFSatisfaction.Equals(_criteria.IIMTFSatisfaction));

                if (_criteria.IIMTFTeam != null && _criteria.IIMTFTeam != 0)
                    builder = builder.And(x => x.IIMTFTeam.Equals(_criteria.IIMTFTeam));

                if (_criteria.IIMTFFacility != null && _criteria.IIMTFFacility != 0)
                    builder = builder.And(x => x.IIMTFFacility.Equals(_criteria.IIMTFFacility));

                return builder;
            }
        }

    }
}

