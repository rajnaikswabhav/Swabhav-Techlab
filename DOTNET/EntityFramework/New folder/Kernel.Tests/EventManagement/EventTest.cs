using Modules.EventManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Model;
using Techlabs.Euphoria.Kernel.Service.Email;
using Techlabs.Euphoria.Kernel.Specification;

namespace Kernel.Tests.EventManagement
{
    class EventTest
    {
        private IRepository<Event> _EventRepo = new EntityFrameworkRepository<Event>();
        private IRepository<Venue> _venueRepository = new EntityFrameworkRepository<Venue>();
        public static void _Main()
        {
            
           new EventTest().sendmail();

            Console.ReadKey();

        }

        async void sendmail()
        {
            EmailService send = new EmailService();
            send.Send("mayursaid20@gmail.com", "hi", "body");
            Console.Write("email sent");
        }

        private void addEvent()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                Event event1= Event.Create("Kolkata 2016", "Kolkata", DateTime.Now, DateTime.Now, DateTime.Now,true);
                var venues = _venueRepository.Find(new GetAllSpecification<Venue>()).OrderBy(x => x.Order);
                event1.Venue = venues.FirstOrDefault();
                _EventRepo.Add(event1);
                unitOfWork.SaveChanges();
            }
        }
    }
}
