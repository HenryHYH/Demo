using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public double Price { get; set; }

        public DateTime PublishDay { get; set; }

        public bool Enabled { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Id = {0}", Id);
            sb.AppendLine();
            sb.AppendFormat("Name = {0}", Name);
            sb.AppendLine();
            sb.AppendFormat("Author = {0}", Author);
            sb.AppendLine();
            sb.AppendFormat("Price = {0}", Price);
            sb.AppendLine();
            sb.AppendFormat("PublisDay = {0}", PublishDay.ToShortDateString());
            sb.AppendLine();
            sb.AppendFormat("Enabled = {0}", Enabled);
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
