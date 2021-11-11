using System;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WinHttpSharp Examples");
            Console.WriteLine("1. SimpleGet\n");
            Console.Write("Select Example Number: ");

            string nStr = Console.ReadLine();
            int number = 0;
            if (!int.TryParse(nStr, out number))
            {
                return;
            }

            switch (number)
            {
                case 1:
                    SimpleGet._SimpleGet();
                    break;
                default:
                    break;
            }


            Console.ReadKey();
        }
    }
}
