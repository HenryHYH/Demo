﻿using Model;
using System;

namespace RealProxyDemo
{
    public interface IUserService
    {
        void Register(User user);
    }

    public class UserService : MarshalByRefObject, IUserService
    {
        public void Register(User user)
        {
            Console.WriteLine("Register. {0}", user.ToString());
        }
    }
}
