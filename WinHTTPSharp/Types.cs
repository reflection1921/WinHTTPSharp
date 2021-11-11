using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinHTTPSharp
{
    public enum WinHttpRequestAutoLogonPolicy { AutoLogonPolicy_Always, AutoLogonPolicy_Never, AutoLogonPolicy_OnlyIfBypassProxy }
    public enum VARIANT { VARIANT_FALSE = 0, VARIANT_TRUE = 1}
    public enum WinHttpRequestOption
    {
        WinHttpRequestOption_UserAgentString = 0,
        WinHttpRequestOption_URL = 1,
        WinHttpRequestOption_URLCodePage = 2,
        WinHttpRequestOption_EscapePercentInURL = 3,
        WinHttpRequestOption_SslErrorIgnoreFlags = 4,
        WinHttpRequestOption_SelectCertificate = 5,
        WinHttpRequestOption_EnableRedirects = 6,
        WinHttpRequestOption_UrlEscapeDisable = 7,
        WinHttpRequestOption_UrlEscapeDisableQuery = 8,
        WinHttpRequestOption_SecureProtocols = 9,
        WinHttpRequestOption_EnableTracing = 10,
        WinHttpRequestOption_RevertImpersonationOverSsl = 11,
        WinHttpRequestOption_EnableHttpsToHttpRedirects = 12,
        WinHttpRequestOption_EnablePassportAuthentication = 13,
        WinHttpRequestOption_MaxAutomaticRedirects = 14,
        WinHttpRequestOption_MaxResponseHeaderSize = 15,
        WinHttpRequestOption_MaxResponseDrainSize = 16,
        WinHttpRequestOption_EnableHttp1_1 = 17,
        WinHttpRequestOption_EnableCertificateRevocationCheck = 18,
        WinHttpRequestOption_RejectUserpwd = 19
    }
    class Option
    {
        object[] _Option = new object[20];
        public object this[int i]
        {
            get { return _Option[i]; }
            set { _Option[i] = value; }
        }
    }
}
