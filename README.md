# WinHttpSharp
The library allows that you can use HttpWebRequest class like WinHttp style using C# and VB.NET.

## Simple Usage
C#:
```csharp
static void main(string[] args)
{
	WinHttpRequest whttp = new WinHttpRequest();

	whttp.Open("GET", "https://www.google.co.kr");
	whttp.Send();

	Console.WriteLine(whttp.ResponseText);
}
```

VB.NET:
```vbnet
Sub Main()
	Dim wHttp As WinHttpRequest = New WinHttpRequest

	wHttp.Open("GET", "https://www.google.co.kr")
	wHttp.Send()

	Console.WriteLine(wHttp.ResponseText)
End Sub
```

## Implemented Functions, Properties, Events
### Functions
| Name                  | Return Type | Implemented |
| :--------------------:|:-----------:|:-----------:|
| Open                  | void        | Yes         |
| Send                  | void        | Yes         |
| SetRequestHeader      | void        | Yes         |
| GetResponseHeader     | string      | Yes         |
| GetResponseHeaders    | string      | Yes         |
| Abort                 | void        | Yes         |
| SetAutoLogonPolicy    | void        | No          |
| SetClientCertificate  | void        | No          |
| SetProxy              | void        | No          |
| SetTimeouts           | void        | No          |
| WaitForResponse       | bool        | No          |

### Properties
| Name                  | Return Type | Implemented |
| :--------------------:|:-----------:|:-----------:|
| Responsebody          | byte[]      | Yes         |
| ResponseStream        | Stream      | Yes         |
| ResponseText          | string      | Yes         |
| Status                | int         | Yes         |
| StatusText            | string      | Yes         |
| Option                | object      | No          |

### Events
| Name                    | Implemented |
| :----------------------:|:-----------:|
| OnError                 | No          |
| OnResponseDataAvailable | No          |
| OnResponseStart         | No          |
| OnResponseFinished      | Yes         |

### Others

- Currently, Async is not supported.
