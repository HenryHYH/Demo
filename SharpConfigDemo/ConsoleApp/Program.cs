using SharpConfig;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = Configuration.LoadFromFile("sample.txt");
            var section = config["General"];

            string someString = section["SomeString"].StringValue;
            string someString2 = section["SomeString"].GetValue<string>();
            int someInteger = section["SomeInteger"].IntValue;
            float someFloat = section["SomeFloat"].FloatValue;
            bool someBool = section["SomeBoolean"].BoolValue;
            int[] someIntArray = section["SomeArray"].IntValueArray;
            string[] someStringArray = section["SomeArray"].StringValueArray;
            DayOfWeek day = section["Day"].GetValue<DayOfWeek>();

            var person = config["Person"].ToObject<Person>();

            var nul = config["Null"]["Null"].StringValue;
            //var nul2 = config["Null"]["Null"].IntValue;

            Console.ReadKey();
        }
    }
}
