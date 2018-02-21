using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Modules.LayoutManagement;

namespace Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework.AdminRepository
{
    public class SalesTargetRepository<T> where T : AggregateEntity
    {
        public IDictionary<string, int> SalesTargetByExhibitorIndustryType()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryables = unitOfWork.DbContext.Set<SalesTarget>().AsExpandable()
                    .Where(x => x.ExhibitorIndustryType != null)
                    .GroupBy(x => x.ExhibitorIndustryType.Id).Select(y => new
                    {
                        IndustryType = y.Key,
                        totalTarget = y.Sum(t => t.Target)
                    }).OrderByDescending(i => i.IndustryType);
                //return queryable.ToList().ForEach(x=>x.Key);
                return queryables.ToDictionary(x => x.IndustryType.ToString(), x => x.totalTarget);
            }
        }

        public IDictionary<string, int> SalesTargetBySalesPerson()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryables = unitOfWork.DbContext.Set<SalesTarget>().AsExpandable()
                    .Where(x => x.Login != null)
                    .Where(x => x.Login.Role.RoleName.ToUpper() != "Partner".ToUpper())
                    .GroupBy(x => x.Login.Id).Select(y => new
                    {
                        SalesPersonId = y.Key,
                        totalTarget = y.Sum(t => t.Target)
                    });
                //return queryable.ToList().ForEach(x=>x.Key);
                return queryables.ToDictionary(x => x.SalesPersonId.ToString(), x => x.totalTarget);
            }
        }

        public IDictionary<string, int> SalesTargetByCountry()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryables = unitOfWork.DbContext.Set<SalesTarget>().AsExpandable()
                    .Where(x => x.Country != null)
                    .Where(x => x.Country.Name.ToUpper() != "India".ToUpper())
                    .GroupBy(x => x.Country.Id).Select(y => new
                    {
                        CountryId = y.Key,
                        totalTarget = y.Sum(t => t.Target)
                    });
                //return queryable.ToList().ForEach(x=>x.Key);
                return queryables.ToDictionary(x => x.CountryId.ToString(), x => x.totalTarget);
            }
        }
        public IDictionary<string, int> SalesTargetByState()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryables = unitOfWork.DbContext.Set<SalesTarget>().AsExpandable()
                    .Where(x => x.State != null)
                    .GroupBy(x => x.State.Id).Select(y => new
                    {
                        StateId = y.Key,
                        totalTarget = y.Sum(t => t.Target)
                    });
                //return queryable.ToList().ForEach(x=>x.Key);
                return queryables.ToDictionary(x => x.StateId.ToString(), x => x.totalTarget);
            }
        }

        public IDictionary<string, int> SalesTargetByPartner()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryables = unitOfWork.DbContext.Set<SalesTarget>().AsExpandable()
                    .Where(x => x.Login != null)
                    .Where(x => x.Login.Role.RoleName.ToUpper().Equals("Partner".ToUpper()))
                    .GroupBy(x => x.Login.Id).Select(y => new
                    {
                        SalesPersonId = y.Key,
                        totalTarget = y.Sum(t => t.Target)
                    });
                //return queryable.ToList().ForEach(x=>x.Key);
                return queryables.ToDictionary(x => x.SalesPersonId.ToString(), x => x.totalTarget);
            }
        }
    }
}
