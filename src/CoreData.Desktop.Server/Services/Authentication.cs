using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server
{
    // https://tools.ietf.org/html/draft-ietf-oauth-native-apps-12#appendix-B.3
    // https://tools.ietf.org/html/rfc8252
    // https://github.com/googlesamples/oauth-apps-for-windows/blob/master/OAuthDesktopApp/OAuthDesktopApp/MainWindow.xaml.cs
    // https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/WebAuthenticationBroker
    // https://docs.microsoft.com/en-us/windows/uwp/security/web-authentication-broker#connecting-with-single-sign-on-sso
    // https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/external-authentication-services#OBTAIN

    // https://en.wikipedia.org/wiki/Integrated_Windows_Authentication
    // https://en.wikipedia.org/wiki/Single_sign-on
    // https://en.wikipedia.org/wiki/Active_Directory_Federation_Services

    //todo: use of CefSharp.OffScreen to auth with data from desktop.
    //          https://github.com/cefsharp/CefSharp/blob/master/CefSharp.OffScreen.Example/Program.cs
    //          https://github.com/cefsharp/CefSharp/blob/master/CefSharp.Example/CefExample.cs
    //          https://www.codeproject.com/Articles/881315/Display-HTML-in-WPF-and-CefSharp-Tutorial-Part
    //          https://www.codeproject.com/Articles/887148/Display-HTML-in-WPF-and-CefSharp-Tutorial-Part

    //public class BasicAuthenticator : Authenticator
    //{
    //}

    //public class SsoAuthenticator : Authenticator
    //{
    //}
}
// HttpClient sample https://github.com/dotnet/samples/blob/master/csharp/getting-started/console-webapiclient/Program.cs

// CefSharp in WPF http://windowsapptutorials.com/wpf/using-chromium-web-browser-control-wpf-app/

//    $FormLogin = New-UDAuthenticationMethod -Endpoint {
//    param([PSCredential]$Credentials)
//​
//    if ($Credentials.UserName -eq "Adam" -and $Credentials.GetNetworkCredential().Password -eq "SuperSecretPassword") {
//        New-UDAuthenticationResult -Success -UserName "Adam"
//    }
//​
//    New-UDAuthenticationResult -ErrorMessage "You aren't Adam!!!"
