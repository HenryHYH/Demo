using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp.Models
{
    [XmlRoot("SchoolA")]
    public class School
    {
        [XmlAttribute("SchoolName")]
        public string Name { get; set; }

        // [XmlElement("Grades")]
        public Grade[] Grades { get; set; }
    }
}
