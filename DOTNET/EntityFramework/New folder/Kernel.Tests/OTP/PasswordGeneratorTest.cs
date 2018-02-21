namespace WorkingCode.CodeProject.Tests.PwdGen
{
    using System;
    using System.Collections.Generic;
    using WorkingCode.CodeProject.PwdGen;


    public class PasswordGeneratorFixture
	{
       
        public static void _Main() {

            testTranactionIDLength();
          
            Console.ReadKey();
        }

        private static void testTranactionIDLength()
        {

            PasswordGenerator passwordGenerator = new PasswordGenerator();
            passwordGenerator.Exclusions = "`~!@#$%^&*()-_=+[]{}\\|;:'\",<.>/?";
            passwordGenerator.Minimum = 16;
            passwordGenerator.Maximum = 20;


            for (int i = 1; i <= 10; i++)
            {
                string token = passwordGenerator.Generate();
               Console.WriteLine(token);
               Console.WriteLine(token.Length);
             
            }


        }

        static void  testOTPLength()
        {
            PasswordGenerator generator = new PasswordGenerator();
            // generator.Exclusions = "abcdefghijklmnopqrstuvwxyz";



            HashSet<string> tokens = new HashSet<string>();




            for (int i = 1; i <= 100000; i++)
            {
                string token = generator.Generate();
                tokens.Add(token);
                Console.WriteLine(token);
                Console.WriteLine(token.Length);
                if (token.Length > 6)
                    Console.WriteLine("================TokenSize > 6 ===========");
            }


            Console.WriteLine("End of main tokens in set is " + tokens.Count);

        }
		
	}
}