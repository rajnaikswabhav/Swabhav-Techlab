using System;
using System.Collections.Generic;
using System.IO;

namespace CityCodeApp
{
    class StateService
    {
        private Dictionary<string, string> stateDictionary;

        public StateService()
        {
            stateDictionary = new Dictionary<string, string>();
        }

        public void IntilizeDictionary()
        {
            string line;
            try
            {
                StreamReader reader = new StreamReader("cityDetail.csv");
                line = reader.ReadLine();

                while (line != null)
                {
                    char[] commaSeparators = new char[] { ',' };
                    string[] city = line.Split(commaSeparators);
                    stateDictionary.Add(city[0], city[1]);
                    line = reader.ReadLine();
                }
                reader.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public Dictionary<string, string> Search(string code)
        {
            int size = code.Length;
            string splitedCode = code.Trim();
            splitedCode = splitedCode.ToUpper();

            Dictionary<string, string> searchStateDictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> entry in stateDictionary)
            {

                if (entry.Key.StartsWith(splitedCode))
                {
                    searchStateDictionary.Add(entry.Key, entry.Value);
                }

                else if (entry.Value.Contains(code))
                {
                    searchStateDictionary.Add(entry.Key, entry.Value);
                }
            }
            return searchStateDictionary;
        }
    }
}
