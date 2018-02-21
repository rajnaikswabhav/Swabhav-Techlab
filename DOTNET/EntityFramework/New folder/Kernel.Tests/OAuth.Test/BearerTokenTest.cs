//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//namespace Kernel.Tests.OAuth.Test
//{
//    class BearerTokenTest
//    {

//        private async Task<string> GetTokenAsync()
//        {
//            var client = new HttpClient();
//            var post = new Dictionary<string, string>
//                {
//                    { "grant_type", "password" },
//                    { "username", "kannan" },
//                    { "password", "kannan@123" }
//                };
//            var response = await client.PostAsync("http://localhost:51205/token",
//            new FormUrlEncodedContent(post));
//            var content = await response.Content.ReadAsStringAsync();
//            var json = JObject.Parse(content);
//            return json["access_token"].ToString();
//        }

//        public static void _Main()
//        {


//            new BearerTokenTest().GetTokenAsync();

//            Console.ReadKey();
//        }
//    }
//}
