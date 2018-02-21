using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEFApp
{
    public class Logger
    {
        public static void Log(String msg)
        {
            StreamWriter writer = new StreamWriter("logData.txt", true);
            writer.Write(msg);
            writer.Close();
        }
    }
}
