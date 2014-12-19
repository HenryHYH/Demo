using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Notice
    {
        public Notice()
        {
            CreateTime = DateTime.Now;
        }

        public Notice(string message)
            : this()
        {
            Message = message;
        }

        public string Message { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
