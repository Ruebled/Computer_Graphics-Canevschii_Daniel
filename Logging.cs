using System;
using System.IO;
using System.Text;

namespace Graphics_Homework
{
    public sealed class Logging
    {
        private String FileName = "log.txt";
        private String FilePath;
        private static bool EnableFileWrite = false;
        private static FileStream file;

        private Logging() 
        {
            file = File.Create(this.FileName);
            FilePath = Directory.GetCurrentDirectory() + "\\" + this.FileName;

            if (File.Exists(this.FilePath))
            {
                EnableFileWrite = true;
            }
            ConsoleOutput("Log file can be found at: " + FilePath);
        }
        private static Logging Instance = null;

        public static void print(string message) 
        {
            if(Instance == null)
            {
                Instance = new Logging();
            }

            ConsoleOutput(message);
            FileOutput(message);
        }

        private static void ConsoleOutput(String _string)
        {
            Console.WriteLine(_string);
        }

        private static void FileOutput(String _string)
        {
            if (EnableFileWrite)
            {
                byte[] b_string = new UTF8Encoding(true).GetBytes(_string);
                file.Write(b_string, 0, _string.Length);
            }
        }

        ~Logging() { 
            file.Close(); 
        }
    }
}
