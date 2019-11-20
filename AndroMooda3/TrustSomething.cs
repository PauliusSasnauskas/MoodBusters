using Javax.Net.Ssl;
using System.Security.Cryptography.X509Certificates;

namespace AndroMooda3
{
    internal class TrustSomething : Java.Lang.Object, ITrustManager
    {
        public void CheckClientTrusted(X509Certificate[] chain, string authType) { }
        public void CheckServerTrusted(X509Certificate[] chain, string authType) { }
        public X509Certificate[] GetAcceptedIssuers()
        {
            return new X509Certificate[0];
        }
    }
}