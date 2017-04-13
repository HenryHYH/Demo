using AutoMapper;
using ConsoleApp.Test2;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test1();
            Test2();

            Console.ReadKey();
        }

        private static void Test2()
        {
            var config = new MapperConfiguration(conf =>
            {
                conf.CreateMap<A, B>()
                    .ForMember(d => d.DT, o => o.UseValue(DateTime.Now))
                    .ForMember(d => d.Ext1, o => o.MapFrom(s => s.ExtField.Str1))
                    .ForMember(d => d.Ext2, o => o.MapFrom(s => s.ExtField.Str2))
                    .ForMember(d => d.ExtA, o => o.MapFrom(s => s.ExtField.Ext2Field.StrA));
                conf.CreateMap<List<A>, List<B>>();
            });
            var mapper = config.CreateMapper();

            var a = new A();
            var b = mapper.Map<B>(a);
            Console.WriteLine(null == b.Ext1);
            Console.WriteLine("[{0}]", b.Ext1);
            Console.WriteLine(null == b.Ext2);
            Console.WriteLine("[{0}]", b.Ext2);
            Console.WriteLine(null == b.ExtA);
            Console.WriteLine("[{0}]", b.ExtA);

            Console.WriteLine();
            a.ExtField = new Ext
            {
                Str1 = "Str1"
            };
            b = mapper.Map<B>(a);
            Console.WriteLine(null == b.Ext1);
            Console.WriteLine("[{0}]", b.Ext1);
            Console.WriteLine(null == b.Ext2);
            Console.WriteLine("[{0}]", b.Ext2);
            Console.WriteLine(null == b.ExtA);
            Console.WriteLine("[{0}]", b.ExtA);

            Console.WriteLine();
            a.ExtField = new Ext
            {
                Ext2Field = new Ext2
                {
                    StrA = "StrA"
                }
            };
            b = mapper.Map<B>(a);
            Console.WriteLine(null == b.Ext1);
            Console.WriteLine("[{0}]", b.Ext1);
            Console.WriteLine(null == b.Ext2);
            Console.WriteLine("[{0}]", b.Ext2);
            Console.WriteLine(null == b.ExtA);
            Console.WriteLine("[{0}]", b.ExtA);

            Console.WriteLine("{0:yyyyMMdd HH:mm:ss.ffff}", b.DT);

            Console.WriteLine();
            IList<A> aList = new List<A>();
            aList.Add(new A
            {
                ExtField = new Ext
                {
                    Str1 = "A1"
                }
            });
            aList.Add(new A
            {
                ExtField = new Ext
                {
                    Str1 = "A2"
                }
            });
            var bList = mapper.Map<IList<B>>(aList);
            Console.WriteLine(null == bList);
            Console.WriteLine(bList.Count);
            foreach (var item in bList)
                Console.WriteLine(item.Ext1);
        }

        private static void Test1()
        {
            var config = new MapperConfiguration(conf =>
            {
                conf.CreateMap<Request, DTO>();
                conf.CreateMap<Extend, DTO>();
            });

            var req = new Request();
            // var dto = new DTO();
            var ext = new Extend();

            var mapper = config.CreateMapper();
            DTO dto = mapper.Map<Request, DTO>(req);
            dto = mapper.Map(ext, dto);

            Console.WriteLine("A = " + dto.A);
            Console.WriteLine("B2 = " + dto.B2);
            Console.WriteLine("C = " + dto.C);
        }
    }
}
