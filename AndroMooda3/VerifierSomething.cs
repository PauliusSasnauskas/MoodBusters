using System;
using Javax.Net.Ssl;

namespace AndroMooda3
{
    internal class VerifierSomething : Java.Lang.Object, IHostnameVerifier
    {

        public bool Verify(string hostname, ISSLSession session)
        {
            return true;
        }
    }
}