using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp.Models
{
    [XmlRoot("Student")]
    public class Student
    {
        [XmlAttribute("StuName")]
        public string Name { get; set; }

        [XmlAttribute("StuAge")]
        public int Age { get; set; }
    }
}
