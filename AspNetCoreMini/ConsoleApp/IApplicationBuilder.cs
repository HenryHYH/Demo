using System;

namespace ConsoleApp
{
    public interface IApplicationBuilder
    {
        IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware);

        RequestDelegate Build();
    }
}
