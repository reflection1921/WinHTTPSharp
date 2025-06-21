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
            
            HttpWebRequest _webReq;
            HttpWebResponse _webRes;
            private bool _isAsync;
            private string _url;
            
            public event IWinHttpRequestEvents_OnResponseFinishedEventHandler OnResponseFinished;
            public event IWinHttpRequestEvents_OnErrorEventHandler OnError;

            public string ResponseText { get; private set; }
            public byte[] ResponseBody { get; private set; }
            public Stream ResponseStream { get; private set; }
            public int Status { get; private set; }
            public string StatusText { get; private set; }

            public void Open(string method, string url, bool async = false)
            {
                _webReq = (HttpWebRequest)WebRequest.Create(url);
                _webReq.Method = method;
                _isAsync = async;
                _url = url;
            }

            public Task Send(byte[] sendData = null)
            {
                if (_isAsync)
                {
                    return Send_Async(sendData);
                }
                else
                {
                    Send_Sync(sendData);
                    return Task.CompletedTask;
                }
            }

            public Task Send(string sendData)
            {
                if (_isAsync)
                {
                    return Send_Async(sendData);
                }
                else
                {
                    Send_Sync(sendData);
                    return Task.CompletedTask;
                }
            }

            public void SetRequestHeader(string header, string value)
            {
                header = header.ToLower();
                switch(header)
                {
                    case "accept":
                        _webReq.Accept = value;
                        break;
                    case "user-agent":
                        _webReq.UserAgent = value;
                        break;
                    case "content-length":
                        bool valid = long.TryParse(value, out var longValue);
                        if (valid) _webReq.ContentLength = longValue;
                        break;
                    case "connection":
                        _webReq.Connection = value;
                        if (!value.ToLower().Equals("keep-alive")) _webReq.KeepAlive = false;
                        break;
                    case "content-type":
                        _webReq.ContentType = value;
                        break;
                    case "host":
                        _webReq.Host = value;
                        break;
                    case "referer":
                        _webReq.Referer = value;
                        break;
                    case "cookie":
                        _webReq.Headers["Cookie"] = value;
                        break;
                    default:
                        _webReq.Headers[header] = value;
                        break;
                }
            }

            public string GetResponseHeaders()
            {
                int count = _webRes.Headers.Count;

                string headers = "";

                for (int i = 0; i < count; i++)
                {
                    string key = _webRes.Headers.GetKey(i);
                    string[] vals = _webRes.Headers.GetValues(i);
                    string val = "";

                    for (int j = 0; j < vals.Length; j++)
                    {
                        val += vals[j] + (j >= vals.Length - 1 ? "" : " ");
                    }
                    headers = headers + key + ": " + val + (i >= count - 1 ? "" : "\n");
                }

                return headers;
            }

            public string GetResponseHeader(string header)
            {
                return _webRes.GetResponseHeader(header);
            }

            public void Abort()
            {
                _webReq.Abort();
            }

            //public void SetTimeouts(int ResolveTimeout, int ConnectionTimeout, int SendTimeout, int ReceiveTimeout)
            //{
            //    
            //}

            #region Private Methods
            private void Send_Sync(byte[] sendData = null)
            {
                if (sendData != null)
                {
                    using Stream stDataParams = _webReq.GetRequestStream();
                    stDataParams.Write(sendData, 0, sendData.Length);
                }

                GetResponseData_Sync();
            }

            private void Send_Sync(string sendData)
            {
                using StreamWriter stDataParams = new StreamWriter(_webReq.GetRequestStream());
                stDataParams.Write(sendData);

                GetResponseData_Sync();
            }

            private async Task Send_Async(byte[] sendData = null)
            {
                if (sendData != null)
                {
                    await using Stream stDataParams = await _webReq.GetRequestStreamAsync();
                    await stDataParams.WriteAsync(sendData, 0, sendData.Length);
                }

                await GetResponseData_Async();

            }

            private async Task Send_Async(string sendData)
            {
                await using StreamWriter stDataParams = new StreamWriter(await _webReq.GetRequestStreamAsync());
                await stDataParams.WriteAsync(sendData);

                await GetResponseData_Async();
            }

            private void GetResponseData_Sync()
            {
                _webRes = (HttpWebResponse)_webReq.GetResponse();

                using (MemoryStream ms = new MemoryStream())
                {
                    _webRes.GetResponseStream().CopyTo(ms);
                    ResponseBody = ms.ToArray();
                    ResponseText = Encoding.UTF8.GetString(ResponseBody);
                    ResponseStream = ms;
                    if (OnResponseFinished != null) OnResponseFinished();
                }

                Status = ((int)_webRes.StatusCode);
                StatusText = _webRes.StatusCode.ToString();
            }

            private async Task GetResponseData_Async()
            {
                _webRes = (HttpWebResponse)await _webReq.GetResponseAsync();

                using (MemoryStream ms = new MemoryStream())
                {
                    _webRes.GetResponseStream().CopyTo(ms);
                    ResponseBody = ms.ToArray();
                    ResponseText = Encoding.UTF8.GetString(ResponseBody);
                    ResponseStream = ms;
                    if (OnResponseFinished != null) OnResponseFinished();
                }

                Status = ((int)_webRes.StatusCode);
                StatusText = _webRes.StatusCode.ToString();
            }
            #endregion
        }
    }
}
