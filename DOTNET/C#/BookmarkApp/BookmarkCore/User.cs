using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkCore
{
    public class User
    {
        private string userName;
        private string password;
        private string email;
        private string location;

        public User(string userName,string password,string email,string location)
        {
            this.userName = userName;
            this.password = password;
            this.email = email;
            this.location = location;
        }

        public string UserName { get { return userName; } }
        public string Password { get { return password; } }
        public string Email { get { return email; } }
        public string Location { get { return location; } }
    }
}
