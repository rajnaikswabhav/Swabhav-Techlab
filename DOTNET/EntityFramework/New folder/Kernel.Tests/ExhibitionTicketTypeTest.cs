//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Techlabs.Euphoria.Kernel.Framework.Repository;
//using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
//using Techlabs.Euphoria.Kernel.Model;

//namespace Kernel.Tests
//{
//    class ExhibitionTicketTypeTest
//    {

//        private IRepository<Exhibition> _exhibitionRepo = new EntityFrameworkRepository<Exhibition>();
//        private IRepository<Organizer> _orgnizerRepo = new EntityFrameworkRepository<Organizer>();


//        public void addExhibitionWithTicketTye()
//        {


//            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
//            {

//                Organizer org = _orgnizerRepo.GetById(Guid.Parse("48606998-C448-42F7-A9F1-31F927E47C7D"));
//                Exhibition mtf = Exhibition.Create("Mega trade fair Again", DateTime.Now, DateTime.Now, "mega trade", true);
//                Exhibition hd = Exhibition.Create("Home and decor fair", DateTime.Now, DateTime.Now, "Home and decor fair", true);

//                Venue venue = Venue.Create("VZ", "science city", "West benagal");

//                mtf.Venue = venue;
//                hd.Venue = venue;

//                venue.Exhibitions = new List<Exhibition> { mtf,hd };



//                 mtf.Organizer = org;
//                 hd.Organizer= org;

//                TicketType typ1 = TicketType.Create("2day", 50, "2day");
//                TicketType typ2 = TicketType.Create("1day", 20, "1day");

//                mtf.TicketTypes = new List<TicketType> { typ1, typ2 };
//                typ1.Exhibitions = new List<Exhibition> { hd };

//                _exhibitionRepo.Add(mtf);
//                _exhibitionRepo.Add(hd);

//                unitOfWork.SaveChanges();
//            }

//            Console.WriteLine("added data");
//        }

//        public static void _Main() {

//          // new OrganizerExhibitionTest().addOrganizerWithExhibition();
//          new ExhibitionTicketTypeTest().addExhibitionWithTicketTye();

//            Console.ReadLine();
//        }
//    }
//}
