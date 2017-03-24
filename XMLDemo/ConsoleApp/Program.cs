using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Student() { Name = "A", Age = 1 };
            var b = new Student() { Name = "B", Age = 2 };

            var grade = new Grade() { Name = "Grade1", Students = new Student[] { a, b } };
            var school = new School() { Name = "School1", Grades = new Grade[] { grade } };

            Console.WriteLine(GetXml(school));

            Console.ReadKey();
        }

        private static string GetXml<T>(T obj)
        {
            var result = string.Empty;

            var serializer = new XmlSerializer(typeof(T));
            using (var mem = new MemoryStream())
            using (var writer = new XmlTextWriter(mem, Encoding.UTF8))
            {
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.None;
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(writer, obj, ns);
                mem.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(mem))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }
    }
}
