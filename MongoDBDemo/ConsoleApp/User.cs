using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ConsoleApp
{
    public class User
    {
        // public ObjectId Id { get; set; }
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
