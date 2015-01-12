﻿namespace FW.Core.Domain.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class User : BaseEntity
    {
        #region Properties

        public string Password
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Age { get; set; }

        #endregion Properties
    }
}