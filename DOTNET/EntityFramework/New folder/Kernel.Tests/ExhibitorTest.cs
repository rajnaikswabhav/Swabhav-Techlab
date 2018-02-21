//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Techlabs.Euphoria.Kernel.Framework.Model;
//using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;

//namespace Kernel.Tests
//{
//    class ExhibitorTest
//    {
//        public static void _Main(String[] args)
//        {

//            //testExhibitorInsertChildTable();
//            // testExhibitorUpdateChildTable();

//            // testExhibitorRepositoryRead();
//            // testExhibitorRepositoryReadById();

//            //testExhibitorRepositoryInsert();

//            // testExhibitorRepositoryUpdate();

//            testExhibitorDelete();

//            Console.WriteLine("End");
//            Console.ReadLine();
//        }

//        private static void testExhibitorDelete()
//        {
//            ExhibitorRepository repo = new ExhibitorRepository();
//           repo.Delete(new Guid("786FFA19-4720-4270-B6AA-3676F4864331"));


//        }

//        private static void testExhibitorRepositoryUpdate()
//        {

//            ExhibitorRepository repo = new ExhibitorRepository();
//            ExhibitorEntity entity = repo.GetById(new Guid("786FFA19-4720-4270-B6AA-3676F4864331"));
//            entity.Name = "Kannan";
//            repo.Update(entity);
//        }

//        private static void testExhibitorRepositoryInsert()
//        {

//            ExhibitorRepository exRepo = new ExhibitorRepository();

//            Organizer orgEntity = (from c in exRepo.CurrentContext.Organizers
//                                        where c.Id== new Guid("576A030A-0CD2-4086-9D5A-67086E27CBA5")
//                                        select c).SingleOrDefault();

//            ExhibitorEntity exhibitor = new ExhibitorEntity();
//            exhibitor.Name = "GoQii123";
//            exhibitor.Description = "Go QII123";
//            orgEntity.Exhibitors = new List<ExhibitorEntity> { exhibitor };

//            exRepo.CurrentContext.SaveChanges();

          

//        }

//        private static void testExhibitorRepositoryReadById()
//        {
//            ExhibitorRepository exbn = new ExhibitorRepository();
//            var exhibition=  exbn.GetById(new Guid("BB8373D9-22C7-445C-8D28-8630AF65408D"));
//            Console.WriteLine(exhibition.Name);
//        }

//        private static void testExhibitorRepositoryRead()
//        {
//            ExhibitorRepository exbn = new ExhibitorRepository();
//            foreach (ExhibitorEntity ex in exbn.Get().ToList())
//            {
//                Console.WriteLine(ex.Name);
//            }




//        }

//        private static void testExhibitorUpdateChildTableUsingParentRepository() 
//        {
//            OrganizerRepository repo = new OrganizerRepository();
//            var exhibitor = (from c in repo.CurrentContext.Exhibitors
//                              where c.Id == new Guid("B0265436-5F52-4E88-89F5-CF0B6FE57E47")
//                              select c).SingleOrDefault();



//            exhibitor.Name = "My new GoQii";
//            exhibitor.Description = "My new GoQii";


//            repo.CurrentContext.SaveChanges();






//        }

//        private static void testExhibitorInsertChildTable()
//        {

//            OrganizerRepository repo = new OrganizerRepository();
//            var org = repo.GetById(new Guid("576A030A-0CD2-4086-9D5A-67086E27CBA5"));


//            ExhibitorEntity exhibitor = new ExhibitorEntity();
//            exhibitor.Name = "GoQii";
//            exhibitor.Description = "Go QII";


            

//            org.Exhibitors = new List<ExhibitorEntity> { exhibitor};



//            repo.Update(org);



           






//        }
//    }
//}
