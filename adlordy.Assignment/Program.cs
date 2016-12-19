﻿using Microsoft.Owin.Hosting;
using System;

namespace adlordy.Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = args.Length == 1 ? args[0] : "http://*:8081";
            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("Listening at " + baseAddress);
                Console.ReadLine();
            }
        }
    }
}
