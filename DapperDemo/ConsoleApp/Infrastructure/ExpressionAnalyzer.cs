using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp.Infrastructure
{
    public class ExpressionAnalyzer
    {
        #region Fields

        /// <summary>
        /// 表达式所有参数集合
        /// </summary>
        private Dictionary<string, object> _params;
        /// <summary>
        /// 命名参数别名
        /// </summary>
        private const string _argName = "TAB";
        /// <summary>
        /// 解析结果
        /// </summary>
        private AnalysisData _resultData;

        #endregion

        #region Ctor

        public ExpressionAnalyzer()
        {
            _resultData = new AnalysisData();
            _params = new Dictionary<string, object>();
        }

        public ExpressionAnalyzer(LambdaExpression exp, AnalysisTable table = null)
            : this()
        {
            if (table != null)
            {
                _resultData.Table = table;
            }

            if (exp != null)
            {
                AppendParams(GetChildValue(exp.Body), _params);
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
            return _resultData;
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
                    _resultData.StackList.Add("(");
                    AnalysisExpression(GetChildExpression(exp));
                    _resultData.StackList.Add(")");
                    _resultData.StackList.Add("AND");
                    _resultData.StackList.Add("(");
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    _resultData.StackList.Add(")");
                    break;
                case ExpressionType.OrElse:
                    _resultData.StackList.Add("(");
                    AnalysisExpression(GetChildExpression(exp));
                    _resultData.StackList.Add(")");
                    _resultData.StackList.Add("OR");
                    _resultData.StackList.Add("(");
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    _resultData.StackList.Add(")");
                    break;
                case ExpressionType.Equal:
                    AnalysisExpression(GetChildExpression(exp));
                    _resultData.StackList.Add("=");
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    break;
                case ExpressionType.NotEqual:
                    AnalysisExpression(GetChildExpression(exp));
                    _resultData.StackList.Add("!=");
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    AnalysisExpression(GetChildExpression(exp));
                    _resultData.StackList.Add(">=");
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    break;
                case ExpressionType.GreaterThan:
                    AnalysisExpression(GetChildExpression(exp));
                    _resultData.StackList.Add(">");
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    break;
                case ExpressionType.LessThan:
                    AnalysisExpression(GetChildExpression(exp));
                    _resultData.StackList.Add("<");
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    break;
                case ExpressionType.LessThanOrEqual:
                    AnalysisExpression(GetChildExpression(exp));
                    _resultData.StackList.Add("<=");
                    AnalysisExpression(GetChildExpression(exp, false), false);
                    break;
                case ExpressionType.Call:
                    var imExp = exp as MethodCallExpression;
                    AnalysisExpression(imExp.Object, true);
                    _resultData.StackList.Add("LIKE");
                    if (imExp.Arguments.Count > 0)
                    {
                        var arg0 = imExp.Arguments[0] as MemberExpression;
                        _resultData.StackList.Add("'%'+");
                        AnalysisExpression(imExp.Arguments[0], false);
                        _resultData.StackList.Add("+'%'");
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
                            _resultData.StackList.Add(string.Format("[{0}].{1}", parentName, GetExpressionName(exp)));
                            break;
                        }
                        _resultData.StackList.Add(GetExpressionName(exp));
                    }
                    else
                    {
                        var paramName = GetParamName(exp);
                        _resultData.ParamList.Add(paramName, _params[paramName]);
                        _resultData.StackList.Add(paramName);
                    }
                    break;
                case ExpressionType.Constant:
                    var constent = exp as ConstantExpression;
                    if (constent.Value == null)
                    {
                        var op = _resultData.StackList.ElementAt(_resultData.StackList.Count - 1);
                        _resultData.StackList.RemoveAt(_resultData.StackList.Count - 1);
                        if (string.Equals(op, "="))
                        {
                            _resultData.StackList.Add("IS NULL");
                        }
                        else
                        {
                            _resultData.StackList.Add("IS NOT NULL");
                        }
                        break;
                    }
                    if (constent.Value.GetType() == typeof(String))
                    {
                        _resultData.StackList.Add(string.Format("'{0}'", constent.Value));
                        break;
                    }
                    if (constent.Value.GetType() == typeof(bool))
                    {
                        if (_resultData.StackList.Count > 0)
                        {
                            var value = Convert.ToBoolean(constent.Value);
                            _resultData.StackList.Add(string.Format("{0}", value ? "1" : "0"));
                        }

                        break;
                    }
                    _resultData.StackList.Add(string.Format("{0}", constent.Value));
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
                        _resultData.StackList.Add("AS");
                        _resultData.StackList.Add(string.Format("'{0}'", newExp.Members[i].Name));
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
                    return _argName;
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
                    var _tampTab = GetTableByRName(_resultData.Table, mberExp.Member.Name);
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
                        if (null == _resultData.Table)
                        {
                            _resultData.Table = new AnalysisTable()
                            {
                                RName = _argName,
                                Name = texp.Type.Name,
                                TableType = texp.Type
                            };
                        }
                        if (refTable != null)
                        {
                            _resultData.Table.LeftJoins.Add(refTable);
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

    public class AnalysisData
    {
        public AnalysisData()
        {
            StackList = new List<string>();
            ParamList = new Dictionary<string, object>();
        }

        public AnalysisTable Table { get; set; }

        public IList<string> StackList { get; set; }

        public IDictionary<string, object> ParamList { get; set; }
    }

    public class AnalysisTable
    {
        public AnalysisTable()
        {
            LeftJoins = new List<AnalysisTable>();
        }

        public string RName { get; set; }

        public string Name { get; set; }

        public Type TableType { get; set; }

        public IList<AnalysisTable> LeftJoins { get; set; }
    }
}
