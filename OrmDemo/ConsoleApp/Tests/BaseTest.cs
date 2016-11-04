using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApp.ORM;

namespace ConsoleApp.Tests
{
    public class BaseTest
    {
        internal readonly IDataBase db = null;

        public BaseTest()
        {
            this.db = new DBSql();
        }
    }
}
