using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    internal class DataLogger
    {
        static readonly int numberOfApplicationsStarts;
        public static void LogToFile(string data)
        {
            // create folder if doesn't exists
            string folderPath = Environment.CurrentDirectory + "\\logs";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // writing to file
            string fileName = $"{DateTime.Now:dd_MM_yyyy}.txt";
            using (StreamWriter sw = new StreamWriter(folderPath+"\\"+fileName, true))
            {
                sw.WriteLine(data);
            }
        }
    }
}
