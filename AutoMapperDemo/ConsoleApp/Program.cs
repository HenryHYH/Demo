using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Config();

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

            Console.ReadKey();
        }

        private static void Config()
        {
        }
    }
}
