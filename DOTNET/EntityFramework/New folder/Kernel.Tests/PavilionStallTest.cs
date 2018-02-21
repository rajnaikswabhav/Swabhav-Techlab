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
//   public class PavilionStallTest
//    {
//        private IRepository<Pavilion> _pavilionRepo = new EntityFrameworkRepository<Pavilion>();
//        private IRepository<Organizer> _orgnizerRepo = new EntityFrameworkRepository<Organizer>();
//        private IRepository<Exhibitor> _ExhibitorRepo = new EntityFrameworkRepository<Exhibitor>();


//        public void addPavilionStall()
//        {
//            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
//            {
                
//                Organizer organizer = Organizer.Create(102, "Tech labs", "Techlbas");

//                Exhibition exhibition = Exhibition.Create("Travel and Tourism", DateTime.Now, DateTime.Now, "mega trade", true);
//                organizer.Exhibitions = new List<Exhibition> { exhibition };

//                Pavilion pavilion = Pavilion.Create("pavilion A");
//                exhibition.Pavilions = new List<Pavilion> { pavilion };

//                Stall Stall1 = Stall.Create(101);
//                Stall Stall2 = Stall.Create(102);
//                Stall Stall3 = Stall.Create(103);

//                pavilion.Stalls = new List<Stall> { Stall1, Stall2, Stall3 };

             

//                Category cat1 = Category.Create("Shoes", "Shoes");
//                Category cat2 = Category.Create("Bags", "Bags");

//                Exhibitor exbtr = Exhibitor.Create("Nike", "Nike");
//                exbtr.Categories = new List<Category> { cat1, cat2 };
//                exbtr.Organizer = organizer;
//                exbtr.Exhibitions = new List<Exhibition> { exhibition };

//                exbtr.Stalls = new List<Stall> { Stall1, Stall2, Stall3 };
//                _orgnizerRepo.Add(organizer);
//                _pavilionRepo.Add(pavilion);
//                _ExhibitorRepo.Add(exbtr);
//                unitOfWork.SaveChanges();
//            }
//            Console.WriteLine("Added");
//        }
        
//        public static void _Main()
//        {
//            //new OrganizerExhibitionTest().addOrganizerWithExhibition();
//            new PavilionStallTest().addPavilionStall();
//            Console.ReadLine();
//        }
//    }
//}
