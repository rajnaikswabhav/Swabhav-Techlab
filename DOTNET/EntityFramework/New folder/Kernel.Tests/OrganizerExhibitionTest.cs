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
//    class OrganizerExhibitionTest
//    {

//        private IRepository<Organizer>  _repository = new EntityFrameworkRepository<Organizer>();


//        public void addOrganizerWithExhibition() {

//            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
//            {


//                Organizer entity = Organizer.Create(101, "Aurionpro", "AurionPro");

//                Exhibition mtf = Exhibition.Create("Mega trade fair", DateTime.Now, DateTime.Now, "mega trade", true);
//                Exhibition hd = Exhibition.Create("Home and decor fair", DateTime.Now, DateTime.Now, "Home and decor fair", true);


//                entity.Exhibitions = new List<Exhibition> { mtf, hd };


//                _repository.Add(entity);


//                unitOfWork.SaveChanges();
//                Console.WriteLine("added data");


//            }

//        }


//        public static void _Main()
//        {

//            new OrganizerExhibitionTest().addOrganizerWithExhibition();

//            Console.ReadLine();

//       }
// }
//}
