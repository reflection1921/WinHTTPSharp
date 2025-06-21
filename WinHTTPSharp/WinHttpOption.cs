using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinHTTPSharp
{

    interface IOption_Impl
    {
        public int this[int x] { get; set; }
    }
    public class Option_Impl : IOption_Impl
    {
        public int this[int x]
        {
            get { return x; }
            set { x = x; }
        }
    }
}
