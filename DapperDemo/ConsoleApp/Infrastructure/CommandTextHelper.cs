using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Infrastructure
{
    public class CommandTextHelper
    {
        public static string GetSelectCommandText(ICreator creator)
        {
            var debris = creator.Debris;
            StringBuilder sb = new StringBuilder();
            if (debris.Take == null)
            {
                sb.Append(" SELECT * FROM (");
            }
            else
            {
                //分页
                sb.AppendFormat(" SELECT TOP {0} * FROM (", debris.Take);
            }
            sb.AppendFormat("SELECT {0},ROW_NUMBER() OVER(ORDER BY  {2} ) as ROWNUMBER  FROM {1}", debris.Select, debris.From, debris.Order);
            //条件
            if (!string.IsNullOrEmpty(debris.Where))
            {
                sb.AppendFormat(" WHERE {0} ", debris.Where);
            }
            sb.Append(") as NEW_TABLE ");
            sb.AppendFormat(" WHERE ROWNUMBER>{0}", debris.Skip);
            return sb.ToString();
        }

        public static string GetCountCommandText(ICreator creator)
        {
            var debris = creator.Debris;
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("SELECT {0} FROM {1}", debris.Select, debris.From);
            //条件
            if (!string.IsNullOrEmpty(debris.Where))
            {
                sb.AppendFormat(" WHERE {0} ", debris.Where);
            }
            return sb.ToString();
        }

        public static string GetUpdateCommandText(ICreator creator)
        {
            var debris = creator.Debris;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE {0} ", debris.Update);
            sb.AppendFormat("SET {0} ", debris.Set);
            sb.AppendFormat("WHERE {0}", debris.Where);
            return sb.ToString();
        }
        public static string GetInsertCommandText(ICreator creator)
        {
            var debris = creator.Debris;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO {0}", debris.InsertTable);
            sb.AppendFormat("({0}) ", debris.Columns);
            sb.AppendFormat("VALUES({0})", debris.Values);
            return sb.ToString();
        }
        public static string GetDeleteCommandText(ICreator creator)
        {
            var debris = creator.Debris;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM {0}", debris.From);
            sb.AppendFormat(" WHERE {0}", debris.Where);
            return sb.ToString();
        }
    }
}
