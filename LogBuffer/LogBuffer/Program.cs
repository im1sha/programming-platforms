using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace LogBuffer
{
    class Program
    {
        static LogBuffer Lb;

        static void Main(string[] args)
        {
            string StorageFile = @"c:/users/mike/desktop/1.txt";
            File.WriteAllText(StorageFile, "");
            Lb = new LogBuffer(10, 100, StorageFile);
            TestLogBuffer();
            //Thread.Sleep(1000);
        }

        private static void TestLogBuffer()
        {
            for (int i = 1; i <= 1000; i++)
            {
                Lb.Add(i.ToString());
            }
        }
    }
}

