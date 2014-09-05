using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Generadores.clsGeneradores
{
    class Ono4XX:IGeneradores
    {
        public Ono4XX(GenOption _opcions)
            : base(_opcions)
        {
            filtro.supportedMAC = new string[] { "ONOXXXX" };
            filtro.supportedSsid = new string[] { "00:01:38", "E0:91:53" };
        }


        public override bool wlanMatch(string essid, string mac)
        {

            bool validMAC = false;
            foreach (string BSSID in filtro.supportedMAC) {
                validMAC = Utils.comparaMAC(mac, BSSID);
                if (validMAC) break;
            
            }


            if (validMAC && opciones.ESSID.Length == 7 && opciones.ESSID.ToUpper().Contains("ONO"))
            {
                return true;
            }
            else return false;
        }
       
        internal override void generaDicc()
        {
            string str = string.Empty;

            //creo el archivo/o lo abro en modo escritura que Me pasan por parametro:
            using (StreamWriter writer = File.CreateText(opciones.Filename))
            {

                int ebp5c, ebp54, ebp8c, ebp88, ebp84, ebp80, ebp1c, ebp18, ebp14;
                int ebp10, ebp08 = 0, ebp04 = 0, ebpd0, ebpd4, ebpcc, ebpc8, ebpc4, ebpc0, ebpbc, ebpb8, ebpb4;
                int a, b, c, d, e, aux, edx, wpa1, wpa2;

                if (opciones.TipoRed == TipoRed.WPA)
                {
                    ebp8c = opciones.ESSID[3] - 48;		//ONOXxxx
                    ebp88 = opciones.ESSID[4] - 48;		//ONOxXxx
                    ebp84 = opciones.ESSID[5] - 48;		//ONOxxXx
                    ebp80 = opciones.ESSID[6] - 48;		//ONOxxxX
                }
                else
                {
                    ebp8c = 9 - (opciones.ESSID[3] - 48);		//ONOXxxx
                    ebp88 = 9 - (opciones.ESSID[4] - 48);		//ONOxXxx
                    ebp84 = 9 - (opciones.ESSID[5] - 48);		//ONOxxXx
                    ebp80 = 9 - (opciones.ESSID[6] - 48);		//ONOxxxX
                }

                ebp5c = (opciones.ESSID[9]) - 48;			//xx:xx:xx:Xx:xx:xx

                if (ebp5c >= 17) ebp5c = ebp5c - 16;	//Pasa A->1, B->2, C->3,..F->6

                ebp54 = (opciones.ESSID[10]) - 48;			//xx:xx:xx:xX:xx:xx

                if (ebp54 >= 17) ebp54 = 4;		//Si bssid[10] entre A..F
                else ebp54 = 3;			//Si bssid[10] entre 0..9

                if (ebp80 == 0) ebp1c = 1;
                else ebp1c = 2;

                if (ebp80 == 0) ebp18 = ebp80 + 9;	//rotacion 0,9,8...1,0,9..0
                else ebp18 = ebp80 - 1;

                ebp14 = ((ebp84 * 10 + ebp88) + 6) / 10;
                if (ebp14 == 10) ebp14 = 0;

                if (ebp88 > 3) ebp10 = ebp88 - 4;
                else ebp10 = ebp88 + 6;

                if (ebp54 == 3) ebp08 = 0;
                if (ebp54 == 4 && ebp5c == 0) ebp08 = 0;
                if (ebp54 == 4 && ebp5c != 0) ebp08 = 1;


                if (ebp5c == 0) ebp04 = 9;
                if (ebp5c != 0) ebp04 = ebp5c - 1;



                MaxProgres = (int)Math.Pow(10, 5);
                float percent, performed = 0;

                //-------------------------------------------------------------------
                for (a = 0; a <= 9; ++a)
                {
                    for (b = 0; b <= 1; ++b)
                    {
                        for (c = 0; c <= 9; ++c)
                        {
                            for (d = 0; d <= 1; ++d)
                            {
                                for (e = 0; e <= 9; ++e)
                                {

                                    if (a + 8 > 9) ebpd4 = a - 2;
                                    else ebpd4 = a + 8;

                                    ebpd0 = (b * 100 + a * 10 + ebp8c + 180) / 100;

                                    if (c + ebp04 > 9) ebpcc = c + ebp04 - 10;		//REPITE mas abajo para ebpbc
                                    else ebpcc = c + ebp04;

                                    ebpbc = ebpcc;					//Es igual, calcula lo mismo

                                    //if (c+ebp04>9)	ebpbc = c+ebp04-10;	// REPITE lo mismo que para ebpcc
                                    //	else	ebpbc = c+ebp04;

                                    ebpc8 = (d * 10 + c + ebp08 * 10 + ebp04) / 10;

                                    if (e * 2 > 9) ebpc4 = (e - 5) * 2;
                                    else ebpc4 = e * 2;

                                    if (d + 1 > 9) ebpc0 = d - 9;			//Nunca se cumple que d+1 sea mayor que 9
                                    else ebpc0 = d + 1;			//siempre se usa esto ebpc0 = d+1;


                                    aux = b * 10 + c + ebp04;

                                    if (aux == -10) ebpb8 = 0;
                                    else ebpb8 = (aux + 10) / 10;

                                    if (a + 9 > 9) ebpb4 = a - 1;
                                    else ebpb4 = a + 9;

                                    edx = (ebp8c * 10 + a + 9) / 10;	//edx
                                    if (edx == 10) edx = 0;


                                    if (opciones.TipoRed == TipoRed.WPA)
                                    {	//------------------------------------------- wpa ------------------------------------------
                                        wpa1 = ebp80 * 100 + ebp84 * 10 + ebp88 + 103;

                                        if (wpa1 > 1000) wpa1 = wpa1 - 897;

                                        wpa2 = (ebp8c * 100 + a * 10 + b + 81) / 10;
                                        str = wpa1.ToString() + wpa2.ToString() + ebpb8.ToString() + ebpbc.ToString() + ebpc0.ToString() + ebpc4.ToString() + ebpc8.ToString() + ebpcc.ToString() + ebpd0.ToString() + ebpd4.ToString() + ebp8c.ToString();
                                        writer.WriteLine(str);

                                        if (ebpc0 == 2)
                                        {
                                            str = wpa1.ToString() + wpa2.ToString() + ebpb8.ToString() + ebpbc.ToString() + (ebpc0 - 1).ToString() + ebpc4.ToString() + ebpc8.ToString() + ebpcc.ToString() + ebpd0.ToString() + ebpd4.ToString() + ebp8c.ToString();
                                            writer.WriteLine(str);
                                        }
                                        if (ebpc0 == 1)
                                        {
                                            str = wpa1.ToString() + wpa2.ToString() + ebpb8.ToString() + ebpbc.ToString() + (ebpc0 + 1).ToString() + ebpc4.ToString() + ebpc8.ToString() + ebpcc.ToString() + ebpd0.ToString() + ebpd4.ToString() + ebp8c.ToString();
                                            writer.WriteLine(str);
                                        }
                                    }	//------------------------------------------- wpa ------------------------------------------
                                    else
                                    {	//------------------------------------------- wep ------------------------------------------
                                        str = ebp80.ToString() + ebp84.ToString() + ebp88.ToString() + ebp8c.ToString() + a.ToString() + b.ToString() + c.ToString() + d.ToString() + e.ToString() + ebp08.ToString() + ebp04.ToString() + ebp1c.ToString() + ebp18.ToString() + ebp14.ToString() + ebp10.ToString() + edx.ToString() + ebpb4.ToString() + ebpb8.ToString() + ebpbc.ToString() + ebpc0.ToString() + ebpc4.ToString() + ebpc8.ToString() + ebpcc.ToString() + ebpd0.ToString() + ebpd4.ToString() + ebp8c.ToString();
                                        writer.WriteLine(str);

                                        if (ebpc0 == 2)
                                        {
                                            str = ebp80.ToString() + ebp84.ToString() + ebp88.ToString() + ebp8c.ToString() + a.ToString() + b.ToString() + c.ToString() + d.ToString() + e.ToString() + ebp08.ToString() + ebp04.ToString() + ebp1c.ToString() + ebp18.ToString() + ebp14.ToString() + ebp10.ToString() + edx.ToString() + ebpb4.ToString() + ebpb8.ToString() + ebpbc.ToString() + (ebpc0 - 1).ToString() + ebpc4.ToString() + ebpc8.ToString() + ebpcc.ToString() + ebpd0.ToString() + ebpd4.ToString() + ebp8c.ToString();
                                            writer.WriteLine(str);
                                        }
                                        if (ebpc0 == 1)
                                        {
                                            str = ebp80.ToString() + ebp84.ToString() + ebp88.ToString() + ebp8c.ToString() + a.ToString() + b.ToString() + c.ToString() + d.ToString() + e.ToString() + ebp08.ToString() + ebp04.ToString() + ebp1c.ToString() + ebp18.ToString() + ebp14.ToString() + ebp10.ToString() + edx.ToString() + ebpb4.ToString() + ebpb8.ToString() + ebpbc.ToString() + (ebpc0 + 1).ToString() + ebpc4.ToString() + ebpc8.ToString() + ebpcc.ToString() + ebpd0.ToString() + ebpd4.ToString() + ebp8c.ToString();
                                            writer.WriteLine(str);
                                        }	//------------------------------------------- wep ------------------------------------------

                                        performed++;
                                        percent = performed / MaxProgres * 100;

                                        ReportProgress((int)percent);
                                    }
                                }//for
                            }
                        }
                    }
                }//for
            }
        }
    }
}
