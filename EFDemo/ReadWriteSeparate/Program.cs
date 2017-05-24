using System;
using System.Data.Common;
using System.Linq;

namespace ReadWriteSeparate
{
    class Program
    {
        static void Main(string[] args)
        {
            TestDb();
            //TestConnectionStringCompare();

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static void TestDb()
        {
            Console.WriteLine("Start");

            try
            {
                using (var context = new CustomDbContext("name=Master"))
                {
                    Console.WriteLine("Start execute");

                    var count = context.Set<Test>().Count();
                    Console.WriteLine("Count = {0};", count);

                    context.Set<Test>().Add(new Test() { Name = "A1" });
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void TestConnectionStringCompare()
        {
            //DbConnectionStringBuilder builder1 = new DbConnectionStringBuilder();
            //builder1.ConnectionString = "Value1=SomeValue;Value2=20;Value3=30;Value4=40";
            //Console.WriteLine("builder1 = " + builder1.ConnectionString);

            //DbConnectionStringBuilder builder2 = new DbConnectionStringBuilder();
            //builder2.ConnectionString = "value2=20;value3=30;VALUE4=40;Value1=SomeValue";
            //Console.WriteLine("builder2 = " + builder2.ConnectionString);

            //DbConnectionStringBuilder builder3 = new DbConnectionStringBuilder();
            //builder3.ConnectionString = "value2=20;value3=30;VALUE4=40;Value1=SOMEVALUE";
            //Console.WriteLine("builder3 = " + builder3.ConnectionString);

            //// builder1 and builder2 contain the same
            //// keys and values, in different order, and the 
            //// keys are not consistently cased. They are equivalent.
            //Console.WriteLine("builder1.EquivalentTo(builder2) = " + builder1.EquivalentTo(builder2).ToString());

            //// builder2 and builder3 contain the same key/value pairs in the 
            //// the same order, but the value casing is different, so they're
            //// not equivalent.
            //Console.WriteLine("builder2.EquivalentTo(builder3) = " + builder2.EquivalentTo(builder3).ToString());

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");

            var builderMaster = factory.CreateConnectionStringBuilder();
            builderMaster.ConnectionString = "data source=172.17.22.18;initial catalog=WDRCBServerDB;user id=Finance_DEV;password=Finance_DEVDEV;MultipleActiveResultSets=True;App=EntityFramework";
            Console.WriteLine("builderMaster = " + builderMaster.ConnectionString);

            var builderSlave = factory.CreateConnectionStringBuilder();
            builderSlave.ConnectionString = "data source=10.1.20.97;initial catalog=WDRCBServerDB;user id=Finance_DEV;password=Finance_DEVDEV;MultipleActiveResultSets=True;App=EntityFramework";
            Console.WriteLine("builderSlave = " + builderSlave.ConnectionString);

            Console.WriteLine("builderMaster.EquivalentTo(builderSlave) = " + builderMaster.EquivalentTo(builderSlave).ToString());
        }
    }
}
