using System;
using System.Collections.Generic;
using System.Text;

namespace CityCodeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StateService stateService = new StateService();
            Dictionary<string, string> stateList = new Dictionary<string, string>();
            stateService.IntilizeDictionary();
            stateList = stateService.Search("Guj");

            foreach (KeyValuePair<string, string> entry in stateList)
            {
                Console.WriteLine("{0},{1}", entry.Key, entry.Value);
            }
        }
    }
}
