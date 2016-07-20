using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp.MyOrm
{
    public class ExpressionAnalyzer
    {
        #region Fields

        /// <summary>
        /// 参数序号
        /// </summary>
        private int parameterIndex = 0;

        /// <summary>
        /// 符合 LIKE 规则的方法名
        /// </summary>
        private static readonly string[] LIKE_METHOD_NAMES = { "StartsWith", "EndsWith", "Contains" };

        /// <summary>
        /// 表达式所有参数集合
        /// </summary>
        private Dictionary<string, object> parameters;

        /// <summary>
        /// 命名参数别名
        /// </summary>
        private const string argName = "t";

        /// <summary>
        /// 解析结果
        /// </summary>
        private AnalysisData resultData;

        #endregion

        #region Ctor

        public ExpressionAnalyzer()
        {
            resultData = new AnalysisData();
            parameters = new Dictionary<string, object>();
        }

        public ExpressionAnalyzer(LambdaExpression exp, AnalysisTable table = null)
            : this()
        {
            if (table != null)
            {
                resultData.Table = table;
            }

            if (exp != null)
            {
                AppendParams(GetChildValue(exp.Body), parameters);
                foreach (var item in exp.Parameters)
                {
                    AnalysisTables(item);
                }
                AnalysisExpression(exp.Body, true);
            }
        }

        #endregion

        #region Methods

        public AnalysisData GetAnalysisResult()
        {
            return resultData;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 解析表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="isLeftChild"></param>
        private void AnalysisExpression(Expression exp, bool isLeftChild = true)
        {
            switch (exp.NodeType)
            {
                case ExpressionType.AndAlso:
                    resultData.StackList.Add("(");
                    AnalysisExpression(GetChildExpression(exp));
                    resultData.StackList.Add(")");
                    resultData.StackList.Add("AND");
                    resultData.StackList.Add("(");
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    resultData.StackList.Add(")");
                    break;
                case ExpressionType.OrElse:
                    resultData.StackList.Add("(");
                    AnalysisExpression(GetChildExpression(exp));
                    resultData.StackList.Add(")");
                    resultData.StackList.Add("OR");
                    resultData.StackList.Add("(");
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    resultData.StackList.Add(")");
                    break;
                case ExpressionType.Not:
                    resultData.StackList.Add("NOT");
                    resultData.StackList.Add("(");
                    var notExp = exp as UnaryExpression;
                    AnalysisExpression(notExp.Operand);
                    resultData.StackList.Add(")");
                    break;
                case ExpressionType.Equal:
                    AnalysisExpression(GetChildExpression(exp));
                    resultData.StackList.Add("=");
                    resultData.StackList.Add("@P" + parameterIndex);
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    break;
                case ExpressionType.NotEqual:
                    AnalysisExpression(GetChildExpression(exp));
                    resultData.StackList.Add("<>");
                    resultData.StackList.Add("@P" + parameterIndex);
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    AnalysisExpression(GetChildExpression(exp));
                    resultData.StackList.Add(">=");
                    resultData.StackList.Add("@P" + parameterIndex);
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    break;
                case ExpressionType.GreaterThan:
                    AnalysisExpression(GetChildExpression(exp));
                    resultData.StackList.Add(">");
                    resultData.StackList.Add("@P" + parameterIndex);
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    break;
                case ExpressionType.LessThan:
                    AnalysisExpression(GetChildExpression(exp));
                    resultData.StackList.Add("<");
                    resultData.StackList.Add("@P" + parameterIndex);
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    break;
                case ExpressionType.LessThanOrEqual:
                    AnalysisExpression(GetChildExpression(exp));
                    resultData.StackList.Add("<=");
                    resultData.StackList.Add("@P" + parameterIndex);
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    break;
                case ExpressionType.Call:
                    var imExp = exp as MethodCallExpression;
                    var methodName = imExp.Method.Name;
                    if (LIKE_METHOD_NAMES.Contains(methodName))
                    {
                        AnalysisExpression(imExp.Object, true);
                        resultData.StackList.Add("LIKE");
                        if (LIKE_METHOD_NAMES[0] == methodName)
                            resultData.StackList.Add(string.Format("@P{0} + '%'", parameterIndex));
                        else if (LIKE_METHOD_NAMES[1] == methodName)
                            resultData.StackList.Add(string.Format("'%' + @P{0}", parameterIndex));
                        else if (LIKE_METHOD_NAMES[2] == methodName)
                            resultData.StackList.Add(string.Format("'%' + @P{0} + '%'", parameterIndex));
                        if (imExp.Arguments.Count > 0)
                            AnalysisExpression(imExp.Arguments[0], false);
                    }
                    break;
                case ExpressionType.MemberAccess:
                    if (isLeftChild)
                    {
                        AnalysisTables(exp);
                        var mberExp = exp as MemberExpression;
                        var parentName = GetExpressionName(mberExp.Expression);
                        if (!string.IsNullOrEmpty(parentName))
                        {
                            resultData.StackList.Add(string.Format("[{0}].[{1}]", parentName, GetExpressionName(exp)));
                            break;
                        }
                        resultData.StackList.Add(GetExpressionName(exp));
                    }
                    else
                    {
                        var paramName = GetParamName(exp);
                        resultData.ParamList.Add(paramName, parameters[paramName]);
                        resultData.StackList.Add(paramName);
                    }
                    break;
                case ExpressionType.Constant:
                    var constent = exp as ConstantExpression;
                    if (constent.Value == null)
                    {
                        resultData.StackList.RemoveAt(resultData.StackList.Count - 1); // Remove parameter
                        var op = resultData.StackList.ElementAt(resultData.StackList.Count - 1);
                        resultData.StackList.RemoveAt(resultData.StackList.Count - 1); // Remove mark
                        if (string.Equals(op, "="))
                            resultData.StackList.Add("IS NULL");
                        else
                            resultData.StackList.Add("IS NOT NULL");
                        break;
                    }
                    else if (constent.Value.GetType() == typeof(bool))
                    {
                        var value = Convert.ToBoolean(constent.Value);
                        resultData.ParamList.Add("P" + (parameterIndex++), (value ? "1" : "0"));
                        break;
                    }
                    resultData.ParamList.Add("P" + (parameterIndex++), constent.Value);
                    break;
                case ExpressionType.Convert:
                    var uExp = exp as UnaryExpression;
                    AnalysisExpression(uExp.Operand, isLeftChild);
                    break;
                case ExpressionType.New:
                    var newExp = exp as NewExpression;
                    //解析查询字段
                    for (int i = 0; i < newExp.Arguments.Count; i++)
                    {
                        AnalysisExpression(newExp.Arguments[i]);
                        resultData.StackList.Add("AS");
                        resultData.StackList.Add(string.Format("'{0}'", newExp.Members[i].Name));
                    }
                    break;
                case ExpressionType.Parameter:
                    // throw new BusinessException(BusinessRes.SelectObjectMastBeAnNewObject);
                    throw new Exception("Ex");
                default:
                    break;
            }

        }

        /// <summary>
        /// 获取孩子节点
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="getLeft"></param>
        /// <returns></returns>
        private Expression GetChildExpression(Expression exp, bool getLeft = true)
        {
            var className = exp.GetType().Name;
            switch (className)
            {
                case "BinaryExpression":
                case "LogicalBinaryExpression":
                    var bExp = exp as BinaryExpression;
                    return getLeft ? bExp.Left : bExp.Right;
                case "PropertyExpression":
                case "FieldExpression":
                    var mberExp = exp as MemberExpression;
                    return mberExp;
                case "MethodBinaryExpression":
                    var mbExp = exp as BinaryExpression;
                    return getLeft ? mbExp.Left : mbExp.Right;
                case "UnaryExpression":
                    var unaryExp = exp as UnaryExpression;
                    return unaryExp;
                case "ConstantExpression":
                    var cExp = exp as ConstantExpression;
                    return cExp;
                case "InstanceMethodCallExpressionN":
                    var imExp = exp as MethodCallExpression;
                    return imExp;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取变量名
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="isLeftChild"></param>
        /// <returns></returns>
        private string GetExpressionName(Expression exp)
        {
            var className = exp.GetType().Name;
            switch (className)
            {
                case "PropertyExpression":
                case "FieldExpression":
                    var mberExp = exp as MemberExpression;
                    return string.Format("{0}", mberExp.Member.Name);
                case "TypedParameterExpression":
                    return argName;
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 获取参数名
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="isLeftChild"></param>
        /// <returns></returns>
        private string GetParamName(Expression exp)
        {
            var className = exp.GetType().Name;
            switch (className)
            {
                case "PropertyExpression":
                case "FieldExpression":
                    var mberExp = exp as MemberExpression;
                    return string.Format("@{0}", mberExp.Member.Name);
                case "TypedParameterExpression":
                    var texp = exp as ParameterExpression;
                    return string.Format("@{0}", texp.Name);
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 解析表信息
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="refTable">引用表</param>
        private void AnalysisTables(Expression exp, AnalysisTable refTable = null)
        {
            var className = exp.GetType().Name;
            switch (className)
            {
                case "PropertyExpression":
                case "FieldExpression":
                    var mberExp = exp as MemberExpression;
                    if (IsDefaultType(mberExp.Type))
                    {
                        AnalysisTables(mberExp.Expression);
                        break;
                    }
                    var _tampTab = GetTableByRName(resultData.Table, mberExp.Member.Name);
                    if (_tampTab == null)
                    {
                        _tampTab = new AnalysisTable()
                        {
                            RName = mberExp.Member.Name,
                            Name = mberExp.Type.Name,
                            TableType = mberExp.Type
                        };
                        AnalysisTables(mberExp.Expression, _tampTab);
                    }
                    if (refTable != null)
                    {
                        _tampTab.LeftJoins.Add(refTable);
                    }
                    break;
                case "TypedParameterExpression":
                    //命名参数表达式
                    var texp = exp as ParameterExpression;
                    if (!IsDefaultType(texp.Type))
                    {
                        if (null == resultData.Table)
                        {
                            resultData.Table = new AnalysisTable()
                            {
                                RName = argName,
                                Name = texp.Type.Name,
                                TableType = texp.Type
                            };
                        }
                        if (refTable != null)
                        {
                            resultData.Table.LeftJoins.Add(refTable);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 遍历树，深度优先
        /// </summary>
        /// <param name="table"></param>
        /// <param name="rName"></param>
        /// <returns></returns>
        private AnalysisTable GetTableByRName(AnalysisTable table, string rName)
        {
            var _tempTable = table;
            if (_tempTable.RName == rName)
            {
                return _tempTable;
            }
            foreach (var item in _tempTable.LeftJoins)
            {
                _tempTable = GetTableByRName(item, rName);
                if (_tempTable != null)
                {
                    return _tempTable;
                }
            }
            return null;
        }

        /// <summary>
        /// 解析获取表达式的值
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="leftChild"></param>
        /// <returns></returns>
        private object GetChildValue(Expression exp)
        {
            var className = exp.GetType().Name;
            switch (className)
            {
                case "BinaryExpression":
                case "LogicalBinaryExpression":
                    var lExp = exp as BinaryExpression;
                    var ret = GetChildValue(lExp.Left);
                    if (IsNullDefaultType(ret))
                    {
                        ret = GetChildValue(lExp.Right);
                    }
                    return ret;
                case "MethodBinaryExpression":
                    var mbExp = exp as BinaryExpression;
                    var ret1 = GetChildValue(mbExp.Left);
                    if (IsNullDefaultType(ret1))
                    {
                        ret1 = GetChildValue(mbExp.Right);
                    }
                    return ret1;

                case "PropertyExpression":
                case "FieldExpression":
                    var mberExp = exp as MemberExpression;
                    return GetChildValue(mberExp.Expression);
                case "ConstantExpression":
                    var cExp = exp as ConstantExpression;
                    return cExp.Value;
                case "UnaryExpression":
                    var unaryExp = exp as UnaryExpression;
                    return GetChildValue(unaryExp.Operand);
                case "InstanceMethodCallExpressionN":
                    var imExp = exp as MethodCallExpression;
                    if (imExp.Arguments.Count > 0)
                    {
                        return GetChildValue(imExp.Arguments[0]);
                    }
                    return null;
                default:
                    return null;
            }

        }

        /// <summary>
        /// 初始化所有参数
        /// </summary>
        /// <param name="paramObj"></param>
        private void AppendParams(object paramObj, Dictionary<string, object> _params)
        {
            if (IsNullDefaultType(paramObj))
            {
                return;
            }
            if (_params == null)
            {
                _params = new Dictionary<string, object>();
            }
            foreach (var item in paramObj.GetType().GetProperties())
            {
                if (IsDefaultType(item.PropertyType))
                {
                    var value = item.GetValue(paramObj, null);
                    if (value != null)
                    {
                        _params.Add(string.Format("@{0}", item.Name), value);
                    }
                    continue;
                }

                AppendParams(item.GetValue(paramObj), _params);
            }

            foreach (var item in paramObj.GetType().GetFields())
            {
                if (IsDefaultType(item.FieldType))
                {
                    var value = item.GetValue(paramObj);
                    if (value != null)
                    {
                        _params.Add(string.Format("@{0}", item.Name), value);
                    }
                    continue;
                }
                AppendParams(item.GetValue(paramObj), _params);
            }
        }

        public Dictionary<string, object> GetParams(object paramObj)
        {
            Dictionary<string, object> dicParams = new Dictionary<string, object>();
            AppendParams(paramObj, dicParams);
            return dicParams;
        }

        /// <summary>
        /// 判断是否是系统默认基本类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsNullDefaultType(object obj)
        {
            if (obj == null)
            {
                return true;
            }
            return IsDefaultType(obj.GetType());
        }

        private bool IsDefaultType(Type type)
        {
            string defaultType = @"String|Boolean|Double|Int32|Int64|Int16|Single|DateTime|Decimal|Char|Object|Guid";

            Regex e = new Regex(defaultType, RegexOptions.IgnoreCase);
            if (type.Name.ToLower().Contains("nullable"))
            {
                if (type.GenericTypeArguments.Count() > 0)
                {
                    return e.IsMatch(type.GenericTypeArguments[0].Name);
                }
            }
            return e.IsMatch(type.Name);
        }

        #endregion
    }
}
