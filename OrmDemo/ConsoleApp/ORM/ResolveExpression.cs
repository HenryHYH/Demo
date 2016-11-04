using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.ORM
{
    public class ResolveExpression
    {
        #region Fields

        private int index = 0;

        #endregion

        #region Properties

        public Dictionary<string, object> Parameters { get; set; }

        public string SqlWhere { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 解析lamdba，生成Sql查询条件
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public void ResolveToSql(Expression expression)
        {
            this.index = 0;
            this.Parameters = new Dictionary<string, object>();
            this.SqlWhere = Resolve(expression);
        }

        #endregion

        #region Private methods

        private object GetValue(Expression expression)
        {
            if (expression is ConstantExpression)
                return (expression as ConstantExpression).Value;
            if (expression is UnaryExpression)
            {
                UnaryExpression unary = expression as UnaryExpression;
                LambdaExpression lambda = Expression.Lambda(unary.Operand);
                Delegate fn = lambda.Compile();
                return fn.DynamicInvoke(null);
            }
            if (expression is MemberExpression)
            {
                MemberExpression member = expression as MemberExpression;
                string name = member.Member.Name;
                var constant = member.Expression as ConstantExpression;
                if (constant == null)
                    throw new Exception("取值时发生异常" + member);
                return constant.Value.GetType().GetFields().First(x => x.Name == name).GetValue(constant.Value);
            }
            throw new Exception("无法获取值" + expression);
        }

        private string Resolve(Expression expression)
        {
            if (expression is LambdaExpression)
            {
                LambdaExpression lambda = expression as LambdaExpression;
                expression = lambda.Body;
                return Resolve(expression);
            }
            if (expression is BinaryExpression)//解析二元运算符
            {
                BinaryExpression binary = expression as BinaryExpression;
                if (binary.Left is MemberExpression)
                {
                    object value = GetValue(binary.Right);
                    return ResolveFunc(binary.Left, value, binary.NodeType);
                }
                if (binary.Left is MethodCallExpression && (binary.Right is UnaryExpression || binary.Right is MemberExpression))
                {
                    object value = GetValue(binary.Right);
                    return ResolveLinqToObject(binary.Left, value, binary.NodeType);
                }
            }
            if (expression is UnaryExpression)//解析一元运算符
            {
                UnaryExpression unary = expression as UnaryExpression;
                if (unary.Operand is MethodCallExpression)
                {
                    return ResolveLinqToObject(unary.Operand, false);
                }
                if (unary.Operand is MemberExpression)
                {
                    return ResolveFunc(unary.Operand, false, ExpressionType.Equal);
                }
            }
            if (expression is MethodCallExpression)//解析扩展方法
            {
                return ResolveLinqToObject(expression, true);
            }
            if (expression is MemberExpression)//解析属性。。如x.Deletion
            {
                return ResolveFunc(expression, true, ExpressionType.Equal);
            }
            var body = expression as BinaryExpression;
            if (body == null)
                throw new Exception("无法解析" + expression);
            var Operator = GetOperator(body.NodeType);
            var Left = Resolve(body.Left);
            var Right = Resolve(body.Right);
            string Result = string.Format("({0} {1} {2})", Left, Operator, Right);
            return Result;
        }

        /// <summary>
        /// 根据条件生成对应的sql查询操作符
        /// </summary>
        /// <param name="expressiontype"></param>
        /// <returns></returns>
        private string GetOperator(ExpressionType expressiontype)
        {
            switch (expressiontype)
            {
                case ExpressionType.And:
                    return "AND";
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.Or:
                    return "OR";
                case ExpressionType.OrElse:
                    return "OR";
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                default:
                    throw new Exception(string.Format("不支持{0}此种运算符查找！" + expressiontype));
            }
        }

        private string ResolveFunc(Expression left, object value, ExpressionType expressiontype)
        {
            string Name = (left as MemberExpression).Member.Name;
            string Operator = GetOperator(expressiontype);
            string Value = value.ToString();
            string CompName = SetArgument(Name, Value);
            string Result = string.Format("({0} {1} {2})", Name, Operator, CompName);
            return Result;
        }

        private string ResolveLinqToObject(Expression expression, object value, ExpressionType? expressiontype = null)
        {
            var methodCall = expression as MethodCallExpression;
            var methodName = methodCall.Method.Name;
            switch (methodName)//这里其实还可以改成反射调用，不用写switch
            {
                case "Contains":
                    if (methodCall.Object != null)
                        return Like(methodCall);
                    return In(methodCall, value);
                case "StartsWith":
                    return Like(methodCall, "{0}%");
                case "EndsWith":
                    return Like(methodCall, "%{0}");
                case "Count":
                    return Len(methodCall, value, expressiontype.Value);
                case "LongCount":
                    return Len(methodCall, value, expressiontype.Value);
                default:
                    throw new Exception(string.Format("不支持{0}方法的查找！", methodName));
            }
        }

        private string SetArgument(string name, string value)
        {
            name = "@" + name;
            string temp = name;
            while (Parameters.ContainsKey(temp))
            {
                temp = name + index;
                index = index + 1;
            }
            Parameters[temp] = value;
            return temp;
        }

        private string In(MethodCallExpression expression, object isTrue)
        {
            var Argument1 = expression.Arguments[0];
            var Argument2 = expression.Arguments[1] as MemberExpression;
            var fieldValue = GetValue(Argument1);
            object[] array = fieldValue as object[];
            List<string> SetInPara = new List<string>();
            for (int i = 0; i < array.Length; i++)
            {
                string Name_para = "InParameter" + i;
                string Value = array[i].ToString();
                string Key = SetArgument(Name_para, Value);
                SetInPara.Add(Key);
            }
            string Name = Argument2.Member.Name;
            string Operator = Convert.ToBoolean(isTrue) ? "IN" : " NOT IN";
            string CompName = string.Join(",", SetInPara);
            string Result = string.Format("{0} {1} ({2})", Name, Operator, CompName);
            return Result;
        }

        private string Like(MethodCallExpression expression, string format = "%{0}%")
        {
            Expression argument = expression.Arguments[0];
            object Temp_Vale = GetValue(argument);
            string Value = string.Format(format, Temp_Vale);
            string Name = (expression.Object as MemberExpression).Member.Name;
            string CompName = SetArgument(Name, Value);
            string Result = string.Format("{0} LIKE {1}", Name, CompName);
            return Result;
        }

        private string Len(MethodCallExpression expression, object value, ExpressionType expressiontype)
        {
            object Name = (expression.Arguments[0] as MemberExpression).Member.Name;
            string Operator = GetOperator(expressiontype);
            string CompName = SetArgument(Name.ToString(), value.ToString());
            string Result = string.Format("LEN({0}){1}{2}", Name, Operator, CompName);
            return Result;
        }

        #endregion
    }
}
