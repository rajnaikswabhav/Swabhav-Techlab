using Modules.EventManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Techlabs.Euphoria.API.Models;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework.AdminRepository;
using Techlabs.Euphoria.Kernel.Model;
using Techlabs.Euphoria.Kernel.Modules.BookingManagement;
using Techlabs.Euphoria.Kernel.Service;
using Techlabs.Euphoria.Kernel.Service.Email;
using Techlabs.Euphoria.Kernel.Service.SMS;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId}/email")]
    public class EmailController : ApiController
    {
        private readonly IRepository<Exhibitor> _exhibitorRepository = new EntityFrameworkRepository<Exhibitor>();
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<Visitor> _visitorRepository = new EntityFrameworkRepository<Visitor>();
        private IRepository<Event> _eventRepository = new EntityFrameworkRepository<Event>();
        private IRepository<EventTicket> _eventTicketRepository = new EntityFrameworkRepository<EventTicket>();
        private IRepository<Ticket> _ticketRepository = new EntityFrameworkRepository<Ticket>();
        private IRepository<Transaction> _transactionRepository = new EntityFrameworkRepository<Transaction>();
        private IRepository<Category> _categoryRepository = new EntityFrameworkRepository<Category>();
        private IRepository<Country> _countryRepository = new EntityFrameworkRepository<Country>();
        private IRepository<VisitorFeedback> _visitorFeedbackRepository = new EntityFrameworkRepository<VisitorFeedback>();
        private IRepository<VisitorCardPayment> _visitorCardPaymentRepository = new EntityFrameworkRepository<VisitorCardPayment>();
        private IRepository<GeneralMaster> _generalMasterRepository = new EntityFrameworkRepository<GeneralMaster>();
        private IRepository<ExhibitorType> _exhibitorTypeRepository = new EntityFrameworkRepository<ExhibitorType>();
        private VisitorEventTicketRepository<EventTicket> _visitorEventTicketRepository = new VisitorEventTicketRepository<EventTicket>();
        private IRepository<Booking> _bookingRepository = new EntityFrameworkRepository<Booking>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [Route("event/{eventId:guid}/sendFeedbackMessage")]
        [ResponseType(typeof(VisitorDTO[]))]
        public IHttpActionResult Post(Guid organizerId, Guid eventId,SendVisitorEmailDTO sendVisitorEmailDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                Event eventDetail = _eventRepository.GetById(eventId);
                if (eventDetail == null)
                    return NotFound();

                var criteria = new EventTicketReportSearchCriteria
                {
                    StartDate = eventDetail.StartDate,
                    EndDate = eventDetail.EndDate,
                    IsPayatLocation = false,
                    IsPayOnline = false,
                    IsWeb = false,
                    IsMobile = false,
                    PaymentCompleted = true
                };
                var sepcification = new EventTicketReportSpecificationForSearch(criteria);
                var totalVisitors = _visitorEventTicketRepository.TotalVisitorsList(sepcification, x => x.Visitor.MobileNo);

                List<VisitorDTO> listVisitor = new List<VisitorDTO>();
                var count = totalVisitors.Select(x => x.MobileNo).Distinct().Count();
                int countSMS = 0;
                var data = totalVisitors.Select(x => x.MobileNo).Distinct().Skip((3 - 1) * 1).Take(1).ToList();
                foreach (string singleEventTicket in data)
                {
                    if (singleEventTicket.Count() == 10)
                    {
                        var visitorcriteria = new VisitorSearchCriteria { PhoneNumber = singleEventTicket };
                        var visitorsepcification = new VisitorSpecificationForSearch(visitorcriteria);
                        var visitor = _visitorRepository.Find(visitorsepcification);
                        var url = string.Format("http://gsmktg.com/f/#/v/{0}", visitor.FirstOrDefault().Id);
                        Console.WriteLine(visitor.FirstOrDefault().MobileNo);
                        SendFeedbackSMS(url, visitor.FirstOrDefault().MobileNo);
                    }
                    countSMS++;
                }
                return Ok(countSMS);
            }
        }

        [Route("bookingList/{eventId:guid}/exhibitorList/{pageSize:int}/{pageNumber:int}")]
        [ResponseType(typeof(BookingDTO))]
        public IHttpActionResult GetExhibitorListByBooking(Guid organizerId, Guid eventId, int pageSize, int pageNumber)
        {

            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                string htmlPage = "<html><body><h3> Date : 28th August, 2017 </h3><br><center><h4>Subject: Proposal for your company’s participation in the India International Mega Trade Fair (IIMTF) to be held from 15th – 25th December, 2017 at the Science City Exhibition Ground, Kolkata. </h4></center><br>Dear Sir, <br><br>It gives us great pleasure to inform you that we are organizing the India International Mega Trade Fair (IIMTF) to be held from 15th – 25th December, 2017 at the Science City Exhibition Ground, Kolkata. <br><br><b>The IIMTF in Kolkata is jointly organized by The Bengal Chamber of Commerce & Industry (BCCI) and G. S. Marketing Associates. </b><br><br><b>The Bengal Chamber of Commerce & Industry (BCCI)</b> is the oldest and one of the most respected institutions of its kind in India. It is a powerful enabler, lobbying for development of the economy and Infrastructure in India. The chamber has been associated with many Government Policies & legislations. <br><br><b>G. S. Marketing Associates</b> is a leading and well known organizer of Trade Fairs and Exhibitions in India and has successfully organized over 200 Exhibitions and Trade Fairs since 27 years. <br><br><b>IIMTF</b> is the biggest trade fair of Eastern India and attracts huge number of visitors. Many foreign countries, corporate bodies, Government Departments & PSUs, traders from all over the world make this fair truly international. The fair is conducted in an international ambiance with air conditioned pavilions dedicated to different product categories, with the whole area carpeted to make it dust free, A.C. conference & meeting rooms etc. Moreover, a highly effective & elaborate advertisement campaign is taken up to attract both B2B and B2C customers. <br><br><b>We would like to have your company’s participation in the India International Mega Trade Fair. The highlights of India International Mega Trade Fair, organized by G.S. are:- </b><br><br><ol><li>It is a Major Fair in Eastern India, approved by ITPO, which comes under the Ministry of Commerce,Government of India. </li><li>It will have 17 Air conditioned pavilions for Kolkata and 12 A.C. Pavilions for other stations dedicated to different products / subjects with wooden platforms and synthetic carpets. The entire exhibition area will be carpeted.</li><li> Attractive ambiance of the exhibition site. The total area of the Fair is approx 4, 00,000 sq.ft. </li><li>Unmatched extensive publicity through Print and Electronic Media, Outdoor Media, Hoardings, etc. </li><li>Impressive footballs. Last year we had almost 10 Lacs in Kolkata. </li><li> Last year 17 countries and 19 States had participated in the IIMTF in Kolkata. This year, we expect 20 countries to participate in this fair.</li><li>Last Year more than 800 companies participated in the IIMTF in Kolkata. </li></ol><br><b>The <u>Space Details & Rates</u> for India International Mega Trade Fair (IIMTF)</b> in Ranchi is mentioned herewith for your ready reference:- <br><style>table { border-collapse: collapse;}table, td, th { border: 1px solid black;padding:10px;}</style><table cellpadding='2px' border=1><tr><th>NAME</th><th>PLACE</th><th>DATE</th><th>SIZE OF STALL</th><th>RATE (INR.)</th></tr><tr><th>India International Mega Trade Fair (IIMTF)</th><th>Science City Ground, Kolkata,West Bengal</th><th>15th – 25th December, 2017</th><th>3 mt X 3 mt = 9 sq. Mt.</th><th>Rs. 1,00,000/-</th></tr></table><br>Stalls are available in the multiple of 9sq.mt. and the rate is also proportionately multiplied.<br><br><b><u>FOR KOLKATA (FOOD PAVILION)</u></b><br><ol><li> Stall size 9 sq.mt = Rate 1,00,000/-</li><li> Stall size 18sq.mt = Rate 2,00,000/-</li><li> Stall size 27sq.mt = Rate 3,00,000/-</li><li>Stall size 36sq.mt = Rate 4,00,000/-</li><li>Add GST @ 18%.</li><li>Payment should be made in Favour of <b>'G. S. MARKETING ASSOCIATES'</b> payable at Kolkata. </li></ol><br><b>The advertising and publicity efforts are as follows: </b><table><tr><th> Newspaper Advertisement </th><th>Venue Branding</th></tr><tr><th>Television</th><th>Complementary Passes</th></tr><tr><th>Hoardings</th><th>Invitation Card</th></tr><tr><th>Mobile Van</th><th>VIP Passes</th></tr><tr><th>Radio Jingle</th><th>Participation Badges</th></tr><tr><th> Local Cable TV </th><th>Gates of the Venue</th></tr><tr><th>Press Conferences </th><th>9 Lacs Tickets</th></tr><tr><th>Digital Media </th><th>Car Parking Stickers</th></tr><tr></tr></table><br>We look forward to the early confirmation of your participation in <b>INDIA INTERNATIONAL MEGA TRADE FAIR (IIMTF). </b>If you have any further queries regarding the above fair, please contact with the undersigned at the following Mobile Number: <b>+918296878363.</b> We look forward to long and very fruitful business relations with your organization. <br><b>Thanking you,<br><br>Yours Faithfully<br><br>For, G.S.Marketing Associates, <br><br>Achintya Ghorai<br><br>M- 8296878363<br></b><br><br><a href='http://gsmktg.com/proposalImages/Inside%20View.1.jpg'><img src='http://gsmktg.com/proposalImages/Inside%20View.1.png' height='100px' ></a><a href='http://gsmktg.com/proposalImages/Inside%20View.2.jpg'><img src='http://gsmktg.com/proposalImages/Inside%20View.2.png' height='100px'></a><a href='http://gsmktg.com/proposalImages/Outside.View.jpg'><img src='http://gsmktg.com/proposalImages/Outside.View.1.png' height='100px'> </a></body></html>";
                string fileName = "test";
                string subject = "GS Marketing Associate-Proposal For IIMTF Kolkata(2017)";

                Task.Run(() => SendeMail(htmlPage, fileName, null, subject));
                var organizer = _organizerRepository.GetById(organizerId);
                return Ok();
            }
        }

        private void SendFeedbackSMS(string url, string mobileNo)
        {
            // messageText = string.Format(messageText);

            var Emails = new
            {
                items = new[] { new { Id = "2981B633-EBFD-4281-BBBC-3EB98BDDA041", Name = "Ajay Chavan", Mobile = "9320037678", Company = "Tanu Creation" } }
            };

            SolutionInfoTechSMSService smsService = new SolutionInfoTechSMSService();
            foreach (var item in Emails.items)
            {
                url = string.Format("http://gsmktg.com/f/#/e/{0}", item.Id);
                var messageText = "Thanks for exhibiting at IIMTF-17. Kindly fill our feedback form for better event in future " + url;
                string message = HttpUtility.UrlEncode(messageText);
                NotificationService notificationService = new NotificationService(smsService, item.Mobile, message);
                notificationService.Send();
            }

        }

        private void SendeMail(string htmlPage, string fileName, Exhibitor exhibitor, string subject)
        {
            try
            {
                var Emails = new
                {
                    items = new[]
                    {
                        new {
                                Email= "ydubla@gmail.com"
                              }
                    }
                };
                //string pdfFile = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PdfFolderLocation"]) + "\\" + fileName + ".pdf";
                // PDFGenerator generator = new PDFGenerator();
                //  generator.CreatePDFFromHTMLFile(htmlPage,pdfFile);

                IEmailService emailService = new SendGridEmailService();
                foreach (var item in Emails.items)
                {
                    emailService.Send(item.Email, subject, htmlPage);
                }
            }
            catch (Exception ex)
            {
                //TODO:need to handle exception or log
            }

        }
    }
}
