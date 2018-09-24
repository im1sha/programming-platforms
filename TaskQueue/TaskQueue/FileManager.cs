using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskQueue
{
    public class FileManager
    {
        /// <summary>
        /// TaskQueue of performing operations under files
        /// </summary>
        private TaskQueue WorkQueue;


        public FileManager(TaskQueue WorkQueue)
        {
            this.WorkQueue = WorkQueue;
        }


        /// <summary>
        /// Copies directory with subfolders or without 
        /// </summary>
        /// <param name="sourceDirName">Source folder</param>
        /// <param name="destDirName">Destination folder</param>
        /// <param name="copySubDirs">Should copy subfolders</param>
        /// <param name="overwrite">Overwrite existing files with identical names</param>
        public void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, bool overwrite)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string newPath = Path.Combine(destDirName, file.Name);

                if (WorkQueue == null)
                {
                    ThreadPool.GetMinThreads(out int minThreads, out int minPorts);
                    WorkQueue = new TaskQueue(minThreads);
                }

                var unitOfWork = new UnitOfWork(CopyFile, new object[] { file, newPath, overwrite });
                WorkQueue.EnqueueTask(unitOfWork);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs, overwrite);
                }
            }          
        }

        /// <summary>
        /// Copies file using data
        /// </summary>
        /// <param name="o">Represents data required to copy file as following 
        /// object[] { File file, string newPath, bool overwrite } where 
        /// file is source file, 
        /// newPath is new path to file</param>
        private void CopyFile(object o)
        {
            object[] args = (object[])o;

            FileInfo file = (FileInfo)args[0];
            string newPath = (string)args[1];
            bool overwrite = (bool)args[2];

            file.CopyTo(newPath, overwrite);
        }
    }
}
