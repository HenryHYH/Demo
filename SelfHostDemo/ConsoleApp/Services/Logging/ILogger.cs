﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services.Logging
{
    public interface ILogger
    {
        void Debug(string message, Exception ex = null);
    }
}
