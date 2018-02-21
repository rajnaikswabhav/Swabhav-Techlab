using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Techlabs.Euphoria.API.Filters;
using Techlabs.Euphoria.API.Models;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Model;
using Techlabs.Euphoria.Kernel.Service;
using Techlabs.Euphoria.Kernel.Service.Email;
using Techlabs.Euphoria.Kernel.Service.SMS;
using Techlabs.Euphoria.Kernel.Specification;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId}/tickets")]
    public class TicketController : ApiController
    {
        private IRepository<Organizer> _organizerRepository = new EntityFrameworkRepository<Organizer>();
        private IRepository<Ticket> _ticketRepository = new EntityFrameworkRepository<Ticket>();
        private IRepository<TicketType> _ticketTypeRepository = new EntityFrameworkRepository<TicketType>();
        private IRepository<Visitor> _visitorRepository = new EntityFrameworkRepository<Visitor>();
        private TokenGenerationService _tokenService = new TokenGenerationService();
        private IRepository<Exhibition> _exhibitionRepository = new EntityFrameworkRepository<Exhibition>();
        private IRepository<Transaction> _transactionRepository = new EntityFrameworkRepository<Transaction>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        [ResponseType(typeof(TicketDTO))]
        async public Task<IHttpActionResult> Get(Guid organizerId, Guid id)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var ticket = _ticketRepository.GetById(id);
                if (ticket == null)
                    return NotFound();

                TicketType ticketType = ticket.TicketType;
                Venue venue = ticketType.Venue;

                var criteria = new ExhibitionSearchCriteria { TicketDate = ticket.TicketDate };
                var sepcification = new ExhibitionSpecificationForSearch(criteria);
                var exhibitions = _exhibitionRepository.Find(sepcification);
                string Bgcolor;

                if (exhibitions != null && exhibitions.Count() != 0)
                {
                    TimeSpan dayCount = ticket.TicketDate.Date - exhibitions.FirstOrDefault().StartDate.Date;
                    if (dayCount.Days < 0)
                    {
                        return NotFound();
                    }

                    TicketColors colors = new TicketColors();
                    if (ticket.IsPayOnLocation == true)
                    {
                        Bgcolor = "#FFFFFF";
                    }
                    else
                    {
                        Bgcolor = colors[dayCount.Days];
                    }

                    TicketDTO ticketDTO = new TicketDTO
                    {
                        Id = ticket.Id,
                        TicketName = ticket.TicketType.Name,
                        VenueName = ticket.TicketType.Venue.City,
                        TokenNumber = ticket.TokenNumber,
                        TicketDate = ticket.TicketDate.ToString("dd-MMM-yyyy"),
                        ExhibitionTime = venue.Exhibitions.FirstOrDefault().ExhibitionTime,
                        NumberOfTicket = ticket.NumberOfTicket,
                        TotalPriceOfTicket = ticket.TotalPriceOfTicket,
                        TicketTypeId = ticket.TicketType.Id.ToString(),
                        IsPayOnLocation = ticket.IsPayOnLocation,
                        BgColor = Bgcolor
                    };

                    foreach (Exhibition exhibition in venue.Exhibitions)
                    {
                        if (exhibition.isActive && exhibition.StartDate.Date <= ticket.TicketDate.Date && exhibition.EndDate.Date >= ticket.TicketDate.Date)
                        {
                            ticketDTO.DisplayExhibitions.Add(new DisplayExhibitionDTO
                            {
                                Name = exhibition.Name,
                                BgImage = exhibition.BgImage,
                                Logo = exhibition.Logo
                            });
                        }
                    }

                    return Ok(ticketDTO);
                }
                return NotFound();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="visitorId"></param>
        /// <returns></returns>
        [Route("visitor/{visitorId}")]
        [ResponseType(typeof(TicketDTO))]
        public IHttpActionResult GetTicketHistory(Guid organizerId, Guid visitorId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var visitor = _visitorRepository.GetById(visitorId);
                if (visitor == null)
                    return NotFound();

                var criteria = new TicketSearchCriteria { VisitorId = visitorId.ToString() };
                var sepcification = new TicketSpecificationForSearch(criteria);
                var tickets = _ticketRepository.Find(sepcification);

                if (tickets == null)
                    return NotFound();

                List<TicketHistoryDTO> ticketDTOs = new List<TicketHistoryDTO>();

                foreach (Ticket ticket in tickets)
                {
                    TicketType ticketType = ticket.TicketType;
                    Venue venue = ticketType.Venue;
                    var exhibitionCriteria = new ExhibitionSearchCriteria { TicketDate = ticket.TicketDate };
                    var exhibitionSepcification = new ExhibitionSpecificationForSearch(exhibitionCriteria);
                    var exhibitions = _exhibitionRepository.Find(exhibitionSepcification);
                    string Bgcolor;
                    if (exhibitions != null && exhibitions.Count() != 0)
                    {
                        if ((ticket.IsPayOnLocation == false && ticket.PaymentCompleted == true) || (ticket.IsPayOnLocation == true && ticket.PaymentCompleted == true))
                        {
                            TimeSpan dayCount = ticket.TicketDate.Date - exhibitions.FirstOrDefault().StartDate.Date;
                            if (dayCount.Days >= 0)
                            {
                                TicketColors colors = new TicketColors();
                                if (ticket.IsPayOnLocation == true)
                                {
                                    Bgcolor = "#FFFFFF";
                                }
                                else
                                {
                                    Bgcolor = colors[dayCount.Days];
                                }

                                TicketHistoryDTO ticketDTO = new TicketHistoryDTO
                                {
                                    Id = ticket.Id,
                                    TicketName = ticket.TicketType.Name,
                                    VenueName = ticket.TicketType.Venue.City,
                                    TokenNumber = ticket.TokenNumber,
                                    TicketDate = ticket.TicketDate.ToString("dd-MMM-yyyy"),
                                    ExhibitionTime = venue.Exhibitions.FirstOrDefault().ExhibitionTime,
                                    NumberOfTicket = ticket.NumberOfTicket,
                                    TotalPriceOfTicket = ticket.TotalPriceOfTicket,
                                    TicketTypeId = ticket.TicketType.Id.ToString(),
                                    IsPayOnLocation = ticket.IsPayOnLocation,
                                    BgColor = Bgcolor
                                };

                                foreach (Exhibition exhibition in venue.Exhibitions)
                                {
                                    if (exhibition.isActive && exhibition.StartDate.Date <= ticket.TicketDate.Date && exhibition.EndDate.Date >= ticket.TicketDate.Date)
                                    {
                                        ticketDTO.DisplayExhibitionTicket.Add(new DisplayExhitonTicketDTO
                                        {
                                            Name = exhibition.Name,
                                            BgImage = exhibition.BgImage,
                                            Logo = exhibition.Logo
                                        });
                                    }
                                }
                                ticketDTOs.Add(ticketDTO);
                            }
                        }
                    }
                }
                return Ok(ticketDTOs);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="tokenNo"></param>
        /// <returns></returns>
        [Route("token/{tokenNo}")]
        [ResponseType(typeof(TicketDTO))]
        public IHttpActionResult GetvalidTicket(Guid organizerId, string tokenNo)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var criteria = new TicketSearchCriteria { TokenNo = tokenNo };
                var sepcification = new TicketSpecificationForSearch(criteria);
                var ticket = _ticketRepository.Find(sepcification).FirstOrDefault();
                if (ticket == null)
                    return NotFound();

                TicketType ticketType = ticket.TicketType;
                Venue venue = ticketType.Venue;

                TicketDTO ticketDTO = new TicketDTO
                {
                    Id = ticket.Id,
                    TicketName = ticket.TicketType.Name,
                    VenueName = ticket.TicketType.Venue.City,
                    TokenNumber = ticket.TokenNumber,
                    TicketDate = ticket.TicketDate.ToString("dd-MMM-yyyy"),
                    ExhibitionTime = venue.Exhibitions.FirstOrDefault().ExhibitionTime,
                    NumberOfTicket = ticket.NumberOfTicket,
                    TotalPriceOfTicket = ticket.TotalPriceOfTicket,
                    TicketTypeId = ticket.TicketType.Id.ToString(),
                    IsPayOnLocation = ticket.IsPayOnLocation
                };

                foreach (Exhibition ex in venue.Exhibitions)
                {
                    ticketDTO.DisplayExhibitions.Add(new DisplayExhibitionDTO() { Name = ex.Name });
                }
                return Ok(ticketDTO);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="tokenNo"></param>
        /// <returns></returns>
        [Route("token/{tokenNo}/issue")]
        public IHttpActionResult PutIssueTicket(Guid organizerId, string tokenNo)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var criteria = new TicketSearchCriteria { TokenNo = tokenNo };
                var sepcification = new TicketSpecificationForSearch(criteria);
                var ticket = _ticketRepository.Find(sepcification).FirstOrDefault();
                if (ticket == null)
                    return NotFound();

                var isIssued = ticket.IssueTicket();
                unitOfWork.SaveChanges();

                string status = "";

                if (isIssued)
                    status = "Ticket Issued";
                else
                    status = "Ticket is expired or used";

                return Ok(new
                {
                    Message = status
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="visitorId"></param>
        /// <param name="ticketDTO"></param>
        /// <returns></returns>
        [Route("visitor/{visitorId}")]
        [ModelValidator]
        [ResponseType(typeof(TicketDTO))]
      async public Task< IHttpActionResult> PostBookNow(Guid organizerId, Guid visitorId, TicketDTO ticketDTO)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var ticketType = _ticketTypeRepository.GetById(new Guid(ticketDTO.TicketTypeId));
                if (ticketType == null)
                    return NotFound();

                var visitor = _visitorRepository.GetById(visitorId);
                if (visitor == null)
                    return NotFound();

                string tokenNumber = _tokenService.Generate();
                string txnIdForPaymentGateWay = GetPaymentGateWayTxnId();

                var ticket = Ticket.Create(tokenNumber, Convert.ToDateTime(ticketDTO.TicketDate), ticketDTO.NumberOfTicket, ticketDTO.TotalPriceOfTicket, txnIdForPaymentGateWay);

                ticket.IsPayOnLocation = ticketDTO.IsPayOnLocation;
                ticket.TicketType = ticketType;
                ticket.Visitor = visitor;
                if (ticketDTO.IsPayOnLocation == true)
                {
                    ticket.PaymentCompleted = true;
                }
                else
                {
                    ticket.PaymentCompleted = false;
                }

                if (ticketType.Name == TypeOfTicket.AllDays.ToString())
                {
                    ticket.ValidityDayCount = (int)TypeOfTicket.AllDays;
                }
                else if (ticketType.Name == TypeOfTicket.Weekend.ToString())
                {
                    ticket.ValidityDayCount = (int)TypeOfTicket.Weekend;
                }
                else if (ticketType.Name == TypeOfTicket.Single.ToString())
                {
                    ticket.ValidityDayCount = (int)TypeOfTicket.Single;
                }

                _ticketRepository.Add(ticket);

                unitOfWork.SaveChanges();

                if (ticketDTO.IsPayOnLocation)
                {
                    var criteria = new ExhibitionSearchCriteria { TicketDate = ticket.TicketDate };
                    var sepcification = new ExhibitionSpecificationForSearch(criteria);
                    var exhibitions = _exhibitionRepository.Find(sepcification);

                    var visitorName = ticket.Visitor.FirstName;
                    var tokenNumbertoSms = ticket.TokenNumber;
                    var mobileNo = ticket.Visitor.MobileNo;
                    var tickeTypeName = ticket.TicketType.Name;
                    var venue = ticket.TicketType.Venue.City;
                    var ticketDate = ticket.TicketDate.ToString("dd-MMM-yyyy");
                    var noOfTickets = ticket.NumberOfTicket;
                    string ExhibitionTime;
                    if (exhibitions != null)
                    {
                        ExhibitionTime = exhibitions.FirstOrDefault().ExhibitionTime;
                    }
                    else
                    {
                        ExhibitionTime = "";
                    }
                    //var paymentComplete = "Pay at Location";
                    string paymentMode;
                    string paymentType;
                    if (visitor.isMobileNoVerified == false)
                    {
                        paymentMode = "Web Ticket";
                        paymentType = "PAY AT LOCATION";
                    }
                    else
                    {
                        paymentMode = "Mobile Ticket";
                        paymentType = "PAY AT LOCATION";
                    }
                    SendSMS(paymentMode, paymentType, tokenNumbertoSms, mobileNo, tickeTypeName, venue, ticketDate, noOfTickets);
                    string logoImage = "";
                    foreach (Exhibition exhibition in exhibitions)
                    {
                        if (exhibition.isActive && exhibition.StartDate.Date <= ticket.TicketDate.Date && exhibition.EndDate.Date >= ticket.TicketDate.Date)
                        {
                            var exhibitionName = exhibition.Name;
                            var BgImage = exhibition.BgImage;
                            var Logo = exhibition.Logo;

                            logoImage += string.Format("<div style='background:url({0});'><center><img src='{1}' alt='{2}' style='align-content:center; margin:auto; width:120px; '/><br><b>{3}</b></center></div>",
                                BgImage, Logo, exhibitionName, exhibitionName);
                        }
                    }

                    var data = string.Format(logoImage + "</tr> <tr> <table style='text-align:center; width: 400px;'><tr><th style='text-align:center;'>Date</th> <th style='text-align:center;'>Time</th> <th style='text-align:center;'>Quantity</th></tr><tr><td>{0}</td> <td>{1}</td> <td style='font-size:25px;'>{2}</td></tr></table> </tr> <tr ><center style='border: 1px solid transparent; border-color: #FEBB13;'><h4>{3}</h4></center> </tr> <tr><center><h2>{4}</h2></center> </tr><tr> <div class='row' style='background-color:#febb13;padding:5px;text-align:center;'> <br> <center style='font-size:16px;font-weight:700;'>BOOKING ID : {5}</center> <br> </div></tr> </table></div></center></div> </body></html>", ticketDate, ExhibitionTime, noOfTickets, venue, paymentType, tokenNumbertoSms);
                    string htmlPage = "<html> <head> </head> <body> <div> <center><div style='width: 400px; justify-content: center; background-color:#FFFFFF; color:#000000; border: 5px solid transparent; border-radius: 4px;margin-top:80px; border-color: #FEBB13;box-shadow: 5px 5px 5px grey;'><table ><tr >" + data;
                    string fileName = "test";
                    string subject = "E-Ticket, Mega Trade Fair 2016, " + venue;
                    Task.Run(()=>SendeMail(htmlPage, fileName, visitor,subject));        
                }
                return await Get(organizerId, ticket.Id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizerId"></param>
        /// <param name="exhibitionId"></param>
        /// <returns></returns>
        [Route("exhibition/{exhibitionId}")]
        [ResponseType(typeof(TicketDTO))]
        public IHttpActionResult GetTicketReports(Guid organizerId, Guid exhibitionId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var organizer = _organizerRepository.GetById(organizerId);
                if (organizer == null)
                    return NotFound();

                var exhibition = _exhibitionRepository.GetById(exhibitionId);
                if (exhibition == null)
                    return NotFound();

                var criteria = new TicketReportSearchCriteria { StartDate = exhibition.StartDate, EndDate = exhibition.EndDate };
                var sepcification = new TicketReportSpecificationForSearch(criteria);
                var tickets = _ticketRepository.Find(sepcification);

                if (tickets == null)
                    return NotFound();

                List<TicketReportDTO> ticketReportDTOs = new List<TicketReportDTO>();

                foreach (Ticket ticket in tickets)
                {
                    string empty = null;
                    var paymentMode = empty;
                    if (ticket.IsPayOnLocation == false)
                    {
                        var transactionCriteria = new TransactionSearchCriteria { TicketId = ticket.Id };
                        var transactionSepcification = new TransactionSpecificationForSearch(transactionCriteria);
                        var transaction = _transactionRepository.Find(transactionSepcification);
                        if (transaction.Count() != 0 && !String.IsNullOrEmpty(transaction.FirstOrDefault().PaymentMode))
                        {
                            paymentMode = transaction.FirstOrDefault().PaymentMode;
                        }
                        else
                        {
                            paymentMode = "Transaction Pending";
                        }
                    }
                    else
                    {
                        paymentMode = "Pay At Location";
                    }
                    TicketReportDTO ticketDTO = new TicketReportDTO
                    {
                        isMobileVerified = ticket.Visitor.isMobileNoVerified,
                        dateOfBooking = ticket.CreatedOn.ToString("dd-MMM-yyyy"),
                        bookingId = ticket.TokenNumber,
                        emailId = ticket.Visitor.EmailId,
                        phoneNo = ticket.Visitor.MobileNo,
                        ticketDate = ticket.TicketDate.ToString("dd-MMM-yyyy"),
                        numberOfTicket = ticket.NumberOfTicket.ToString(),
                        ticketAmount = ticket.TotalPriceOfTicket.ToString(),
                        paymentMode = paymentMode,
                        isPaymentCompleted = ticket.PaymentCompleted,
                        pincode = ticket.Visitor.Pincode.ToString()
                    };

                    ticketReportDTOs.Add(ticketDTO);
                }
                return Ok(ticketReportDTOs);
            }
        }

        private TicketDTO GetDTO(Ticket ticket)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return new TicketDTO
                {
                    Id = ticket.Id,
                    TokenNumber = ticket.TokenNumber,
                    TicketDate = ticket.TicketDate.ToString("dd-MMM-yyyy"),
                    NumberOfTicket = ticket.NumberOfTicket,
                    TotalPriceOfTicket = ticket.TotalPriceOfTicket,
                    TicketName = ticket.TicketType == null ? "noTicketType" : ticket.TicketType.Name,
                    IsPayOnLocation = ticket.IsPayOnLocation
                };
            }
        }

        private TicketDTO GetDTO(Ticket ticket, Venue venue, Exhibition exhibition)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return new TicketDTO
                {
                    Id = ticket.Id,
                    TokenNumber = ticket.TokenNumber,
                    TicketDate = ticket.TicketDate.ToString("dd-MMM-yyyy"),
                    NumberOfTicket = ticket.NumberOfTicket,
                    TotalPriceOfTicket = ticket.TotalPriceOfTicket,
                    TicketName = ticket.TicketType.Name,
                    VenueName = venue.City,
                    IsPayOnLocation = ticket.IsPayOnLocation
                };
            }
        }

        private string GetPaymentGateWayTxnId()
        {
            TokenGenerationService txnIdPaymentGateWaySvc = new TokenGenerationService();
            txnIdPaymentGateWaySvc.Exclusions = "`~!@#$%^&*()-_=+[]{}\\|;:'\",<.>/?";
            txnIdPaymentGateWaySvc.Minimum = 16;
            txnIdPaymentGateWaySvc.Maximum = 20;

            return txnIdPaymentGateWaySvc.Generate();
        }

        private void SendSMS(string paymentMode, string paymentType, string tokenNumbertoSms, string mobileNo, string ticketType, string venue, string ticketDate, int noOfTicktets)
        {
            var messageText = "Thank you for Booking, {0}, Type : {1}, Venue : {2}, TicketNo : {3}, Quantity : {4}, Date : {5}";
            messageText = string.Format(messageText, paymentMode, paymentType, venue, tokenNumbertoSms, noOfTicktets, ticketDate);

            SolutionInfoTechSMSService smsService = new SolutionInfoTechSMSService();
            NotificationService notificationService = new NotificationService(smsService, mobileNo, messageText);
            notificationService.Send();
        }

        private void SendeMail(string htmlPage, string fileName, Visitor visitor, string subject)
        {
            try
            {
                //string pdfFile = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PdfFolderLocation"]) + "\\" + fileName + ".pdf";

                // PDFGenerator generator = new PDFGenerator();
                //  generator.CreatePDFFromHTMLFile(htmlPage,pdfFile);

                IEmailService emailService = new SendGridEmailService();
                emailService.Send(visitor.EmailId, subject, htmlPage);
            }
            catch (Exception ex)
            {
                //TODO:need to handle exception or log
            }
            
        }
    }
}
