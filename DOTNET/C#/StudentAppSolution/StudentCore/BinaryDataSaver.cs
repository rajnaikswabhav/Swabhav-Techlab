using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StudentCore
{
    public class BinaryDataSaver
    {
        public void Save(List<Student> studentList)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream("student.binary", FileMode.Create, FileAccess.Write);
            try
            {
                using (fileStream)
                {
                    binaryFormatter.Serialize(fileStream, studentList);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
