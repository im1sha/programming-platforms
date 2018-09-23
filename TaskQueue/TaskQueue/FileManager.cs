using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskQueue
{
    public class FileManager
    {
        static void Main(string[] args)
        {
            //if (args.Length != 2)
            //{
            //    Console.WriteLine("wrong parameters");
            //    return;
            //}

            const int queueAmount = 100;
            TaskQueue T = new TaskQueue(queueAmount);
            UnitOfWork u = new UnitOfWork(DoWork, null);

            for (int i = 0; i < queueAmount; i++)
            {
                T.EnqueueTask(u);
            }

            T.Close();

            Console.WriteLine("press any key");
            Console.ReadLine();
        }

        //public static void Main()
        //{
        //    // Copy from the current directory, include subdirectories
        //    DirectoryCopy(@"C:\Users\Foxx\Desktop\abc", @"C:\Users\Foxx\Desktop\xdd", true);
        //}

        //private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        //{
        //    // Get the subdirectories for the specified directory
        //    DirectoryInfo dir = new DirectoryInfo(sourceDirName);
        //    if (!dir.Exists)
        //    {
        //        throw new DirectoryNotFoundException(
        //            "Source directory does not exist or could not be found: "
        //            + sourceDirName);
        //    }
        //    DirectoryInfo[] dirs = dir.GetDirectories();

        //    // If the destination directory doesn't exist, create it
        //    if (!Directory.Exists(destDirName))
        //    {
        //        Directory.CreateDirectory(destDirName);
        //    }

        //    // Get the files in the directory and copy them to the new location
        //    FileInfo[] files = dir.GetFiles();
        //    foreach (FileInfo file in files)
        //    {
        //        string temppath = Path.Combine(destDirName, file.Name);
        //        file.CopyTo(temppath, false);
        //    }

        //    // If copying subdirectories, copy them and their contents to new location
        //    if (copySubDirs)
        //    {
        //        foreach (DirectoryInfo subdir in dirs)
        //        {
        //            string temppath = Path.Combine(destDirName, subdir.Name);
        //            DirectoryCopy(subdir.FullName, temppath, copySubDirs);
        //        }
        //    }
        //}

        private static void DoWork(object o)
        {
            Console.WriteLine("started");
            var rand = new Random();
            int total = rand.Next(100000);
            int val = 1;
            for (int j = 0; j < total; j++)
            {
                val *= 2;
            }
            Console.WriteLine("ended");
        }
    }
}
