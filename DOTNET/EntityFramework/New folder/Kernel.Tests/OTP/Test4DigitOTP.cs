using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingCode.CodeProject.PwdGen;

namespace Kernel.Tests.OTP
{
   public class Test4DigitOTP
    {

        public static void _Main() {

            // Random random = new Random();
            //   int value = random.Next(10000);////will generate a number 0 to 9999

            //  Console.WriteLine(value);

            PasswordGenerator generator = new PasswordGenerator();
            generator.Exclusions = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (int i = 1; i <= 20; i++)
            {
                Console.WriteLine(generator.Generate());
            }

            Console.ReadLine();


        }
    }
}
