using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Model;

namespace Kernel.Tests
{
    class ExhibitorProductCategory
    {

        private IRepository<Exhibitor> _exbtrRepo = new EntityFrameworkRepository<Exhibitor>();
        private IRepository<Organizer> _orgnizerRepo = new EntityFrameworkRepository<Organizer>();


        public void addExhibitorandProducts()
        {

            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {

                Organizer org = _orgnizerRepo.GetById(Guid.Parse("D091885D-C3A6-4419-B3A5-B98C3A8476DE"));

                Exhibitor exbtr = Exhibitor.Create("Nike", "Nike");
                Category cat1 = Category.Create("Shoes", "Shoes");
                cat1.Organizer = org;

                Category cat2 = Category.Create("Bags", "Bags");
                cat2.Organizer = org;

                exbtr.Categories = new List<Category> { cat1, cat2 };
                exbtr.Organizer = org;


                _exbtrRepo.Add(exbtr);
                unitOfWork.SaveChanges();
            }

            Console.WriteLine("added data");
        }

        public static void _Main()
        {

           // new OrganizerExhibitionTest().addOrganizerWithExhibition();
             new ExhibitorProductCategory().addExhibitorandProducts();


            Console.ReadLine();

        }
    }
}
