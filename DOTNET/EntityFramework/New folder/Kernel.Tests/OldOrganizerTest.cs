//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Kernel;
//using Kernel.Framework.Model;
//using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
//using Techlabs.Euphoria.Kernel.Framework.Model;
//using Techlabs.Euphoria.Kernel.Framework.Repository;
//using System.Linq.Expressions;

//namespace Kernel.Tests
//{
//     /*

//    class OrganizerQueryExprn : ISpecification<Organizer>
//    {



//        public Expression<Func<Organizer, bool>> Expression
//        {
//            get
//            {
//                Expression<Func<Organizer, bool>> theTenatnaID = c => c.TenantId == 101;
//                return theTenatnaID;
//            }
//        }
//    }

   
//    class OrganizerTest
//    {

       

//        static void Main_(string[] args)
//        {

//            testOrganizerWithExhibiors();
           


            

//            Console.WriteLine("End");
//            Console.ReadKey();
//        }

//        private static void testOrganizerWithExhibiors()
//        {
//            OrganizerRepository repo = new OrganizerRepository();

//            Organizer org = new Organizer();
//            org.TenantId = 103;
//            org.Name = "Syntel Marketing";

//            org.Exhibitors = new List<ExhibitorEntity>() {
//                new ExhibitorEntity {Name="Nike",Description="Nike" },
//                new ExhibitorEntity {Name="Adidas",Description="Adidas" }
//            };

//            repo.Add(org);

//        }


//        private static void testOrganizerEntitySearch()
//        {
//            OrganizerRepository repo = new OrganizerRepository();
//           List<Organizer> list=  repo.Find(new OrganizerQueryExprn()).ToList();

//            foreach (Organizer elem in list)
//                Console.WriteLine(elem.Name);


//        }

//        private static void testOrganizerEntityUpdate()
//        {

//            OrganizerRepository repo = new OrganizerRepository();

//            Organizer org  = repo.GetById(new Guid("E65A7508-FDDF-4052-8361-FC8541D1C4B3"));
//            org.Name = "GS Marketting with techlabs";
//            org.Description = "New description";

//            repo.Update(org);




//        }

//        private static void testOrganizerEntityDelete()
//        {
//            OrganizerRepository repo = new OrganizerRepository();
//            repo.Delete(new Guid("5A07D718-49A5-4B1D-B2BA-4F03F3ACF34F"));

            

           

//        }*/
//}
