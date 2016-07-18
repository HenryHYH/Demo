using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Infrastructure
{
    public class QueryCreator : BaseCreator, IQuery
    {
        public virtual void CreateSelect(AnalysisData data)
        {
            var primaryTable = data.Table;
            StringBuilder sb = new StringBuilder();
            //默认查询全部列
            if (data.StackList.Count <= 0)
            {
                sb.AppendFormat("[{0}].*", primaryTable.RName);
            }
            else
            {
                //查询部分列
                for (int i = 0; i < data.StackList.Count; i += 3)
                {
                    sb.AppendFormat("{0} {1} {2},", data.StackList[i], data.StackList[i + 1], data.StackList[i + 2]);
                }
                sb.Remove(sb.Length - 1, 1);
            }
            Debris.Select = sb.ToString();
        }
        public virtual void CreateFrom(AnalysisTable anlyTable, TableInfo tableInfo)
        {
            if (null == anlyTable)
            {
                throw new ArgumentNullException("anlyTable");
            }
            //默认排序信息
            if (string.IsNullOrEmpty(Debris.Order))
            {
                Debris.Order = string.Format("[{0}].{1} {2}", anlyTable.RName, tableInfo.PrimaryKey, System.Enum.GetName(typeof(OrderTypeEnum), OrderTypeEnum.ASC));
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("[{0}] AS [{1}]", anlyTable.Name, anlyTable.RName);
            AppendLeftJoinTables(anlyTable, tableInfo, sb);
            Debris.From = sb.ToString();
        }
        public virtual void CreateWhere(AnalysisData data)
        {
            if (data == null || data.StackList.Count() <= 0)
            {
                return;
            }
            Debris.Where = string.Join(" ", data.StackList);
        }
        public virtual void AppendSkip(int count)
        {
            Debris.Skip = count;
        }
        public virtual void AppendTake(int count)
        {
            Debris.Take = count;
        }
        public virtual void AppendOrder(AnalysisData data, OrderTypeEnum orderType)
        {
            if (data.StackList.Count <= 0)
            {
                return;
            }
            var field = data.StackList.First();
            StringBuilder sb = new StringBuilder(Debris.Order);
            if (string.IsNullOrEmpty(Debris.Order))
            {
                sb.AppendFormat("{0} {1}", field, System.Enum.GetName(typeof(OrderTypeEnum), orderType));
                Debris.Order = sb.ToString();
                return;
            }
            sb.AppendFormat(",{0} {1}", field, System.Enum.GetName(typeof(OrderTypeEnum), orderType));
            Debris.Order = sb.ToString();
        }
        public void GetCount()
        {
            Debris.Select = "COUNT(1)";
        }

        private KeyValuePair<string, TableInfo> GetForeignReference(TableInfo tInfo, string rName)
        {
            var keyValue = tInfo.ForeignRefs.Where(u => u.Value.RName == rName).FirstOrDefault();
            if (string.IsNullOrEmpty(keyValue.Key))
            {
                foreach (var item in tInfo.ForeignRefs)
                {
                    var foreignTable = GetForeignReference(item.Value, rName);
                    if (!string.IsNullOrEmpty(keyValue.Key))
                    {
                        return foreignTable;
                    }
                }

            }

            return keyValue;
        }
        private void AppendLeftJoinTables(AnalysisTable anlyTable, TableInfo tableInfo, StringBuilder sb)
        {
            ///添加关系表信息
            foreach (var item in anlyTable.LeftJoins)
            {
                var _foreignRef = GetForeignReference(tableInfo, item.RName);
                if (string.IsNullOrEmpty(_foreignRef.Key))
                {
                    // throw new BusinessException(BusinessRes.WhitoutThisForeignReference);
                    throw new Exception("foreign key");
                }
                sb.AppendFormat(" LEFT JOIN [{0}] AS [{1}] ON [{1}].{2}=[{3}].{4} ", item.Name, item.RName, _foreignRef.Value.PrimaryKey, anlyTable.RName, _foreignRef.Key);
                AppendLeftJoinTables(item, _foreignRef.Value, sb);
            }
        }
    }

    public interface ICreator
    {
        SqlDebris Debris { get; set; }
    }

    public class BaseCreator : ICreator
    {
        public SqlDebris Debris { get; set; }
        public BaseCreator()
        {
            Debris = new SqlDebris();
        }
    }
    public interface IQuery : ICreator
    {
        void CreateFrom(AnalysisTable data, TableInfo tableInfo);
        void CreateSelect(AnalysisData data);
        void CreateWhere(AnalysisData data);
        void AppendOrder(AnalysisData data, OrderTypeEnum orderType);
        void AppendSkip(int count);
        void AppendTake(int count);
        void GetCount();
    }

    public enum OrderTypeEnum
    {
        ASC,
        DESC
    }

    public class TableInfo
    {
        public string ConStr { get; set; }
        public string RName { get; set; }
        public string TableName { get; set; }
        public string PrimaryKey { get; set; }
        public Dictionary<string, TableInfo> ForeignRefs { get; set; }
    }
}
