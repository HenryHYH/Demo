using System;
using System.IO;

namespace ConsoleApp
{
    public class FileWatcher
    {
        private readonly FileSystemWatcher watcher;

        public FileWatcher(string path, string filter)
        {
            watcher = new FileSystemWatcher(path, filter);
            watcher.Changed += Watcher_Changed;
            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = true;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                // 修复执行多次的问题
                watcher.EnableRaisingEvents = false;

                Console.WriteLine("{0}_{1}_{2}",
                                  e.FullPath,
                                  e.Name,
                                  e.ChangeType.ToString());
            }
            finally
            {
                watcher.EnableRaisingEvents = true;
            }
        }
    }
}
