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
//    class VenueTest
//    {
//        private IRepository<Pavilion> _pavilionRepo = new EntityFrameworkRepository<Pavilion>();
//        private IRepository<Organizer> _orgnizerRepo = new EntityFrameworkRepository<Organizer>();
//        private IRepository<Exhibitor> _ExhibitorRepo = new EntityFrameworkRepository<Exhibitor>();
//        private IRepository<Venue> _venueRepo = new EntityFrameworkRepository<Venue>();
//        private IRepository<Visitor> _visitorRepo = new EntityFrameworkRepository<Visitor>();


//        public void addVenuePavilionStall()
//        {
//            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
//            {

//                Organizer organizer = Organizer.Create(105, "GS Mktg labs", "Techlbas");




//                Exhibition exhibition = Exhibition.Create("Travel and Tourism", DateTime.Now, DateTime.Now, "mega trade", true);
//                organizer.Exhibitions = new List<Exhibition> { exhibition };

//                Pavilion pavilion = Pavilion.Create("pavilion A");
//                exhibition.Pavilions = new List<Pavilion> { pavilion };

//                Stall Stall1 = Stall.Create(101);
//                Stall Stall2 = Stall.Create(102);
//                Stall Stall3 = Stall.Create(103);

//                pavilion.Stalls = new List<Stall> { Stall1, Stall2, Stall3 };

//                TicketType typ1 = TicketType.Create("2day", 50, "2day");
//                TicketType typ2 = TicketType.Create("1day", 20, "1day");

//                Venue venue = new Venue { City = "Mumbai", Address = "Mumbai", State = "Maharashtra", Organizer = organizer, Exhibitions = new List<Exhibition> { exhibition } };
//                venue.TicketTypes = new List<TicketType> { typ1, typ2 };

//                Country county = new Country { Name = "India" };
//                State state = new State { Name = "Kolkata" };

//                Country county2 = new Country { Name = "Thaniland" };
//                State state2 = new State { Name = "ThaiStata" };


//                Category cat1 = Category.Create("Shoes", "Shoes");
//                Category cat2 = Category.Create("Bags", "Bags");
//                cat1.Organizer = organizer;
//                cat2.Organizer = organizer;

//                Exhibitor exbtr = Exhibitor.Create("Nike", "Nike");
//                exbtr.Categories = new List<Category> { cat1, cat2 };
//                exbtr.Organizer = organizer;
//                exbtr.Exhibitions = new List<Exhibition> { exhibition };
//                exbtr.Country = county;
//                exbtr.State = state;

//                Exhibitor exbtr2 = Exhibitor.Create("adidas", "adidas");
//                exbtr2.Categories = new List<Category> { cat1, cat2 };
//                exbtr2.Organizer = organizer;
//                exbtr2.Exhibitions = new List<Exhibition> { exhibition };
//                exbtr2.Country = county;
//                exbtr2.State = state;

//                Ticket ticket = new Ticket { TokenNumber = "1001", NumberOfTicket=5,TotalPriceOfTicket=50*5,
//                    TicketDate = DateTime.Now  };
//                Visitor visitor = new Visitor
//                {
//                    FirstName = "kannan",
//                    LastName = "Sudhakaran",
//                    Address = "Andheri",
//                    EmailId = "mayursaid20@gmail.com",
//                    MobileNo = "988989898",
//                    Pincode = 400023,
//                    DateOfBirth = DateTime.UtcNow
//                };

//                visitor.Tickets = new List<Ticket> { ticket };
//                organizer.Visitors = new List<Visitor> { visitor };
//                ticket.TicketType = typ1;

               

//                exbtr.Stalls = new List<Stall> { Stall1, Stall2, Stall3 };


//                _orgnizerRepo.Add(organizer);
//                _pavilionRepo.Add(pavilion);
//                _ExhibitorRepo.Add(exbtr);
//                _ExhibitorRepo.Add(exbtr2);
//                _venueRepo.Add(venue);
//                _visitorRepo.Add(visitor);

//                unitOfWork.SaveChanges();
//            }
//            Console.WriteLine("Added");
//        }

//        public static void _Main()
//        {

//            new VenueTest().addVenuePavilionStall();

//            Console.ReadKey();

//        }
//    }
//}
