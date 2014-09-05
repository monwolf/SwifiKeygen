using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Generadores.clsGeneradores
{
   public sealed class Verizon:IGeneradores
    {

       public Verizon(GenOption _opcions):base(_opcions) {
           filtro.supportedMAC = new string[] { };
           filtro.supportedSsid = new string[] { };
       }
        private int valor36(char c)
        {

            int i;
            for (i = 0; i < Utils.charTable.Length; i++)
            {

                if (Utils.charTable[i] == c) break;
            }
            return i;
        }

       internal override void generaDicc()
        {
            int j; double k = 0;
            string digitoessid = opciones.ESSID.Substring(opciones.ESSID.Length - 5);
            using (StreamWriter writer = new StreamWriter(opciones.Filename))
            {
                for (j = 0; j < digitoessid.Length; j++)
                {
                    k += valor36(digitoessid[j]) * (Math.Pow(36, j));
                }
                // writer.WriteLine(k.ToString());
                writer.WriteLine("1801" + ((int)k).ToString("X"));
                writer.WriteLine("1F90" + ((int)k).ToString("X"));

            }
        }
       public override bool wlanMatch(string essid, string mac) {
           return true;
       }


    }
}
