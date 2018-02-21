using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Tests.SMS
{
    class TestSMSService
    {

       async void sendSMS() {


            var userName= "GSMKTG";
            var password = "pass1234";
            var senderId = "GSMKTG";
            Dictionary<string, string> customerNumbersandNames = new Dictionary<string, string>();
            customerNumbersandNames.Add("9594178582", "Mr.Kannan");
            //customerNumbersandNames.Add("9821192053", "Mr.Bhavesh");
           // customerNumbersandNames.Add("8097905706", "Ms.Ruhina");
          //  customerNumbersandNames.Add("9773131242", "Mr.Mayur");
            customerNumbersandNames.Add("9930527293", "Ms.Aditi");
            customerNumbersandNames.Add("9619200490", "Ms.Dhwani");
          //  customerNumbersandNames.Add("9773647624", "Ms.Ruhi");


            var message = "Hello {0} I am back, where are you ?? ";



            foreach (var numberNames in customerNumbersandNames)
            {
                string serviceUrl = string.Format("http://sms.solution-infotech.com/api.php?"
                    + "username={0}&password={1}&route=8&"
                    + "sender={2}&mobile[]={3}&message[]={4}", userName, password, senderId,numberNames.Key, string.Format(message, numberNames.Value));

                var client = new HttpClient();
                var response = await client.GetAsync(serviceUrl);
                var content = await response.Content.ReadAsStringAsync();

                Console.WriteLine(content);

            }

            Console.WriteLine("SMS sent");
        }
        public static void _Main()
        {


            new TestSMSService().sendSMS();
            Console.ReadLine();

        }
    }
}
