using System;
using System.Linq;

namespace ReadWriteSeparate
{
    class Program
    {
        static void Main(string[] args)
        {
            // TestDb();
            TestPerformance.Run();

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
    }
}
