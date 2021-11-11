using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinHTTPSharp.WinHttp;

namespace Examples
{
    public class SimpleGet
    {
        public static void _SimpleGet()
        {
            WinHttpRequest whttp = new WinHttpRequest();

            whttp.Open("GET", "https://www.google.co.kr");
            whttp.Send();

            Console.WriteLine(whttp.ResponseText);
        }
    }
}
