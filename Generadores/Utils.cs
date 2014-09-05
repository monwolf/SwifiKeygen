using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Generadores
{
    class Utils
    {

        public   const string HEX = "0123456789ABCDEF";
        public const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string charTable = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string MIN_ALPHABET = "abcdefghijklmnopqrstuvwxyz";

        private bool is_Hex(char c)
        {

            bool X = false;
            foreach (char CC in HEX)
                if (CC == c) X = true;
            return X;
        }
        public bool is_hex(string str)
        {
            int j; bool ret = true;
            for (j = 0; j < str.Length & ret == true; j++)
            {
                if (str[j] != ':') { ret = ret & is_Hex(str[j]); }
            }
            return ret;
        }

      

        /*****************************************
         * Funciones propias para los generadores*
         * **************************************/

      
        public static bool comparaMAC(string mac1, string mac2)
        {
            int j; bool x = true;
            int length = Math.Min(mac1.Length, mac2.Length);
            for (j = 0; j < length; j++)
            {
                if (mac1[j] != mac2[j]) x = false;
            }
            return x;
        }

        private string dec2hex(int numero)
        {
            string temp = "", ret = "";
            // int i = 0;
            while (numero / 16 > 0)
            {
                temp += HEX[numero % 16];
                numero = numero / 16;
                //  i++;
            }
            temp += HEX[numero % 16];
            //hacemos un flip
            for (int i = temp.Length - 1; i >= 0; i--)
            {
                ret += temp[i];
            }
            //
            return ret;
        }




        private int hex2dex(char c) //transformamos caracteres en hex a su valor entero 
        {  //suponemos que no nos llegara ningun error
            int i;

            for (i = 0; i < HEX.Length & c != HEX[i]; i++) ;
            return i;
        }
        /* FUNCIONES PRIVADAS MAGICWLANDECRYPTER */
        private string change1(string str, string str2)
        {
            int i;
            string str3 = str2[0].ToString();
            for (i = 1; i < 8; i++)
            {
                str3 = str3 + str2[i].ToString();
            }
            return str3 + str;
        }

  

        /*-------------------------------------
      * --------------Calcula md5-----------
      * ------------------------------------
      */
        public string md5(string Value)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(Value);
            data = x.ComputeHash(data);

            string ret = "";
            for (int i = 0; i < data.Length; i++)
                ret += data[i].ToString("x2").ToLower();

            return ret;
        }

        public string md5(Char[] Value)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(Value);
            data = x.ComputeHash(data);
            string ret = "";
            for (int i = 0; i < data.Length; i++)
                ret += data[i].ToString("x2").ToLower();

            return ret;
        }



        /* -----------------------------
         * Calcula el SHA1 de un string
         * -----------------------------
         */

        public string CalculateSHA1(string text, Encoding enc)
        {

            SHA1CryptoServiceProvider cryptoTransformSHA1 =
            new SHA1CryptoServiceProvider();
            return BitConverter.ToString(cryptoTransformSHA1.ComputeHash(enc.GetBytes(text))).Replace("-", "");

        }



        /* ----------------------------------------
         * Primer digito hexadecimal de un caracter
         * ----------------------------------------
         */

        private char hexmsb(char x)
        {

            return HEX[((x & 0xf0) >> 4)];
        }
        /* --------------------------
        * Segundo digito hexadecimal de un caracter
        * --------------------------
        */
        private char hexlsb(char x)
        {

            return (HEX[(x & 0x0f)]);
        }

     
        




    }
}
