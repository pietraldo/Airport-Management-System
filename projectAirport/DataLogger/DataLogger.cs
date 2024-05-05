using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    internal class DataLogger
    {
        private static string? fileName = null;
        public static string GetFileName()
        {
            if (fileName != null)
                return fileName;

            // create folder if doesn't exists
            string folderPath = Environment.CurrentDirectory + "/logs";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string dateTime = $"{DateTime.Now:dd_MM_yyyy}";
            int num = 1;
            while (File.Exists($"{folderPath}/{dateTime}_{num}.txt")) num++;

            fileName = $"{folderPath}/{dateTime}_{num}.txt";
            return fileName;
        }
        public static void LogToFile(string data)
        {
            // writing to file
            string fileName = GetFileName();
            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                sw.WriteLine(data);
            }
        }
    }
}
