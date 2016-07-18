using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Infrastructure;
using ConsoleApp.Model;
using Xunit;

namespace ConsoleApp
{
    public class TestExpressionAnalyzer : BaseTest
    {
        [Fact]
        public void TestHelloworld()
        {
            // Expression<Func<User, bool>> exp = u => u.Name.Contains("lilei");
            Expression<Func<User, bool>> exp = u => u.Name == "Test";

            var result = new ExpressionAnalyzer(exp).GetAnalysisResult();
            var stack = string.Join(" ", result.StackList);

            Assert.Equal(1, 1);
        }

        [Fact]
        public void Test2()
        {
            var param = new User()
            {
                Name = "lichun",
                Age = 10
            };
            QueryCommand<User> f = new QueryCommand<User>();
            var command = f.Where(u => u.Name == "cengdu").GetSelectCommand(u => new
            {
                u.Id,
                u.Name,
                u.Age
            });

            ShowCommand(command);

            Assert.Equal(1, 1);
        }

        private void ShowCommand<TResult>(Command<TResult> command)
        {
            Console.WriteLine("链接字符串：");
            Console.WriteLine(command.ConStr);
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Sql命令：");
            Console.WriteLine(command.CommandText);

            Console.WriteLine("----------------------------------");
            if (command.Params != null)
            {
                Console.WriteLine("参数：");
                foreach (var item in command.Params)
                {
                    Console.WriteLine("{0}  →  {1}", item.Key, item.Value);
                }
            }

        }
    }
}
