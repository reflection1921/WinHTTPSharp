using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinHTTPSharp
{
    public interface IWinHttpRequest
    {
        byte[] ResponseBody { get; }
        Stream ResponseStream { get; }
        public string ResponseText { get; }
        int Status { get; }
        string StatusText { get; }

        //Functions
        void Open(string method, string url, bool async = false);
        Task Send(byte[] sendData = null);
        Task Send(string sendData);
        void SetRequestHeader(string header, string value);
        void Abort();
        string GetResponseHeaders();
        string GetResponseHeader(string header);


        //void SetAutoLogonPolicy(WinHttpRequestAutoLogonPolicy AutoLogonPolicy);
        //void SetClientCertificate(string ClientCertificate);
        //void SetProxy(int ProxySetting, [object ProxyServer], [object BypassList]);
        //void SetTimeouts(int ResolveTimeout, int ConnectionTimeout, int SendTimeout, int ReceiveTimeout);
        //bool WaitForResponse([object Timeout]);
        //change option
        //object Option[int i] { get; set{ Option a = new Option(); } }
    }
}
