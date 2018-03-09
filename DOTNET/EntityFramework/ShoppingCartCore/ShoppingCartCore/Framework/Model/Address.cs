using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Framework.Model
{
    public class Address : Entity
    {

        public Guid UserId  { get; set; }
        public string StreetAddress  { get; set; }
        public string  City { get; set; }
        public string State  { get; set; }
        public string Country  { get; set; }
        public int PinCode { get; set; }

        public override string ToString()
        {
            return StreetAddress + " , " + City + " , " + State + " , " + Country;
        }
    }
}
