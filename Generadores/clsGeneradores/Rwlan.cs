using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Generadores.clsGeneradores
{
   public sealed class Rwlan:IGeneradores
    {

        public Rwlan(GenOption _opcion):base(_opcion)
        {
            filtro.supportedSsid = new string[] { "Rwlan_XX" };
            filtro.supportedMAC = new string[] {  };
        }

        internal override void generaDicc()
        {

            string partefija = "0000000";
            // int i, j, k, l, m, n;
            using (StreamWriter writer = new StreamWriter(opciones.Filename))
            {
                int j;
                if (base.opciones.Padding != string.Empty)
                {
                    j = partefija.Length;
                    partefija = partefija.PadLeft(j, opciones.Padding[0]);
                }
                MaxProgres = (int)Math.Pow(10, 6);
                float percent, performed = 0;

                char[] clave = "0123456789".ToCharArray();
                char[] str = ("000000" + partefija).ToCharArray();
                foreach (char a in clave)
                {
                    foreach (char b in clave)
                    {
                        foreach (char c in clave)
                        {
                            foreach (char d in clave)
                            {
                                foreach (char e in clave)
                                {
                                    foreach (char f in clave)
                                    {
                                        str[0] = a;
                                        str[1] = b;
                                        str[2] = c;
                                        str[3] = d;
                                        str[4] = e;
                                        str[5] = f;
                                        writer.WriteLine(str);
                                        performed++;
                                        percent = performed / MaxProgres * 100;
                                        this.ReportProgress((int)percent);

                                    }
                                }
                            }
                        }
                    }
                }

            }

        }

        

        public override bool wlanMatch(string essid, string mac)
        {
            if (essid.ToLower().Contains("rwlan")) return true;
            else
                return  false;



        }

    }
}
