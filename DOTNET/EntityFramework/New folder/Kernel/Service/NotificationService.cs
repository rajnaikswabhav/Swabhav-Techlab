using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Service.SMS;

namespace Techlabs.Euphoria.Kernel.Service
{
    public class NotificationService
    {
        private ISmsService _smsService;
        private string _mobileNo;
        private string _text;
        private string _email;

        public NotificationService(ISmsService smsService, string mobileNo, string text, string email = null)
        {
            _smsService = smsService;
            _mobileNo = mobileNo;
            _text = text;
            _email = email;

        }
        private void SendSMS()
        {


            _smsService.Send(_mobileNo, _text);
        }

        private void SendEmail()
        {
        }

        public void Send()
        {

            try
            {
                // SendEmail();
                SendSMS();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
