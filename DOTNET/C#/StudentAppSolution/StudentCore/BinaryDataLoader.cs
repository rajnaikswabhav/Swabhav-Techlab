using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace StudentCore
{
   public class BinaryDataLoader
    {
        private List<Student> _studentList = new List<Student>();

        public BinaryDataLoader()
        {
            IntializeList();
        }

        private void IntializeList()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStreamIn = new FileStream("student.binary", FileMode.Open, FileAccess.Read);

            try
            {
                using (fileStreamIn)
                {
                    _studentList = (List<Student>)binaryFormatter.Deserialize(fileStreamIn);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Student> Students
        {
            get
            {
                return _studentList;
            }
        }
    }
}
