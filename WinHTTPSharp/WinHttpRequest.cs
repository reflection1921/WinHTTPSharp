using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinHTTPSharp
{
    namespace WinHttp
    {

        //async not supported.
        public class WinHttpRequest : IWinHttpRequest, IWinHttpRequestEvents
        {

            HttpWebRequest webReq;
            HttpWebResponse webRes;
            bool isAsync = false;

            public event IWinHttpRequestEvents_OnResponseFinishedEventHandler OnResponseFinished;
            public event IWinHttpRequestEvents_OnErrorEventHandler OnError;

            public string ResponseText { get; private set; }
            public byte[] ResponseBody { get; private set; }
            public Stream ResponseStream { get; private set; }
            public int Status { get; private set; }
            public string StatusText { get; private set; }

            public void Open(string Method, string Url, VARIANT Async = VARIANT.VARIANT_FALSE)
            {
                webReq = (HttpWebRequest)WebRequest.Create(Url);
                webReq.Method = Method;
                if (Async == VARIANT.VARIANT_TRUE)
                {
                    isAsync = true;
                }
                else
                {
                    isAsync = false;
                }
            }

            public void Send(byte[] sendData = null)
            {
                //if (isAsync)
                //{
                //    await Send_Impl_Async(sendData);
                //    await GetResponseData_Impl_Async();
                //}

                //else
                {
                    if (webReq.Method.ToLower() != "get")
                    {
                        Send_Impl(sendData);
                    }
                    GetResponseData_Impl();
                }
                
            }

            public void Send(string sendData)
            {
                //if (isAsync)
                //{
                //    await Send_Impl_Async(sendData);
                //    await GetResponseData_Impl_Async();
                //}

                //else
                {
                    if (webReq.Method.ToLower() != "get")
                    {
                        Send_Impl(sendData);
                    }
                    
                    GetResponseData_Impl();
                }
            }

            public void SetRequestHeader(string Header, string Value)
            {
                string _header = Header.ToLower();
                switch(_header)
                {
                    case "accept":
                        webReq.Accept = Value;
                        break;
                    case "user-agent":
                        webReq.UserAgent = Value;
                        break;
                    case "content-length":
                        long long_val = 0;
                        bool valid = long.TryParse(Value, out long_val);
                        if (valid) webReq.ContentLength = long_val;
                        break;
                    case "connection":
                        webReq.Connection = Value;
                        if (!Value.ToLower().Equals("keep-alive")) webReq.KeepAlive = false;
                        break;
                    case "content-type":
                        webReq.ContentType = Value;
                        break;
                    case "host":
                        webReq.Host = Value;
                        break;
                    case "referer":
                        webReq.Referer = Value;
                        break;
                    default:
                        webReq.Headers[Header] = Value;
                        break;
                }
            }

            public string GetResponseHeaders()
            {
                int count = webRes.Headers.Count;

                string headers = "";

                for (int i = 0; i < count; i++)
                {
                    string key = webRes.Headers.GetKey(i);
                    string[] vals = webRes.Headers.GetValues(i);
                    string val = "";

                    for (int j = 0; j < vals.Length; j++)
                    {
                        val += vals[j] + (j >= vals.Length - 1 ? "" : " ");
                    }
                    headers = headers + key + ": " + val + (i >= count - 1 ? "" : "\n");
                }

                return headers;
            }

            public string GetResponseHeader(string Header)
            {
                return webRes.GetResponseHeader(Header);
            }

            public void Abort()
            {
                webReq.Abort();
            }

            //public void SetTimeouts(int ResolveTimeout, int ConnectionTimeout, int SendTimeout, int ReceiveTimeout)
            //{
            //    
            //}

            public dynamic Option()
            {
                object[] krv = new object[4];
                krv[0] = 1;
                krv[1] = "KRV";
                return false;
            }

            /*private*/
            private void Send_Impl(byte[] sendData = null)
            {
                using (Stream stDataParams = webReq.GetRequestStream())
                {
                    stDataParams.Write(sendData, 0, sendData.Length);
                }
            }

            private void Send_Impl(string sendData)
            {

                using (StreamWriter stDataParams = new StreamWriter(webReq.GetRequestStream()))
                {
                    stDataParams.Write(sendData);
                }
            }

            private async Task Send_Impl_Async(byte[] sendData = null)
            {
                using (Stream stDataParams = await webReq.GetRequestStreamAsync())
                {
                    await stDataParams.WriteAsync(sendData, 0, sendData.Length);
                }
            }

            private async Task Send_Impl_Async(string sendData)
            {
                using (StreamWriter stDataParams = new StreamWriter(await webReq.GetRequestStreamAsync()))
                {
                    await stDataParams.WriteAsync(sendData);
                }
            }

            private void GetResponseData_Impl()
            {
                webRes = (HttpWebResponse)webReq.GetResponse();

                using (MemoryStream ms = new MemoryStream())
                {
                    webRes.GetResponseStream().CopyTo(ms);
                    ResponseBody = ms.ToArray();
                    ResponseText = Encoding.UTF8.GetString(ResponseBody);
                    ResponseStream = ms;
                    if (OnResponseFinished != null) OnResponseFinished();
                }

                Status = ((int)webRes.StatusCode);
                StatusText = webRes.StatusCode.ToString();

            }

            private async Task GetResponseData_Impl_Async()
            {
                webRes = (HttpWebResponse)await webReq.GetResponseAsync();

                using (MemoryStream ms = new MemoryStream())
                {
                    webRes.GetResponseStream().CopyTo(ms);
                    ResponseBody = ms.ToArray();
                    ResponseText = Encoding.UTF8.GetString(ResponseBody);
                    ResponseStream = ms;
                    OnResponseFinished();
                }

                Status = ((int)webRes.StatusCode);
                StatusText = webRes.StatusCode.ToString();
            }
        }
    }
}
