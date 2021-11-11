using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinHTTPSharp
{
    public delegate void IWinHttpRequestEvents_OnErrorEventHandler(int ErrorNumber, string ErrorDescription);
    public delegate void IWinHttpRequestEvents_OnResponseStartEventHandler(int Status, string ContentType);
    public delegate void IWinHttpRequestEvents_OnResponseFinishedEventHandler();
    public delegate void IWinHttpRequestEvents_OnResponseDataAvailableEventHandler(byte[] data);

    interface IWinHttpRequestEvents
    {
        //event IWinHttpRequestEvents_OnErrorEventHandler OnError;
        //void OnResponseDataAvailable(ref Array data);
        event IWinHttpRequestEvents_OnResponseFinishedEventHandler OnResponseFinished;
        //void OnResponseStart(int Status, string ContentType);
    }
}
