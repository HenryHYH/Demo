using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp.Models
{
    [XmlRoot("GradeAA")]
    public class Grade
    {
        [XmlAttribute("GradeName")]
        public string Name { get; set; }

        // [XmlElement("Students2")]
        public Student[] Students { get; set; }
    }
}
