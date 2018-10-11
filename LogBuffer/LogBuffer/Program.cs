using System;
using System.IO;

namespace LogBuffer
{
    class Program
    {
        static LogBuffer Lb;
        static string StorageFile;

        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException("Path to file should be passed");
            }

            StorageFile = args[0];
            int.TryParse(args[1], out int bufferSize);
            int.TryParse(args[2], out int releaseTime);

            using (Lb = new LogBuffer(bufferSize, releaseTime, StorageFile))
            {
                TestLogBuffer();
            }
        }

        private static void TestLogBuffer()
        {
            File.WriteAllText(StorageFile, "");
            for (int i = 1; i <= 9999; i++)
            {
                Lb.Add(i.ToString());
            }
        }
    }
}

