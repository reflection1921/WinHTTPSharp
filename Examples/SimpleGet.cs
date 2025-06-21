using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinHTTPSharp;
using WinHTTPSharp.WinHttp;

namespace Examples
{
    public class SimpleGet
    {
        public static void _SimpleGet()
        {
            IWinHttpRequest whttp = new WinHttpRequest();

            whttp.Open("GET", "https://www.google.co.kr");
            whttp.Send();

            Console.WriteLine(whttp.ResponseText);
        }

        public static async Task _SimpleGetAsync()
        {
            IWinHttpRequest whttp = new WinHttpRequest();
            whttp.Open("GET", "https://www.google.co.kr", true);
            await whttp.Send();

            Console.WriteLine(whttp.ResponseText);
        }
    }
}
