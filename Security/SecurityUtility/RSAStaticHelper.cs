using System;
using System.Collections.Generic;
using System.Text;

namespace SercurityUtility
{
    public static class RSAStaticHelper
    {
        const string _publicKey = "<RSAKeyValue><Modulus>qWRrYsiAA69Ka8TU9ljjF+CAhgI9mDIu5DeV7fNOlgO8lXps9X1DL3bnFRh9xN41</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        const string _privateKey = "<RSAKeyValue><Modulus>qWRrYsiAA69Ka8TU9ljjF+CAhgI9mDIu5DeV7fNOlgO8lXps9X1DL3bnFRh9xN41</Modulus><Exponent>AQAB</Exponent><P>17DU2dA2+nZUkN3Q0E+MiggKTl9Y7+p7</P><Q>yQyOZbfwzXDtYle/RU5nAW/CPrquPRMP</Q><DP>hXwjjpVkoQcAeRM9+t1wXbByKGSr1vbn</DP><DQ>MPE/kP+QPVLqVvBpfaOya6UbMk4bsH59</DQ><InverseQ>1oyFM7Y1mPnFlwFHbwJQn13eyK6Rj8rP</InverseQ><D>WAN5WIZdHY0C2iQXkzh0Jn/bx8V6Q44uvpTLqJy8OPPLNG0bZqcdNlTicC7jZ96x</D></RSAKeyValue>";

        public static string Encrypt(this string plainText)
        {
            var rsa = new RSAHelper();
            return rsa.Encrypt(_publicKey, plainText, false);
        }
        public static string Decrypt(this string encryptText)
        {
            var rsa = new RSAHelper();
            return rsa.Decrypt(_privateKey, encryptText, false);
        }
    }
}
