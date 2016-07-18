using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Infrastructure
{
    public class CommandFactory<TEntity>
    {
        protected TableInfo _tableInfo;
        public CommandFactory()
        {
            // _tableInfo = new Mapper().MapToTable(typeof(TEntity));
            _tableInfo = new TableInfo() { PrimaryKey = "Id" };
        }
    }

    public class QueryCommand<TEntity> : CommandFactory<TEntity>
    {
        //sql碎片生成器
        private IQuery _creator;
        //查询参数
        private IDictionary<string, object> _params;

        private AnalysisTable _table;

        public QueryCommand()
        {
            _creator = new QueryCreator();
        }
        public QueryCommand<TEntity> Where(Expression<Func<TEntity, bool>> exp)
        {
            var retData = new ExpressionAnalyzer(exp).GetAnalysisResult();
            _creator.CreateWhere(retData);
            _params = retData.ParamList;
            _table = retData.Table;
            return this;
        }
        public QueryCommand<TEntity> OrderBy(Expression<Func<TEntity, object>> exp)
        {
            _creator.AppendOrder(new ExpressionAnalyzer(exp).GetAnalysisResult(), OrderTypeEnum.ASC);
            return this;
        }
        public QueryCommand<TEntity> OrderByDescding(Expression<Func<TEntity, object>> exp)
        {
            _creator.AppendOrder(new ExpressionAnalyzer(exp).GetAnalysisResult(), OrderTypeEnum.DESC);
            return this;
        }
        public QueryCommand<TEntity> ThenBy(Expression<Func<TEntity, object>> exp)
        {
            return OrderBy(exp);
        }
        public QueryCommand<TEntity> ThenByDescding(Expression<Func<TEntity, object>> exp)
        {
            return OrderByDescding(exp);
        }
        public QueryCommand<TEntity> Skip(int count)
        {
            _creator.AppendSkip(count);
            return this;
        }
        public QueryCommand<TEntity> Take(int count)
        {
            _creator.AppendTake(count);
            return this;
        }
        public Command<TResult> GetSelectCommand<TResult>(Expression<Func<TEntity, TResult>> exp)
        {
            var _result = new ExpressionAnalyzer(exp, _table).GetAnalysisResult();
            _creator.CreateSelect(_result);
            _creator.CreateFrom(_result.Table, _tableInfo);
            return new Command<TResult>()
            {
                ConStr = _tableInfo.ConStr,
                CommandText = CommandTextHelper.GetSelectCommandText(_creator),
                Params = _params
            };
        }
        public Command<int> GetCountCommand(Expression<Func<TEntity, bool>> exp)
        {
            var retData = new ExpressionAnalyzer(exp).GetAnalysisResult();
            _creator.CreateWhere(retData);
            _creator.GetCount();
            _creator.CreateFrom(retData.Table, _tableInfo);
            return new Command<int>()
            {
                CommandText = CommandTextHelper.GetCountCommandText(_creator),
                ConStr = _tableInfo.ConStr,
                Params = retData.ParamList
            };
        }
    }

    public class Command<T>
    {
        public string CommandText { get; set; }

        public string ConStr { get; set; }

        public IDictionary<string, object> Params { get; set; }
    }
}
