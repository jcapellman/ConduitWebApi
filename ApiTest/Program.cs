using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CONDUIT.PCL.Handlers;

namespace ApiTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            var test = new Test();
            while (true)
            {
                Console.WriteLine("Hit any key to run the test");
                var input = Console.ReadLine();
                Console.WriteLine("Running Test...");
                var result = test.TryTest();
                Console.WriteLine($"Test returned: {result.Result}");

            }
        }
    }

    public class Test
    {
        public async Task<bool> TryTest()
        {
            var baseHandler = new Base("http://localhost:57871/api/");
            var result = await baseHandler.Post<string, bool>($"Login?", "username=shawn&password=Guyver25");

            return result;
        }
    }
}
