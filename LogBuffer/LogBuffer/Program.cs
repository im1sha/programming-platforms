using System.IO;

namespace LogBuffer
{
    class Program
    {
        static LogBuffer Lb;
        static string StorageFile;

        static void Main(string[] args)
        {
            StorageFile = @"c:/users/foxx/desktop/1.txt";
            
            using (Lb = new LogBuffer(11, 20, StorageFile))
            {
                TestLogBuffer();
            }
        }

        private static void TestLogBuffer()
        {
            File.WriteAllText(StorageFile, "");
            for (int i = 1; i <= 1000; i++)
            {
                Lb.Add(i.ToString());
            }
        }
    }
}

