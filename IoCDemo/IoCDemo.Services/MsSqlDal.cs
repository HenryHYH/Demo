using System;

namespace IoCDemo.Services
{
    public class MsSqlDal : IDal
    {
        public void Save()
        {
            Console.WriteLine("Ms SQL Saved");
        }
    }
}
