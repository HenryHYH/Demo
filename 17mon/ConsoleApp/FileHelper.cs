using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class FileHelper
    {
        public static IList<string> Read()
        {
            return System.IO.File.ReadAllLines(@"D:\ip.txt").ToList();
        }
    }
}
