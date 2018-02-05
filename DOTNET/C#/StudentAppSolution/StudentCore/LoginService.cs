using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentCore
{
    public class LoginService
    {
        private Dictionary<string, string> userDetails = new Dictionary<string, string>();

        public LoginService()
        {
            InitializeDictionary();
        }
        private void InitializeDictionary()
        {
            userDetails.Add("Akash", "akash");
            userDetails.Add("Brijesh", "brijesh");
        }

        public bool Login(string userName, string password) {
            
            foreach (var details in userDetails)
            {
                if(details.Key.Equals(userName) && details.Value.Equals(password))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
