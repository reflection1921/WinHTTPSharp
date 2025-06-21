using System;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WinHttpSharp Examples");
            Console.WriteLine("1. SimpleGet");
            Console.WriteLine("2. SimpleGetAsync");
            Console.Write("Select Example Number: ");

            string nStr = Console.ReadLine();
            if (!int.TryParse(nStr, out var number))
            {
                return;
            }

            switch (number)
            {
                case 1:
                    SimpleGet._SimpleGet();
                    break;
                case 2:
                    SimpleGet._SimpleGetAsync().GetAwaiter().GetResult();
                    break;
                default:
                    break;
            }


            Console.ReadKey();
        }
    }
}
