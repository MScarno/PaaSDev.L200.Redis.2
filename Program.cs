using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace PaaSDevL200Redis2
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["RedisConnectionString"].ConnectionString;
            int valueSize = 1000000;
            var rand = new Random();

            byte[] file = new byte[valueSize];
            rand.NextBytes(file);

            // Set Client config 
            ConfigurationOptions options = ConfigurationOptions.Parse(connectionString);
            options.SyncTimeout = 1000000;
            try
            {
                ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(options);
                IDatabase db = connection.GetDatabase();

                for (int i = 0; i < 50; i++)
                {
                    db.StringSet(i.ToString(), value: file);
                    Console.WriteLine("Key {0}: Added", i.ToString());
                }
                Console.WriteLine("- All 50 Keys successfully added -\nPress a button to exit");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("- 1 Exception occurred -\n");
                // Handle the  exception.
                
                Console.WriteLine(e.Message+"\n");
                Console.WriteLine("\nPress a button to exit");
                Console.ReadLine();
            }
        }
    }
}
