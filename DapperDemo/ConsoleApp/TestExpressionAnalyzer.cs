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
        #region Equality

        [Theory]
        [MemberData("EqualityTestData")]
        public void TestEquality(Expression<Func<User, bool>> exp, string expectedMark)
        {
            var result = new ExpressionAnalyzer(exp).GetAnalysisResult();

            Assert.Equal(string.Format("[TAB].Age {0} @P0", expectedMark), string.Join(" ", result.StackList));
            Assert.Equal(1, result.ParamList["P0"]);
        }

        private static readonly Expression<Func<User, bool>> EQUAL_EXP = x => x.Age == 1;
        private static readonly Expression<Func<User, bool>> NOT_EQUAL_EXP = x => x.Age != 1;
        private static readonly Expression<Func<User, bool>> GRATER_THAN_EXP = x => x.Age > 1;
        private static readonly Expression<Func<User, bool>> LESS_THAN_EXP = x => x.Age < 1;
        private static readonly Expression<Func<User, bool>> GRATER_THAN_OR_EQUAL_EXP = x => x.Age >= 1;
        private static readonly Expression<Func<User, bool>> LESS_THAN_OR_EQUAL_EXP = x => x.Age <= 1;

        public static IEnumerable<object[]> EqualityTestData
        {
            get
            {
                return new[]
                {
                    new object[] { EQUAL_EXP, "=" },
                    new object[] { NOT_EQUAL_EXP, "<>" },
                    new object[] { GRATER_THAN_EXP, ">" },
                    new object[] { LESS_THAN_EXP, "<" },
                    new object[] { GRATER_THAN_OR_EQUAL_EXP, ">=" },
                    new object[] { LESS_THAN_OR_EQUAL_EXP, "<=" }
                };
            }
        }

        #endregion

        #region Like

        [Fact]
        public void TestLikeContains()
        {
            Expression<Func<User, bool>> exp = x => x.Name.Contains("Hello");

            var result = new ExpressionAnalyzer(exp).GetAnalysisResult();

            Assert.Equal("[TAB].Name LIKE '%' + @P0 + '%'", string.Join(" ", result.StackList));
            Assert.Equal("Hello", result.ParamList["P0"]);
        }

        [Fact]
        public void TestLikeStartWith()
        {
            Expression<Func<User, bool>> exp = x => x.Name.StartsWith("Hello");

            var result = new ExpressionAnalyzer(exp).GetAnalysisResult();

            Assert.Equal("[TAB].Name LIKE @P0 + '%'", string.Join(" ", result.StackList));
            Assert.Equal("Hello", result.ParamList["P0"]);
        }

        [Fact]
        public void TestLikeEndWith()
        {
            Expression<Func<User, bool>> exp = x => x.Name.EndsWith("Hello");

            var result = new ExpressionAnalyzer(exp).GetAnalysisResult();

            Assert.Equal("[TAB].Name LIKE '%' + @P0", string.Join(" ", result.StackList));
            Assert.Equal("Hello", result.ParamList["P0"]);
        }

        #endregion

        #region Logic

        [Fact]
        public void TestLogicAnd()
        {
            Expression<Func<User, bool>> exp = x => x.Name == "Test" && x.Age == 1;

            var result = new ExpressionAnalyzer(exp).GetAnalysisResult();

            Assert.Equal("( [TAB].Name = @P0 ) AND ( [TAB].Age = @P1 )", string.Join(" ", result.StackList));
            Assert.Equal("Test", result.ParamList["P0"]);
            Assert.Equal(1, result.ParamList["P1"]);
        }

        [Fact]
        public void TestLogicOr()
        {
            Expression<Func<User, bool>> exp = x => x.Name == "Test" || x.Age == 1;

            var result = new ExpressionAnalyzer(exp).GetAnalysisResult();

            Assert.Equal("( [TAB].Name = @P0 ) OR ( [TAB].Age = @P1 )", string.Join(" ", result.StackList));
            Assert.Equal("Test", result.ParamList["P0"]);
            Assert.Equal(1, result.ParamList["P1"]);
        }

        [Fact]
        public void TestLogicNot()
        {
            Expression<Func<User, bool>> exp = x => !(x.Name == "Test");

            var result = new ExpressionAnalyzer(exp).GetAnalysisResult();

            Assert.Equal("NOT ( [TAB].Name = @P0 )", string.Join(" ", result.StackList));
            Assert.Equal("Test", result.ParamList["P0"]);
        }

        [Fact]
        public void TestLogic()
        {
            Expression<Func<User, bool>> exp = x => (x.Age > 0 || x.Id == 1) && x.Name.Contains("Test");

            var result = new ExpressionAnalyzer(exp).GetAnalysisResult();

            Assert.Equal("( ( [TAB].Age > @P0 ) OR ( [TAB].Id = @P1 ) ) AND ( [TAB].Name LIKE '%' + @P2 + '%' )", string.Join(" ", result.StackList));
            Assert.Equal(0, result.ParamList["P0"]);
            Assert.Equal(1, result.ParamList["P1"]);
            Assert.Equal("Test", result.ParamList["P2"]);
        }

        #endregion

        #region Boolean

        [Fact]
        public void TestNull()
        {
            Expression<Func<User, bool>> exp = x => x.Name == null;
            var result = new ExpressionAnalyzer(exp).GetAnalysisResult();

            Assert.Equal("[TAB].Name IS NULL", string.Join(" ", result.StackList));
            Assert.Equal(0, result.ParamList.Count);
        }

        [Fact]
        public void TestTrue()
        {
            Expression<Func<User, bool>> exp = x => x.Enabled == true;
            var result = new ExpressionAnalyzer(exp).GetAnalysisResult();

            Assert.Equal("[TAB].Enabled = @P0", string.Join(" ", result.StackList));
            Assert.Equal("1", result.ParamList["P0"]);
        }

        #endregion

        #region OrderBy

        [Fact]
        public void TestOrderBy()
        {
            Expression<Func<User, object>> exp = x => x.Name;
            var result = new ExpressionAnalyzer(exp).GetAnalysisResult();

            Assert.Equal("[TAB].Name", string.Join(" ", result.StackList));
        }

        #endregion

        // [Fact]
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
