using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace UserManagementAPI
{
    /// <summary>
    /// Main class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point for program
        /// </summary>
        /// <param name="args">args</param>
        public static void Main(string[] args)
        {
            // setting console title
            Console.Title = "UserManagementAPI";

            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Builds web host
        /// </summary>
        /// <param name="args">arguments</param>
        /// <returns>webhost</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
