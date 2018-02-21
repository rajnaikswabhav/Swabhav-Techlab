using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Techlabs.Euphoria.Kernel.Service.SMS
{
    public class SolutionInfoTechSMSService : ISmsService
    {
        private async Task<string> SendSMSAsync(string mobileNo, string messageText)
        {

            var senderId = "GSMKTG";
            var route = "4";
            var country = "91";
            string serviceUrl = string.Format("http://54.254.154.166/api/sendhttp.php?authkey=125890A35YJcU257e11577"
                    + "&mobiles={0}&message={1}"
                    + "&sender={2}&route={3}&country={4}", mobileNo, messageText, senderId, route, country);

            var client = new HttpClient();
            var response = await client.GetAsync(serviceUrl);
            var content = await response.Content.ReadAsStringAsync();


            return content;
        }

        public void Send(string mobileNo, string text)
        {
            SendSMSAsync(mobileNo, text);
        }
    }
}
