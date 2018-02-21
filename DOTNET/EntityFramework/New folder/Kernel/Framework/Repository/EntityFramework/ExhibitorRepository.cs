using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework
{
    //public  class ExhibitorRepository : EntityFrameworkRepository<ExhibitorEntity>
    //  {    
    //      public IList<ExhibitorEntity> GetExhibitorsByOrganizerId(Guid organizerId) {

    //          var exhibitors = (from c in CurrentContext.Exhibitors
    //                           where c.OrganizerID==organizerId
    //                           select c).ToList();
    //          return exhibitors;
    //      }

    //      public IList<ExhibitorEntity> GetExhibitorsById(Guid organizerid,Guid exhibitorid)
    //      {
    //          var exhibitors = (from c in CurrentContext.Exhibitors
    //                            where c.OrganizerID == organizerid && c.Id==exhibitorid
    //                            select c).ToList();
    //          return exhibitors;
    //      }

    //      public void AddExhibitor(Guid organizerid,ExhibitorEntity exhibitor)
    //      {         
    //          Organizer orgEntity = (from c in this.CurrentContext.Organizers
    //                                       where c.Id == organizerid
    //                                       select c).SingleOrDefault();
    //          orgEntity.Exhibitors = new List<ExhibitorEntity> { exhibitor };
    //          this.CurrentContext.SaveChanges();
    //  }

    //    public  void UpdateExhibitor(Guid organizerid,ExhibitorEntity newEnity)
    //      {
    //          ExhibitorEntity entity = ( from c in CurrentContext.Exhibitors
    //                                   where c.OrganizerID==organizerid && c.Id==newEnity.Id
    //                                   select c).SingleOrDefault();
    //          entity.Name = newEnity.Name;
    //          entity.Description = newEnity.Description;
    //          base.Update(entity);
    //      }
    //  }
}
