using ConsoleApp.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class TestDal
    {
        public int GetCount()
        {
            using (var context = new ATSEntities())
            {
                return context.T_Binding.Count();
            }
        }
    }
}
