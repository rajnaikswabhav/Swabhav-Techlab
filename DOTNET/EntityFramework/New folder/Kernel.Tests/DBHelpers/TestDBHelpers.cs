using DBHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Tests.DBHelpers
{

    public class OrganizerDTO
    {
        public Guid Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    class TestDBHelpers
    {

        // Reports:
        // List of visitors who made online payment
        // List of

        public static void _Main(String[] args) {

            var schema = "azure";

            var db = new DBHelper("EuphoriaContext");
            var count = db.ExecuteScalar<int>(string.Format("select count(*) from {0}.Organizer",schema));
            Console.WriteLine(count);



             List<OrganizerDTO> organizers = db.ExecuteList<OrganizerDTO>(string.Format("select * from {0}.Organizer", schema));

            foreach (OrganizerDTO organizer in organizers)
            {
                Console.WriteLine(organizer.Name);
                Console.WriteLine(organizer.Description);
            }


            Console.WriteLine("End..");
            Console.ReadLine();


        }

    }
}
