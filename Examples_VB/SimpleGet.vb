Imports WinHTTPSharp.WinHttp
Module SimpleGet
    Public Sub _simpleGet()
        Dim wHttp As WinHttpRequest = New WinHttpRequest

        wHttp.Open("GET", "https://www.google.co.kr")
        wHttp.Send()

        Console.WriteLine(wHttp.ResponseText)
    End Sub
End Module
