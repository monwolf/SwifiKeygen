using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Collections.Generic;
namespace Proyecto_Windows
{
    class Generadores
    {

        BackgroundWorker bw;
        swifi_keygen.boxProgreso progresso = new swifi_keygen.boxProgreso();
        bool WorkerFirstTime = false;
        bool ERROR = false;
        int MaxProgres = 0;
        private string ESSID, MAC;
        private opcio opc;
        private bool claves, anio;
        private bool BelkinAllKeys;

        /*
         * Declaraciones de tipos y variables globales
         */
        #region Constantes  y enumeraciones
        private const string HEX = "0123456789ABCDEF";
        private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string charTable = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string MIN_ALPHABET = "abcdefghijklmnopqrstuvwxyz";
        private const string charsTPlinkPureNetwork = "2345678923456789ABCDEFGHJKLMNPQRSTUVWXYZ";

        readonly static char[] alphabetlower ={'0','1','2','3','4','5','6','7','8','9','a','b','c','d','e','f','g','h','i',
                                  'j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
        public enum opcio { WEP, WPA };
        public enum generadores
        {
            wlandecrypter,
            jazzteldecrypter,
            thompsonxxxxxx,
            rwlandecrypter,
            dlinkdecrypter,
            Jmagicwlandecrypter,
            magicwlandecrypter,
            ono,
            discus,
            speedtouchxxxxxx,
            verizon,
            mac2wepkey,
            tele2dic,
            skyv1,
            wlan4xx,
            wifixxxxxxx,
            yacomxxxxxx,
            orangexxxxxx,
            OrangeXXXX,
            Linksys_DLINK_PureGenkeys2,
            TPLINK_PureGenKeys2,
            EasyBox_es,
            EasyBox_de,
            Belkin4xx,
            WiFiArnetXXXX,
            other,
            none

        }
        public generadores gen_selected { get; set; }
        public string archivo;
        #endregion 
        #region Constructores
        public Generadores(string archivo)
        {
            this.archivo = archivo;
            this.gen_selected = generadores.none;
            bw = new BackgroundWorker();
            progresso = new swifi_keygen.boxProgreso();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;

        }
        public Generadores(string archivo, generadores gen)
        {
            this.archivo = archivo;
            this.gen_selected = gen;
            this.gen_selected = generadores.none;
            progresso = new swifi_keygen.boxProgreso();
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;

        }
        public Generadores()
        {
            progresso = new swifi_keygen.boxProgreso();
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;

        }
        #endregion

        public void Set_File(string file)
        {
#pragma warning disable
            this.archivo = file;
            using (StreamWriter abc = new StreamWriter(file)) ;
#pragma warning restore

        }


        #region funciones auxiliares
       
        public bool is_hex(string str)
        {
            int j; bool ret = true;
            for (j = 0; j < str.Length & ret == true; j++)
            {
                if (str[j] != ':') { ret = ret & HEX.Contains(str[j].ToString()); }
            }
            return ret;
        }

        /*
         * Funcion compara MAC
         */
        public bool comparaMAC(string mac1, string mac2)
        {
            int j; bool x = true;
            int length = Math.Min(mac1.Length, mac2.Length);
            for (j = 0; j < length; j++)
            {
                if (mac1[j] != mac2[j]) x = false;
            }
            return x;
        }


        /*****************************************
         * Funciones propias para los generadores*
         * **************************************/

        //FUNCION USADA EN VERIZON
        private int valor36(char c)
        {

            int i;
            for (i = 0; i < charTable.Length; i++)
            {

                if (charTable[i] == c) break;
            }
            return i;
        }


        private string dec2hex(int numero)
        {
            string temp = string.Empty, ret = string.Empty;
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


        private int getDecimalValue(string hex) {
            return int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        
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
        /** Transformacion de String a byte array **/
        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
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
        #endregion


        #region funciones criptograficas
        /*-------------------------------------
      * --------------Calcula md5-----------
      * ------------------------------------
      */
        public string md5(string Value)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(Value);
            data = x.ComputeHash(data);

            string ret = string.Empty;
            for (int i = 0; i < data.Length; i++)
                ret += data[i].ToString("x2").ToLower();

            return ret;
        }

        public string md5(Char[] Value)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(Value);
            data = x.ComputeHash(data);
            string ret = string.Empty;
            for (int i = 0; i < data.Length; i++)
                ret += data[i].ToString("x2").ToLower();

            return ret;
        }
        /* -----------------------------
         * Calcula el SHA256 de una cadena de bytes
         * -----------------------------
         */

        public static string getHashSha256(byte[] bytes, out byte[] hash)
        {
            SHA256Managed hashstring = new SHA256Managed();
            hash = hashstring.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            foreach (byte x in hash)
            {
                sb.AppendFormat("{0:x2}", x);
            }

            return sb.ToString();
        }


        /* -----------------------------
         * Calcula el SHA1 de un string
         * -----------------------------
         */

        public string CalculateSHA1(string text, Encoding enc)
        {

            SHA1CryptoServiceProvider cryptoTransformSHA1 =
            new SHA1CryptoServiceProvider();
            return BitConverter.ToString(cryptoTransformSHA1.ComputeHash(enc.GetBytes(text))).Replace("-", string.Empty);

        }
        #endregion
        

        /* ----------------------------------------------------------------------
         * A partir de aquí declaramos las funciones de los distintos generadores
         * ----------------------------------------------------------------------
         */
        #region Rwlan
        /*------------------------------------------------------------------------
         *----------------------- RWlan Decrypter de Uxío-------------------------
         *------------------------------------------------------------------------*/
        public void Rwlan(string valor)
        {
            string partefija = "0000000";
            // int i, j, k, l, m, n;
            using (StreamWriter writer = new StreamWriter(archivo))
            {
                int j;
                if (valor != string.Empty)
                {
                    j = partefija.Length;
                    partefija = partefija.PadLeft(j, valor[0]);
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
                                        bw.ReportProgress((int)percent);

                                    }
                                }
                            }
                        }
                    }
                }

            }
        }

        #endregion

        #region Verizon
        /*--------------------------------------------------------------------
         * --------------------Verizon Keygen---------------------------------
         * -------------------------------------------------------------------*/
     
        public void Verizon(string digitoessid)
        {
            int j; double k = 0;
            using (StreamWriter writer = new StreamWriter(archivo))
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
        #endregion
        #region Belkin4xx


        private readonly static short[] ORDER_0 = { 6, 2, 3, 8, 5, 1, 7, 4 };
        private readonly static short[] ORDER_1 = { 1, 2, 3, 8, 5, 1, 7, 4 };
        private readonly static short[] ORDER_2 = { 1, 2, 3, 8, 5, 6, 7, 4 };
        private readonly static short[] ORDER_3 = { 6, 2, 3, 8, 5, 6, 7, 4 };

        private readonly static char[] CHARSET = "024613578ACE9BDF".ToCharArray();
        private readonly static char[] charset = "944626378ace9bdf".ToCharArray();


        /*
         * 
         *     targets     = ['94:44:52','08:86:3B','EC:1A:59']
         *     essids      = ['Belkin.XXXX','Belkin_XXXXXX','belkin.xxxx','belkin.xxx']    
         */
        public void Belkin4xx(string MACStr, string essid, bool AllKeys)
        {
            List<string> _clave = new List<string>();

            String mac = MACStr.Replace(":", "");

            if (!AllKeys)
            {

                if (essid.StartsWith("B"))
                {
                    string wmac = mac.Substring(4);
                    AddKey(ref _clave, generateKey(wmac, CHARSET, ORDER_0));
                }
                else
                {
                    mac = addOneToMac(MACStr.Replace(":", ""));
                    string wmac = mac.Substring(4);
                    if (mac.StartsWith("944452"))
                    {

                        AddKey(ref _clave, generateKey(wmac, charset, ORDER_0));

                    }
                    else
                    {
                        AddKey(ref _clave, generateKey(wmac, charset, ORDER_0));
                        AddKey(ref _clave, generateKey(wmac, charset, ORDER_2));
                        mac = addOneToMac(mac);
                        wmac = mac.Substring(4);
                        AddKey(ref _clave, generateKey(wmac, charset, ORDER_0));


                    }

                }
            }
            else
            {
                // Bruteforce         

                List<short[]> orders = new List<short[]>() { ORDER_0, ORDER_1, ORDER_2, ORDER_3 };
                List<char[]> charsets = new List<char[]>() { CHARSET, charset };
                for (int i = 0; i < 3; i++)
                {
                    string wmac = mac.Substring(4);
                    foreach (char[] chars in charsets)
                    {
                        foreach (short[] order in orders)
                        {
                            AddKey(ref _clave,generateKey(wmac, chars, order));

                        }

                    }
                    mac = addOneToMac(mac);
                }

            }
            using (StreamWriter writer = new StreamWriter(archivo))
            {
                foreach (string key in _clave)
                {
                    writer.WriteLine(key);

                }
            }

        }

        private string addOneToMac(string lmac)
        {

            Int64 imac = Int64.Parse(lmac, System.Globalization.NumberStyles.HexNumber);
            imac++;
            return imac.ToString("X12");



        }
        private void AddKey( ref List<string> _clave ,string k)
        {
            if (!_clave.Contains(k)) { _clave.Add(k); }
        }

        private static string generateKey(string wmac, char[] chars, short[] order)
        {

            char[] k = new char[wmac.Length];
            string KEY = string.Empty;
            for (int i = 0; i < wmac.Length; i++)
            {
                k[i] = wmac[order[i] - 1];
            }
            for (int i = 0; i < wmac.Length; i++)
            {
                KEY += chars[int.Parse(k[i].ToString(), System.Globalization.NumberStyles.HexNumber)];
            }

            return KEY;

        }
        #endregion

        #region Ono4XX
        /*----------------------------------Ono4XX-----------------------------*/
        //--------------------------------------------------------------------------
        // Funcion: generaClaves
        // Genera las posibles claves para redes onoxyxy
        //--------------------------------------------------------------------------
        public void generaONO(string essid, string bssid, Generadores.opcio opcion)
        {
            string str = string.Empty;



            //creo el archivo/o lo abro en modo escritura que Me pasan por parametro:
            using (StreamWriter writer = File.CreateText(archivo))
            {

                int ebp5c, ebp54, ebp8c, ebp88, ebp84, ebp80, ebp1c, ebp18, ebp14;
                int ebp10, ebp08 = 0, ebp04 = 0, ebpd0, ebpd4, ebpcc, ebpc8, ebpc4, ebpc0, ebpbc, ebpb8, ebpb4;
                int a, b, c, d, e, aux, edx, wpa1, wpa2;

                if (opcion == opcio.WPA)
                {
                    ebp8c = essid[3] - 48;		//ONOXxxx
                    ebp88 = essid[4] - 48;		//ONOxXxx
                    ebp84 = essid[5] - 48;		//ONOxxXx
                    ebp80 = essid[6] - 48;		//ONOxxxX
                }
                else
                {
                    ebp8c = 9 - (essid[3] - 48);		//ONOXxxx
                    ebp88 = 9 - (essid[4] - 48);		//ONOxXxx
                    ebp84 = 9 - (essid[5] - 48);		//ONOxxXx
                    ebp80 = 9 - (essid[6] - 48);		//ONOxxxX
                }

                ebp5c = (bssid[9]) - 48;			//xx:xx:xx:Xx:xx:xx

                if (ebp5c >= 17) ebp5c = ebp5c - 16;	//Pasa A->1, B->2, C->3,..F->6

                ebp54 = (bssid[10]) - 48;			//xx:xx:xx:xX:xx:xx

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


                                    if (opcion == opcio.WPA)
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

                                        bw.ReportProgress((int)percent);
                                    }
                                }//for
                            }
                        }
                    }
                }//for
            }
        }
        #endregion

        #region WLAN4XX
        //------------------------------------------------------------------------------------
        // Genera las posibles claves de una WLANXXXXXX,WifiXXXXXX,YaComXXXXXXX
        // Algoritmo por Mambostar
        //-------------------------------------------------------------------------------------



        public void wlan4xx(string essid, string bssid) //tiene que entrar la mac y el nombre de lared entero
        {   //string str=string.Empty;
            int i;
            int X1, X2, X3, Y1, Y2, Y3, Z1, Z2, Z3, W1, W2, W3, W4, K1, K2, S7;
            char[] essid1, bssid1;
            essid1 = new char[essid.Length];
            bssid1 = new char[bssid.Length];
            using (StreamWriter writer = File.CreateText(archivo))
            {

                for (i = 0; i < essid.Length; i++)
                {
                    if (essid[i] >= 65) essid1[i] = (char)(essid[i] - 55); else essid1[i] = essid[i];

                }

                for (i = 0; i < bssid.Length; i++)
                {
                    if (bssid[i] >= 65) bssid1[i] = (char)(bssid[i] - 55); else bssid1[i] = bssid[i];
                }


                i = 0;
                // if (essid.Length == 10) i = 1;
                if (essid.Length == 11) i = 1;
                //wlanxxxxxx
                byte S8 = (byte)(essid1[7 + i] & 15), S9 = (byte)(essid1[8 + i] & 15), S10 = (byte)(essid1[9 + i] & 15), M9 = (byte)essid1[5 + i], M10 = (byte)(essid1[6 + i] & 15);
                byte M11 = (byte)bssid1[15], M12 = (byte)bssid1[16];
                MaxProgres = 10;
                for (S7 = 0; S7 <= 9; S7++)
                {
                    K1 = (S7 + S8 + M11 + M12);
                    K2 = (M9 + M10 + S9 + S10);
                    X1 = K1 ^ S10;
                    X2 = K1 ^ S9;
                    X3 = K1 ^ S8;
                    Y1 = K2 ^ M10;
                    Y2 = K2 ^ M11;
                    Y3 = K2 ^ M12;
                    Z1 = M11 ^ S10;
                    Z2 = M12 ^ S9;
                    Z3 = K1 ^ K2;
                    W1 = X1 ^ Z2;
                    W2 = Y2 ^ Y3;
                    W3 = Y1 ^ X3;
                    W4 = Z3 ^ X2;

                    writer.WriteLine(HEX[(W4 & 15)].ToString() + HEX[(X1 & 15)].ToString() + HEX[(Y1 & 15)].ToString() + HEX[(Z1 & 15)].ToString() + HEX[(W1 & 15)].ToString() + HEX[(X2 & 15)].ToString() + HEX[(Y2 & 15)].ToString() + HEX[(Z2 & 15)].ToString() + HEX[(W2 & 15)].ToString() + HEX[(X3 & 15)].ToString() + HEX[(Y3 & 15)].ToString() + HEX[(Z3 & 15)].ToString() + HEX[(W3 & 15)].ToString());
                    bw.ReportProgress(S7 * 10);
                }
            }
        }
        #endregion

        #region mac2wepkey
        /*----------------------------------------------------------------------------
         * ------------------------Mac2Wepkey-----------------------------------------
         * --------------------------------------------------------------------------*/

        public void mac2wepkey(string BSSID)
        {
            using (StreamWriter writer = new StreamWriter(archivo))
            {
                int i, s1, s2, s3, s4, ya, yb, yc, yd, ye;

                int[] mac, key, a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15, n16, n17, n18, n19, n20, n21, n22, n23, n24, n25, n26, n27, n28, n29, n30, n31, n32, n33;

                a0 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                a1 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                a2 = new int[] { 0, 13, 10, 7, 5, 8, 15, 2, 10, 7, 0, 13, 15, 2, 5, 8 };
                a3 = new int[] { 0, 1, 3, 2, 7, 6, 4, 5, 15, 14, 12, 13, 8, 9, 11, 10 };
                a4 = new int[] { 0, 5, 11, 14, 7, 2, 12, 9, 15, 10, 4, 1, 8, 13, 3, 6 };
                a5 = new int[] { 0, 4, 8, 12, 0, 4, 8, 12, 0, 4, 8, 12, 0, 4, 8, 12 };
                a6 = new int[] { 0, 1, 3, 2, 6, 7, 5, 4, 12, 13, 15, 14, 10, 11, 9, 8 };
                a7 = new int[] { 0, 8, 0, 8, 1, 9, 1, 9, 2, 10, 2, 10, 3, 11, 3, 11 };
                a8 = new int[] { 0, 5, 11, 14, 6, 3, 13, 8, 12, 9, 7, 2, 10, 15, 1, 4 };
                a9 = new int[] { 0, 9, 2, 11, 5, 12, 7, 14, 10, 3, 8, 1, 15, 6, 13, 4 };
                a10 = new int[] { 0, 14, 13, 3, 11, 5, 6, 8, 6, 8, 11, 5, 13, 3, 0, 14 };
                a11 = new int[] { 0, 12, 8, 4, 1, 13, 9, 5, 2, 14, 10, 6, 3, 15, 11, 7 };
                a12 = new int[] { 0, 4, 9, 13, 2, 6, 11, 15, 4, 0, 13, 9, 6, 2, 15, 11 };
                a13 = new int[] { 0, 8, 1, 9, 3, 11, 2, 10, 6, 14, 7, 15, 5, 13, 4, 12 };
                a14 = new int[] { 0, 1, 3, 2, 7, 6, 4, 5, 14, 15, 13, 12, 9, 8, 10, 11 };
                a15 = new int[] { 0, 1, 3, 2, 6, 7, 5, 4, 13, 12, 14, 15, 11, 10, 8, 9 };
                n1 = new int[] { 0, 14, 10, 4, 8, 6, 2, 12, 0, 14, 10, 4, 8, 6, 2, 12 };
                n2 = new int[] { 0, 8, 0, 8, 3, 11, 3, 11, 6, 14, 6, 14, 5, 13, 5, 13 };
                n3 = new int[] { 0, 0, 3, 3, 2, 2, 1, 1, 4, 4, 7, 7, 6, 6, 5, 5 };
                n4 = new int[] { 0, 11, 12, 7, 15, 4, 3, 8, 14, 5, 2, 9, 1, 10, 13, 6 };
                n5 = new int[] { 0, 5, 1, 4, 6, 3, 7, 2, 12, 9, 13, 8, 10, 15, 11, 14 };
                n6 = new int[] { 0, 14, 4, 10, 11, 5, 15, 1, 6, 8, 2, 12, 13, 3, 9, 7 };
                n7 = new int[] { 0, 9, 0, 9, 5, 12, 5, 12, 10, 3, 10, 3, 15, 6, 15, 6 };
                n8 = new int[] { 0, 5, 11, 14, 2, 7, 9, 12, 12, 9, 7, 2, 14, 11, 5, 0 };
                n9 = new int[] { 0, 0, 0, 0, 4, 4, 4, 4, 0, 0, 0, 0, 4, 4, 4, 4 };
                n10 = new int[] { 0, 8, 1, 9, 3, 11, 2, 10, 5, 13, 4, 12, 6, 14, 7, 15 };
                n11 = new int[] { 0, 14, 13, 3, 9, 7, 4, 10, 6, 8, 11, 5, 15, 1, 2, 12 };
                n12 = new int[] { 0, 13, 10, 7, 4, 9, 14, 3, 10, 7, 0, 13, 14, 3, 4, 9 };
                n13 = new int[] { 0, 1, 3, 2, 6, 7, 5, 4, 15, 14, 12, 13, 9, 8, 10, 11 };
                n14 = new int[] { 0, 1, 3, 2, 4, 5, 7, 6, 12, 13, 15, 14, 8, 9, 11, 10 };
                n15 = new int[] { 0, 6, 12, 10, 9, 15, 5, 3, 2, 4, 14, 8, 11, 13, 7, 1 };
                n16 = new int[] { 0, 11, 6, 13, 13, 6, 11, 0, 11, 0, 13, 6, 6, 13, 0, 11 };
                n17 = new int[] { 0, 12, 8, 4, 1, 13, 9, 5, 3, 15, 11, 7, 2, 14, 10, 6 };
                n18 = new int[] { 12, 9, 5, 2, 14, 11, 7, 5, 9, 12, 0, 7, 11, 14, 2 };
                n19 = new int[] { 0, 6, 13, 11, 10, 12, 7, 1, 5, 3, 8, 14, 15, 9, 2, 4 };
                n20 = new int[] { 0, 9, 3, 10, 7, 14, 4, 13, 14, 7, 13, 4, 9, 0, 10, 3 };
                n21 = new int[] { 0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15 };
                n22 = new int[] { 0, 1, 2, 3, 5, 4, 7, 6, 11, 10, 9, 8, 14, 15, 12, 13 };
                n23 = new int[] { 0, 7, 15, 8, 14, 9, 1, 6, 12, 11, 3, 4, 2, 5, 13, 10 };
                n24 = new int[] { 0, 5, 10, 15, 4, 1, 14, 11, 8, 13, 2, 7, 12, 9, 6, 3 };
                n25 = new int[] { 0, 11, 6, 13, 13, 6, 11, 0, 10, 1, 12, 7, 7, 12, 1, 10 };
                n26 = new int[] { 0, 13, 10, 7, 4, 9, 14, 3, 8, 5, 2, 15, 12, 1, 6, 11 };
                n27 = new int[] { 0, 4, 9, 13, 2, 6, 11, 15, 5, 1, 12, 8, 7, 3, 14, 10 };
                n28 = new int[] { 0, 14, 12, 2, 8, 6, 4, 10, 0, 14, 12, 2, 8, 6, 4, 10 };
                n29 = new int[] { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 };
                n30 = new int[] { 0, 15, 14, 1, 12, 3, 2, 13, 8, 7, 6, 9, 4, 11, 10, 5 };
                n31 = new int[] { 0, 10, 4, 14, 9, 3, 13, 7, 2, 8, 6, 12, 11, 1, 15, 5 };
                n32 = new int[] { 0, 10, 5, 15, 11, 1, 14, 4, 6, 12, 3, 9, 13, 7, 8, 2 };
                n33 = new int[] { 0, 4, 9, 13, 3, 7, 10, 14, 7, 3, 14, 10, 4, 0, 13, 9 };
                key = new int[] { 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 61, 62, 63, 64, 65, 66 };
                char[] ssid;
                ssid = new char[15] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

                mac = new int[12];
                //Ahoraya tenemos todos las variables necesarias para empezar


                for (i = 0; i < mac.Length; i++)
                {
                    mac[i] = hex2dex(BSSID[i]);
                }

                s1 = (n1[mac[0]]) ^ (a4[mac[1]]) ^ (a6[mac[2]]) ^ (a1[mac[3]]) ^ (a11[mac[4]]) ^ (n20[mac[5]]) ^ (a10[mac[6]]) ^ (a4[mac[7]]) ^ (a8[mac[8]]) ^ (a2[mac[9]]) ^ (a5[mac[10]]) ^ (a9[mac[11]]) ^ 5;
                s2 = (n2[mac[0]]) ^ (n8[mac[1]]) ^ (n15[mac[2]]) ^ (n17[mac[3]]) ^ (a12[mac[4]]) ^ (n21[mac[5]]) ^ (n24[mac[6]]) ^ (a9[mac[7]]) ^ (n27[mac[8]]) ^ (n29[mac[9]]) ^ (a11[mac[10]]) ^ (n32[mac[11]]) ^ 10;
                s3 = (n3[mac[0]]) ^ (n9[mac[1]]) ^ (a5[mac[2]]) ^ (a9[mac[3]]) ^ (n19[mac[4]]) ^ (n22[mac[5]]) ^ (a12[mac[6]]) ^ (n25[mac[7]]) ^ (a11[mac[8]]) ^ (a13[mac[9]]) ^ (n30[mac[10]]) ^ (n33[mac[11]]) ^ 11;
                s4 = (n4[mac[0]]) ^ (n10[mac[1]]) ^ (n16[mac[2]]) ^ (n18[mac[3]]) ^ (a13[mac[4]]) ^ (n23[mac[5]]) ^ (a1[mac[6]]) ^ (n26[mac[7]]) ^ (n28[mac[8]]) ^ (a3[mac[9]]) ^ (a6[mac[10]]) ^ (a0[mac[11]]) ^ 10;
                ya = (a2[mac[0]]) ^ (n11[mac[1]]) ^ (a7[mac[2]]) ^ (a8[mac[3]]) ^ (a14[mac[4]]) ^ (a5[mac[5]]) ^ (a5[mac[6]]) ^ (a2[mac[7]]) ^ (a0[mac[8]]) ^ (a1[mac[9]]) ^ (a15[mac[10]]) ^ (a0[mac[11]]) ^ 13;
                yb = (n5[mac[0]]) ^ (n12[mac[1]]) ^ (a5[mac[2]]) ^ (a7[mac[3]]) ^ (a2[mac[4]]) ^ (a14[mac[5]]) ^ (a1[mac[6]]) ^ (a5[mac[7]]) ^ (a0[mac[8]]) ^ (a0[mac[9]]) ^ (n31[mac[10]]) ^ (a15[mac[11]]) ^ 4;
                yc = (a3[mac[0]]) ^ (a5[mac[1]]) ^ (a2[mac[2]]) ^ (a10[mac[3]]) ^ (a7[mac[4]]) ^ (a8[mac[5]]) ^ (a14[mac[6]]) ^ (a5[mac[7]]) ^ (a5[mac[8]]) ^ (a2[mac[9]]) ^ (a0[mac[10]]) ^ (a1[mac[11]]) ^ 7;
                yd = (n6[mac[0]]) ^ (n13[mac[1]]) ^ (a8[mac[2]]) ^ (a2[mac[3]]) ^ (a5[mac[4]]) ^ (a7[mac[5]]) ^ (a2[mac[6]]) ^ (a14[mac[7]]) ^ (a1[mac[8]]) ^ (a5[mac[9]]) ^ (a0[mac[10]]) ^ (a0[mac[11]]) ^ 14;
                ye = (n7[mac[0]]) ^ (n14[mac[1]]) ^ (a3[mac[2]]) ^ (a5[mac[3]]) ^ (a2[mac[4]]) ^ (a10[mac[5]]) ^ (a7[mac[6]]) ^ (a8[mac[7]]) ^ (a14[mac[8]]) ^ (a5[mac[9]]) ^ (a5[mac[10]]) ^ (a2[mac[11]]) ^ 7;

                writer.WriteLine((key[ya]).ToString() + (key[yb]).ToString() + (key[yc]).ToString() + (key[yd]).ToString() + (key[ye]).ToString());
             
            }

        }
        #endregion
        /* MAGICWLANDECRYPTER */
        #region Magic Wlandecrypter
        public void mwlan(string campo1, string mac)
        {
            float performed = 0;
            float percent;
            string mac1 = mac.Replace(":", string.Empty);
            string cambio = change1(campo1, mac1);
            string cambio1 = change1(string.Empty, mac1);
            using (StreamWriter writer = File.CreateText(archivo))
            {
                // int i, j;
                if (campo1 != "XXXX")
                {
                    if (comparaMAC("00:1F:A4", mac) || comparaMAC("F4:3E:61", mac))
                    {
                        writer.WriteLine(md5(cambio.ToLower()).Remove(20).ToUpper());
                    }
                    else if (comparaMAC("00:1D:20", mac) || comparaMAC("64:68:0C", mac) || comparaMAC("38:72:C0", mac))
                    {
                        //  writer.WriteLine("bcgbghgg" + cambio + mac1);
                        writer.WriteLine(md5("bcgbghgg" + cambio + mac1).Remove(20));
                    }
                    else if (comparaMAC("00:1A:2B", mac) && gen_selected == generadores.magicwlandecrypter)
                    /* 00:1A:2B */
                    {
                        MaxProgres = 2 * 16 * 16;

                        char[] clave1 = ("bcgbghgg3872C000" + campo1 + mac1).ToCharArray();
                        char[] clave2 = ("bcgbghgg64680C00" + campo1 + mac1).ToCharArray();
                        foreach (char c in HEX)
                        {
                            foreach (char d in HEX)
                            {
                                clave1[14] = c;
                                clave1[15] = d;
                                clave2[14] = c;
                                clave2[15] = d;
                                writer.WriteLine(md5(clave1).Remove(20));
                                performed++;
                                writer.WriteLine(md5(clave2).Remove(20));
                                performed++;
                                percent = performed / MaxProgres * 100;
                                bw.ReportProgress((int)percent);
                            }
                        }

                    }
                    else if (comparaMAC("00:1A:2B", mac) && gen_selected == generadores.Jmagicwlandecrypter)
                    {

                        /********** BETA Algoritmo bucky + mambo ************/
                        string cuartaParejaMac = mac1.Substring(mac1.Length - 6).Remove(2); ;
                        //string cuartaParejaMac=fragmentoMAC.Remove(2);
                        writer.WriteLine(md5("bcgbghgg" + "3872C0" + cuartaParejaMac + campo1 + mac1).Remove(20));
                        writer.WriteLine(md5("bcgbghgg" + "001A2B" + cuartaParejaMac + campo1 + mac1).Remove(20));


                    }



                }
                else
                {
                    string mac_cortada = string.Empty;
                    for (int z = mac1.Length - 4; z < mac1.Length; z++) mac_cortada += mac1[z];

                    int SUMARESTA = int.Parse(mac_cortada, System.Globalization.NumberStyles.AllowHexSpecifier);
                    int salto1 = (SUMARESTA - 1),
                        salto3 = (SUMARESTA - 3);



                    string cambio2 = change1(string.Empty, mac1) + dec2hex(salto1),
                           cambio3 = change1(string.Empty, mac1) + dec2hex(salto3);
                    if (comparaMAC("00:1F:A4", mac) || comparaMAC("F4:3E:61", mac))
                    {
                        writer.WriteLine(md5(cambio2.ToLower()).Remove(20).ToUpper());
                        writer.WriteLine(md5(cambio3.ToLower()).Remove(20).ToUpper());
                    }
                    else if (comparaMAC("00:1D:20", mac) || comparaMAC("64:68:0C", mac) || comparaMAC("38:72:C0", mac))
                    {
                        writer.WriteLine(md5("bcgbghgg" + cambio2 + mac1).Remove(20));
                        writer.WriteLine(md5("bcgbghgg" + cambio3 + mac1).Remove(20));
                    }
                    else if (comparaMAC("00:1A:2B", mac) && gen_selected == generadores.magicwlandecrypter)
                    {

                        MaxProgres = 16 * 16 * 16 * 16 * 16 * 16;

                        char[] clave1 = ("bcgbghgg3872C000" + campo1 + mac1).ToCharArray();
                        char[] clave2 = ("bcgbghgg64680C00" + campo1 + mac1).ToCharArray();

                        foreach (char c in HEX)
                        {
                            foreach (char d in HEX)
                            {
                                clave1[14] = c;
                                clave1[15] = d;
                                writer.WriteLine(md5(clave1).Remove(20));
                                // writer.WriteLine(md5("bcgbghgg" + "3872C0" + HEX[i] + HEX[j] + ParteFinal).Remove(20));
                                performed++;
                                foreach (char e in HEX)
                                {
                                    foreach (char f in HEX)
                                    {
                                        foreach (char g in HEX)
                                        {
                                            foreach (char h in HEX)
                                            {
                                                clave2[14] = c;
                                                clave2[15] = d;
                                                clave2[16] = e;
                                                clave2[17] = f;
                                                clave2[18] = g;
                                                clave2[19] = h;

                                                clave1[14] = c;
                                                clave1[15] = d;
                                                clave1[16] = e;
                                                clave1[17] = f;
                                                clave1[18] = g;
                                                clave1[19] = h;

                                                writer.WriteLine(md5(clave1).Remove(20));
                                                writer.WriteLine(md5(clave2).Remove(20));
                                                performed++;
                                                percent = performed / MaxProgres * 100;
                                                bw.ReportProgress((int)percent);
                                            }
                                        }
                                    }
                                }
                            }

                        }

                        //  writer.WriteLine(performed);



                    }
                    else if (comparaMAC("00:1A:2B", mac) && gen_selected == generadores.Jmagicwlandecrypter)
                    {   /********** BETA Algoritmo bucky + mambo ************/
                        MaxProgres = 16 * 16 * 16 * 16;
                        string cuartaParejaMac = mac1.Substring(mac1.Length - 6).Remove(2);
                        //int k, l, longitud = HEX.Length;
                        char[] clave1 = ("bcgbghgg3872C0" + cuartaParejaMac + "0000" + mac1).ToCharArray();
                        char[] clave2 = ("bcgbghgg64680C" + cuartaParejaMac + "0000" + mac1).ToCharArray();

                        foreach (char e in HEX)
                        {
                            foreach (char f in HEX)
                            {
                                foreach (char g in HEX)
                                {
                                    foreach (char h in HEX)
                                    {


                                        clave2[16] = e;
                                        clave2[17] = f;
                                        clave2[18] = g;
                                        clave2[19] = h;

                                        clave1[16] = e;
                                        clave1[17] = f;
                                        clave1[18] = g;
                                        clave1[19] = h;

                                        writer.WriteLine(md5(clave1).Remove(20));
                                        writer.WriteLine(md5(clave2).Remove(20));
                                        performed++;
                                        percent = performed / MaxProgres * 100;
                                        bw.ReportProgress((int)percent);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

#endregion

        #region Pure Networks GenKey

        public void PureGenkey(string end, string start, bool tplink)
        {
            int performed = 0;
            double percent;
           
            long seedB = genSeedPureNetwork(Convert.ToDateTime(end));
            long seedA = genSeedPureNetwork(Convert.ToDateTime(start));
            char[] key;
            MaxProgres= (int)(seedB - seedA + 1);
            using (StreamWriter writer = new StreamWriter(archivo)){ 
                for (long i = seedB; i >= seedA; i--)
                {
                    if (tplink)
                    {
                        key = genTPlinkPureNetwork(i);
                    }
                    else
                    {
                        key = genPureNetwork(i);
                    }
                    writer.WriteLine(key);
                    performed++;
                    percent = (double)performed / (double)MaxProgres * 100;
                    bw.ReportProgress((int)percent);

                    if (i == 0) break;
                }
            }
        }

        static char[] genPureNetwork(long seed)
        {
            char[] key = new char[11];
            key[10] = (char)0xA;
            


            int len = 10;
            long edx;
            for (int i = 0; i < len; i++)
            {
                while (true)
                {
                    seed = seed * 0x343FD + 0x269EC3;
                    edx = ((seed >> 0x10) & 0x7FFF) % 0x24;
                    if (edx >= 0xA)
                    {
                        edx += 0x37;
                    }
                    else
                    {
                        edx += 0x30;
                    }
                    //If character not in "1I2Z0O5SUV"
                    if (edx > 0x32 && edx != 53 && edx != 73 && edx != 79 && edx != 83 && edx != 85 && edx != 86 && edx != 90)
                    {
                        key[i] = (char)edx;
                        break;
                    }
                }
            }
            return key;
        }


        static char[] genTPlinkPureNetwork(long seed)
        {
            char[] key = new char[11];
            key[10] = (char)0xA;
            int len = 10;
            for (int i = 0; i < len; i++)
            {
                seed = seed * 0x343FD + 0x269EC3;
                key[i] = charsTPlinkPureNetwork[(int)(((seed >> 0x10) & 0x7FFF) % 0x28)];
            }
            return key;
        }

        static long genSeedPureNetwork(DateTime end)
        {
            return genSeedPureNetwork(end, new DateTime(1601, 1, 1, 0, 0, 0));
        }

        static long genSeedPureNetwork(DateTime end, DateTime start)
        {
            TimeSpan dt = new TimeSpan(end.Ticks - start.Ticks);
            long t = (long)(dt.TotalDays * 864000000000L + dt.Seconds * 10000000L + dt.Milliseconds / 100);
            long tA = (t / (long)Math.Pow(2, 32) + 0xFE624E21);
            long tB = (t % (long)Math.Pow(2, 32) + 0x2AC18000L) % (1L << 32);
            if (tA >= (1L << 32))
            {
                tA += 1;
                tA %= 1L << 32;
            }
            long r = (tA % 0x989680L) * ((long)Math.Pow(2, 32));
            r = ((r + tB) / 0x989680L) % ((long)Math.Pow(2, 31));
            return r;
        }

        #endregion

        #region DlinkDecrypter
        /*-------------------------------dlinkdecrypter------------------------*/
        public void dlinkdecrypter(string BSSID1)
        {
            using (StreamWriter writer = File.CreateText(archivo))
            {
                string P1, P2, P3, P4, P5, P6, P61, P62, S1, S2, BSSID;
                //BSSID = extraeMAC(BSSID1);
                BSSID = BSSID1.Replace(":", String.Empty);

                int f, t;
                //Asiganacion de las primeras variables
                P1 = BSSID[0].ToString() + BSSID[1].ToString(); P2 = BSSID[2].ToString() + BSSID[3].ToString(); P3 = BSSID[4].ToString() + BSSID[5].ToString();
                P4 = BSSID[6].ToString() + BSSID[7].ToString(); P5 = BSSID[8].ToString() + BSSID[9].ToString(); P6 = BSSID[10].ToString() + BSSID[11].ToString();
                P61 = BSSID[10].ToString(); P62 = BSSID[11].ToString();
                S1 = P61;
                //inicializacion debida a las restricciones de C#
                S2 = string.Empty;

                if (P62 == "F")
                {
                    S2 = "E";
                }
                if (P62 == "E")
                {
                    S2 = "D";
                }
                if (P62 == "D")
                {
                    S2 = "C";
                }
                if (P62 == "C")
                {
                    S2 = "B";
                }
                if (P62 == "B")
                {
                    S2 = "A";
                }
                if (P62 == "A")
                {
                    S2 = "9";
                }
                if (P62 == "9")
                {
                    S2 = "8";
                }
                if (P62 == "8")
                {
                    S2 = "7";
                }
                if (P62 == "7")
                {
                    S2 = "6";
                }
                if (P62 == "6")
                {
                    S2 = "5";
                }
                if (P62 == "5")
                {
                    S2 = "4";
                }
                if (P62 == "4")
                {
                    S2 = "3";
                }
                if (P62 == "3")
                {
                    S2 = "2";
                }
                if (P62 == "2")
                {
                    S2 = "1";
                }
                if (P62 == "1")
                {
                    S2 = "0";
                }
                if (P62 == "0")
                {
                    S2 = "F";
                    if (P61 == "F")
                    {
                        S1 = "E";
                    }
                    if (P61 == "E")
                    {
                        S1 = "D";
                    }
                    if (P61 == "D")
                    {
                        S1 = "C";
                    }
                    if (P61 == "C")
                    {
                        S1 = "B";
                    }
                    if (P61 == "B")
                    {
                        S1 = "A";
                    }
                    if (P61 == "A")
                    {
                        S1 = "9";
                    }
                    if (P61 == "9")
                    {
                        S1 = "8";
                    }
                    if (P61 == "8")
                    {
                        S1 = "7";
                    }
                    if (P61 == "7")
                    {
                        S1 = "6";
                    }
                    if (P61 == "6")
                    {
                        S1 = "5";
                    }
                    if (P61 == "5")
                    {
                        S1 = "4";
                    }
                    if (P61 == "4")
                    {
                        S1 = "3";
                    }
                    if (P61 == "3")
                    {
                        S1 = "2";
                    }
                    if (P61 == "2")
                    {
                        S1 = "1";
                    }
                    if (P61 == "1")
                    {
                        S1 = "0";
                    }
                    if (P61 == "0")
                    {
                        S1 = "0";
                        S2 = "0";
                    }
                }

                float performed = 0, percent;
                MaxProgres = 16 * 16;
                for (f = 0; f < HEX.Length; f++)
                {
                    for (t = 0; t < HEX.Length; t++)
                    {

                        writer.WriteLine(P6 + P1 + P5 + P2 + P3 + P4 + P6 + P5 + P2 + P3 + P4 + P1 + HEX[f] + HEX[t]);
                        writer.WriteLine(S1 + S2 + P1 + P5 + P2 + P3 + P4 + P6 + P5 + P2 + P3 + P4 + P1 + HEX[f] + HEX[t]);
                        writer.WriteLine(S1 + S2 + P1 + P5 + P2 + P3 + P4 + S1 + S2 + P5 + P2 + P3 + P4 + P1 + HEX[f] + HEX[t]);

                        performed++;
                        percent = performed / MaxProgres * 100;
                        bw.ReportProgress((int)percent);
                    }
                }

            }
        }//End DlinkDecrypter       

        #endregion

        #region TELE2DIC
        /*------------------------------------TELE2DIC-------------------------*/
        public void tele2dic(string anio, bool t2, bool a2)
        {
            //declaracion de variables
            char[] clave = "0123456789".ToCharArray();
            string partefija = "IX1V";
            // string partefija1="IX1VPV";
            bool anio_ok = false;
            // int i, a, b, c, d, e, f;
            int i;
            string anio_ = anio;
            using (StreamWriter writer = File.CreateText(archivo))
            {


                //Empezamos con el codigo
                if (t2) { partefija = "IX1VPV"; }

                //Lanzamos comprobacion de año
                for (i = 0; i < clave.Length; i++)
                {//primer digito
                    if (anio[0] == clave[i]) anio_ok = true;
                }
                if (anio_ok)
                {
                    for (i = 0; i < clave.Length; i++)
                    {//segundo digito
                        if (anio[1] == clave[i]) anio_ok = true;
                    }
                }
                if (anio_ok)
                {
                    if (!a2) anio_ = anio[1].ToString();//si no hay que uasr los dos digitos del año solo ponemos 1


                    char[] output = (partefija + anio + "000000").ToCharArray();
                    float performed = 0, percent;
                    MaxProgres = (int)Math.Pow(10, 6);

                    int outputLenght = output.Length;

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


                                            output[outputLenght - 1] = f;
                                            output[outputLenght - 2] = e;
                                            output[outputLenght - 3] = d;
                                            output[outputLenght - 4] = c;
                                            output[outputLenght - 5] = b;
                                            output[outputLenght - 6] = a;

                                            writer.WriteLine(output);

                                            performed++;
                                            percent = performed / MaxProgres * 100;
                                            bw.ReportProgress((int)percent);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

#endregion

        #region SKYv1
        /*
          * This is the algorithm to generate the WPA passphrase
 * for the SKYv1.
 * Generate the md5 hash form the mac.
 * Use the numbers in the following positions on the hash.
 *  Position 3,7,11,15,19,23,27,31 ,
 *  Use theses numbers, modulus 26, to find the correct letter
 *  and append to the key.
         */

        public void SKYv1(string mac)
        {


            using (StreamWriter writer = new StreamWriter(archivo))
            {
                byte[] hash = Encoding.ASCII.GetBytes(md5(mac));
                String key = string.Empty;
                for (int i = 1; i <= 15; i += 2)
                {
                    int index = hash[i] & 0xFF;
                    index %= 26;
                    key += ALPHABET[index] + ALPHABET[index + 1];
                }
                writer.WriteLine(key);
            }
        }

        #endregion

        #region STKEYS
        /*---------------------------------- STKEYS -------------------------------------*/

        public void stkeys(string essid, int anio_fin)
        {
            MaxProgres = (anio_fin - 4 + 1) * 52 * 36 * 36 * 36;
            SHA1CryptoServiceProvider cryptoTransformSHA1 =
            new SHA1CryptoServiceProvider();
            float performed = 0;
            float percent;
            string str = string.Empty;
            int year, week, x1, x2, x3;
            char[] serial;
            serial = new char[] { 'C', 'P', '0', (char)0x00, (char)0x00, (char)0x00, (char)0x00, (char)0x00, (char)0x00, (char)0x00, (char)0x00, (char)0x00 };
            using (StreamWriter writer = File.CreateText(archivo))
            {

                for (year = 4; year <= anio_fin; year++)
                {

                    serial[3] = (char)(year | '0');

                    // 52 weeks of the year

                    for (week = 1; week <= 52; week++)
                    {

                        serial[4] = (char)((week / 10) + '0');
                        serial[5] = (char)((week % 10) + '0');

                        for (x1 = 0; x1 < 36; x1++)
                        {
                            serial[6] = HEX[((charTable[x1] & 0xf0) >> 4)];
                            serial[7] = HEX[(charTable[x1] & 0x0f)];

                            for (x2 = 0; x2 < 36; x2++)
                            {

                                serial[8] = HEX[((charTable[x2] & 0xf0) >> 4)];
                                serial[9] = HEX[(charTable[x2] & 0x0f)];


                                for (x3 = 0; x3 < 36; x3++)
                                {
                                    serial[10] = HEX[((charTable[x3] & 0xf0) >> 4)];
                                    serial[11] = HEX[(charTable[x3] & 0x0f)];

                                    str = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(Encoding.ASCII.GetBytes(new String(serial)))).Replace("-", string.Empty);
                                    //avanzamos en la barra de progreso
                                    performed++;
                                    percent = performed / MaxProgres * 100;
                                    bw.ReportProgress((int)percent);


                                    if (str.Substring(34) == essid)
                                    {
                                        writer.WriteLine(str.Remove(10));

                                    }


                                }
                            }
                        }
                    }
                }

            }
        }

        #endregion

        #region wlandecrypter

        /* -------------------------------------------------------
         * --------Generador que se usa en WLANDECRYPTER----------
         * -------------------------------------------------------
         */

        public void wlanjazztel(string partefija, string partefija2, string partefija3, string final)
        {
            using (StreamWriter writer = File.CreateText(archivo))
            {
                bool boolParteFija2 = (partefija2 != string.Empty);
                bool boolParteFija3 = (partefija3 != string.Empty);
                char[] key1 = (partefija + "0000" + final).ToCharArray();
                int key1lenght = key1.Length;

                char[] key2 = null;
                int key2lenght = 0;
                if (boolParteFija2)
                {
                    key2 = (partefija2 + "0000" + final).ToCharArray();
                    key2lenght = key2.Length;
                }


                char[] key3 = null;
                int key3lenght = 0;
                if (boolParteFija3)
                {
                    key3 = (partefija3 + "0000" + final).ToCharArray();
                    key3lenght = key3.Length;
                }


                if (final != "XX")
                {

                    int mult = 1 + ((partefija2 != string.Empty) ? 1 : 0) + ((partefija3 != string.Empty) ? 1 : 0);
                    MaxProgres = (int)Math.Pow(HEX.Length, 4) * mult;
                    float performed = 0;
                    float percent = 0;

                    foreach (char ii in HEX)
                    {
                        foreach (char jj in HEX)
                        {
                            foreach (char kk in HEX)
                            {
                                foreach (char ll in HEX)
                                {

                                    key1[key1lenght - 3] = ii;
                                    key1[key1lenght - 4] = jj;
                                    key1[key1lenght - 5] = kk;
                                    key1[key1lenght - 6] = ll;
                                    writer.WriteLine(key1);

                                    performed++;
                                    percent = performed / MaxProgres * 100;
                                    bw.ReportProgress((int)percent);
                                    if (boolParteFija2)
                                    {
                                        key2[key2lenght - 3] = ii;
                                        key2[key2lenght - 4] = jj;
                                        key2[key2lenght - 5] = kk;
                                        key2[key2lenght - 6] = ll;
                                        writer.WriteLine(key2);
                                        performed++;
                                        percent = performed / MaxProgres * 100;
                                        bw.ReportProgress((int)percent);
                                    }

                                    if (boolParteFija3)
                                    {

                                        key3[key3lenght - 3] = ii;
                                        key3[key3lenght - 4] = jj;
                                        key3[key3lenght - 5] = kk;
                                        key3[key3lenght - 6] = ll;
                                        writer.WriteLine(key3);

                                        //writer.WriteLine(partefija3 + HEX[i] + HEX[j] + HEX[k] + HEX[l] + final);
                                        performed++;
                                        percent = performed / MaxProgres * 100;
                                        bw.ReportProgress((int)percent);


                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    int mult = 1 + ((partefija2 !=string.Empty) ? 1 : 0) + ((partefija3 != string.Empty) ? 1 : 0);
                    MaxProgres = (int)Math.Pow(HEX.Length, 6) * mult;
                    float performed = 0;
                    float percent = 0;
                    foreach (char ii in HEX)
                    {
                        foreach (char jj in HEX)
                        {
                            foreach (char kk in HEX)
                            {
                                foreach (char ll in HEX)
                                {
                                    foreach (char nn in HEX)
                                    {
                                        foreach (char oo in HEX)
                                        {
                                            key1[key1lenght - 1] = oo;
                                            key1[key1lenght - 2] = nn;
                                            key1[key1lenght - 3] = ll;
                                            key1[key1lenght - 4] = kk;
                                            key1[key1lenght - 5] = jj;
                                            key1[key1lenght - 6] = ii;
                                            writer.WriteLine(key1);

                                            if (boolParteFija2)
                                            {
                                                key2[key2lenght - 1] = oo;
                                                key2[key2lenght - 2] = nn;
                                                key2[key2lenght - 3] = ll;
                                                key2[key2lenght - 4] = kk;
                                                key2[key2lenght - 5] = jj;
                                                key2[key2lenght - 6] = ii;
                                                writer.WriteLine(key2);

                                                performed++;
                                                percent = performed / MaxProgres * 100;
                                                bw.ReportProgress((int)percent);
                                            }

                                            if (boolParteFija3)
                                            {
                                                key3[key1lenght - 1] = oo;
                                                key3[key1lenght - 2] = nn;
                                                key3[key1lenght - 3] = ll;
                                                key3[key1lenght - 4] = kk;
                                                key3[key1lenght - 5] = jj;
                                                key3[key1lenght - 6] = ii;
                                                writer.WriteLine(key3);

                                                performed++;
                                                percent = performed / MaxProgres * 100;
                                                bw.ReportProgress((int)percent);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region discus
        //-------------------------------------------------
        //Generador de Claves PAra las redes Discus-XXXXXX-
        //-------------------------------------------------
        public void discus(string essid)
        {
            using (StreamWriter writer = new StreamWriter(archivo))
            {
                int constn = Convert.ToInt32("D0EC31", 16);
                int inp = Convert.ToInt32(essid, 16);
                int result = (inp - constn) / 4;
                // writer.WriteLine(inp.ToString()+" " + constn.ToString());
                writer.WriteLine("YW0" + result.ToString());
            }
        }
        #endregion

        #region GenTekom
        //---------------------------------------------------
        // Generador Tekom 00:19:15
        //---------------------------------------------------
        private void genTekom(string MAC)
        {
            string result = string.Empty;
            using (StreamWriter writer = File.CreateText(archivo))
            {
                float performed = 0, percent;
                MaxProgres = (int)(246510000000 - 246500000000);
                int count = 0;
                System.Int64 i;
                char[] Val = new char[20];


                string mac1 = MAC.Replace(":", string.Empty);
                writer.WriteLine(md5("bcgbghgg" + mac1 + mac1).Remove(20));

                DialogResult dr = new DialogResult();
                using (swifi_keygen.advertBox adv = new swifi_keygen.advertBox())
                {
                    dr = adv.ShowDialog();
                }
                if (dr == DialogResult.Yes)
                {
                    for (i = 246500000000; i <= 246510000000; i++)
                    {
                        result = md5b64(i.ToString());

                        for (count = 0; count < 20; count++)
                        {

                            if (Char.IsLetter(result[count]))
                            {
                                if (Char.IsUpper(result[count]))

                                    Val[count] = Char.ToLower(result[count]);
                                else

                                    Val[count] = Char.ToUpper(result[count]);

                            }
                            else if (result[count] == '+')
                            {

                                Val[count] = '.';
                            }
                            else
                                Val[count] = result[count];


                        }//end for

                        performed++;
                        percent = performed / MaxProgres * 100;
                        bw.ReportProgress((int)percent);
                        writer.WriteLine(Val);
                    }//end for
                }
            }

        }
        static private string md5b64(string Value)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(Value);
            data = x.ComputeHash(data);
            string returnValue = System.Convert.ToBase64String(data);
            return returnValue;

        }

        #endregion

        #region Orange-XXXX

        private  void OrangeXXXX(string starting) {

            using (StreamWriter writer = File.CreateText(archivo))
            {

                char[] key = new char[8];
                const string values = "2345679ACEF";
                float performed = 0, percent;
                

                if (starting.Length > 0)
                {
                    MaxProgres = (int)Math.Pow(values.Length,7);
                    char start = starting[0];

                    foreach (char b in values)
                    {
                        foreach (char c in values)
                        {
                            foreach (char d in values)
                            {
                                foreach (char e in values)
                                {
                                    foreach (char f in values)
                                    {
                                        foreach (char g in values)
                                        {
                                            foreach (char h in values)
                                            {
                                                key[0] = start;
                                                key[1] = b;
                                                key[2] = c;
                                                key[3] = d;
                                                key[4] = e;
                                                key[5] = f;
                                                key[6] = g;
                                                key[7] = h;
                                                writer.WriteLine(key);
                                                performed++;
                                                percent = performed / MaxProgres * 100;
                                                bw.ReportProgress((int)percent);


                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                else
                {
                    MaxProgres = (int)Math.Pow(values.Length, 8);

                    foreach (char a in values)
                    {
                        foreach (char b in values)
                        {
                            foreach (char c in values)
                            {
                                foreach (char d in values)
                                {
                                    foreach (char e in values)
                                    {
                                        foreach (char f in values)
                                        {
                                            foreach (char g in values)
                                            {
                                                foreach (char h in values)
                                                {
                                                    key[0] = a;
                                                    key[1] = b;
                                                    key[2] = c;
                                                    key[3] = d;
                                                    key[4] = e;
                                                    key[5] = f;
                                                    key[6] = g;
                                                    key[7] = h;
                                                    writer.WriteLine(key);
                                                    performed++;
                                                    percent = performed / MaxProgres * 100;
                                                    bw.ReportProgress((int)percent);


                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

            }
        
        }


        #endregion

        #region PinVodafone
        void VodafoneXXXX(string MACStr,bool esEspana)
        {
            string[] MACValues = MACStr.Split(':');
            string DecimalMac = (getDecimalValue(MACValues[4] + MACValues[5])).ToString().PadLeft(5, '0');
            UInt16 S6 = UInt16.Parse(DecimalMac[0].ToString()),
                S7 = UInt16.Parse(DecimalMac[1].ToString()),
                S8 = UInt16.Parse(DecimalMac[2].ToString()),
                S9 = UInt16.Parse(DecimalMac[3].ToString()),
                S10 = UInt16.Parse(DecimalMac[4].ToString(), System.Globalization.NumberStyles.HexNumber),
                M7 = UInt16.Parse(MACValues[3][0].ToString(), System.Globalization.NumberStyles.HexNumber),
                M8 = UInt16.Parse(MACValues[3][1].ToString(), System.Globalization.NumberStyles.HexNumber),
                M9 = UInt16.Parse(MACValues[4][0].ToString(), System.Globalization.NumberStyles.HexNumber),
                M10 = UInt16.Parse(MACValues[4][1].ToString(), System.Globalization.NumberStyles.HexNumber),
                M11 = UInt16.Parse(MACValues[5][0].ToString(), System.Globalization.NumberStyles.HexNumber),
                M12 = UInt16.Parse(MACValues[5][1].ToString(), System.Globalization.NumberStyles.HexNumber);
            UInt16 K1 = (UInt16)((S7 + S8 + M11 + M12) & 0x0F);
            UInt16 K2 = (UInt16)((M9 + M10 + S9 + S10) & 0x0f);
            UInt16 X1 = (UInt16)(K1 ^ S10);
            UInt16 X2 = (UInt16)(K1 ^ S9);
            UInt16 X3 = (UInt16)(K1 ^ S8);
            UInt16 Y1 = (UInt16)(K2 ^ M10);
            UInt16 Y2 = (UInt16)(K2 ^ M11);
            UInt16 Y3 = (UInt16)(K2 ^ M12);
            UInt16 Z1 = (UInt16)(M11 ^ S10);
            UInt16 Z2 = (UInt16)(M12 ^ S9);
            UInt16 Z3 = (UInt16)(K1 ^ K2);
            using (StreamWriter writer = File.CreateText(archivo))
            {
                string clave = X1.ToString("X") + Y1.ToString("X") + Z1.ToString("X") + X2.ToString("X") + Y2.ToString("X") + Z2.ToString("X") + X3.ToString("X") + Y3.ToString("X") + Z3.ToString("X");
                if (esEspana)
                {
                    clave = clave.Replace("0","1");
                }
                writer.WriteLine(clave);
            }
        }
        #endregion

        #region WifiArnet

        static string AddIntToMAC(string MAC, int i)
        {
            string hex = MAC.Replace(":", "");
            long macAsNumber = Convert.ToInt64(hex, 16);
            macAsNumber = macAsNumber + i;
            return macAsNumber.ToString("X12");
        }
        public static byte[] CombineWifiArnet(byte[] third)
        {
            byte[] ret = new byte[seed.Length + seed2.Length + third.Length];
            Buffer.BlockCopy(seed, 0, ret, 0, seed.Length);
            Buffer.BlockCopy(seed2, 0, ret, seed.Length, seed2.Length);
            Buffer.BlockCopy(third, 0, ret, seed.Length + seed2.Length,
                             third.Length);
            return ret;
        }

        readonly static byte[] seed = {0x64,0xC6,0xDD,0xE3,0xE5,0x79,0xB6,0xD9,0x86,0x96,0x8D,0x34,0x45,0xD2,0x3B,0x15,
                                0xCA,0xAF,0x12,0x84,0x02,0xAC,0x56,0x00,0x05,0xCE,0x20,0x75,0x91,0x3F,0xDC,0xE8};
        static byte[] seed2;

        public static char[] GenKeyWifiArnet(string MAC)
        {
            byte[] shadigest = CombineWifiArnet(StringToByteArray(MAC));
            byte[] sha256;
            string sha256str = getHashSha256(shadigest, out sha256);
            Console.WriteLine("SHA256:" + sha256str);
            char[] key = new char[10];
            for (int x = 0; x < 10; x++)
            {
                key[x] = alphabetlower[sha256[x] % alphabetlower.Length];

            }
            return key;

        
        
        }

        /*     
         * targets = ['00:08:27','00:13:C8','00:17:C2','00:19:3E','00:1C:A2','00:1D:8B','00:22:33','00:8C:54',
                '30:39:F2','74:88:8B','84:26:15','A4:52:6F','A4:5D:A1','D0:D4:12','D4:D1:84','DC:0B:1A','F0:84:2F']
        */
        public void WiFiArnetXXXX(string MAC,bool printallkeys) {
            using (StreamWriter writer = new StreamWriter(archivo))
            {
                seed2 = Encoding.ASCII.GetBytes("1236790");

                if (!printallkeys)
                {
                    writer.WriteLine(GenKeyWifiArnet(AddIntToMAC(MAC, 0)));

                }
                else
                {
                    for (int i = -2; i <= 5; i++)
                    {
                        writer.WriteLine(GenKeyWifiArnet(AddIntToMAC(MAC, i)));
                    }
                }
            }
        
        }
        #endregion
        #region backgroundworker StartGame DoWork Complete
        public void startGame(string ESSID, string MAC, opcio opc, bool claves, bool anio, bool BelkinAllKeys1)
        {
            this.anio = anio;
            this.claves = claves;
            this.ESSID = ESSID;
            this.MAC = MAC;
            this.opc = opc;
            this.BelkinAllKeys = BelkinAllKeys1;
            WorkerFirstTime = true;
            ERROR = false;
            if (!bw.IsBusy)
                bw.RunWorkerAsync();
            else
                error1("Worker ocupado, espere a que termine.", "Error", 92);
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            bool isValidMac;//x2=false;
            isValidMac = (MAC.Length == 17) && is_hex(MAC);

            /*
             * Funcion que cargara el generador correspondiente
             */
            switch (gen_selected)
            {
                case Generadores.generadores.wlandecrypter://wlandecrypter WLAN_XX
                    //longitud = 13;
                    if (isValidMac && ESSID.Length == 7)
                    {
                        if (this.comparaMAC("00:60:B3", MAC))
                        {

                            this.wlanjazztel("Z001349", string.Empty, string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("00:01:38", MAC))
                        {
                            this.wlanjazztel("X000138", string.Empty, string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("00:03:C9", MAC))
                        {
                            this.wlanjazztel("C0030DA", string.Empty, string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            // x2 = true;
                        }
                        else if (this.comparaMAC("00:A0:C5", MAC))
                        {
                            this.wlanjazztel("Z001349", string.Empty, string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("00:16:38", MAC))
                        {
                            this.wlanjazztel("C0030DA", string.Empty, string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("00:13:49", MAC))
                        {
                            this.wlanjazztel("Z001349", "Z0002CF", string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("00:02:CF", MAC))
                        {
                            this.wlanjazztel("Z0002CF", "Z0023F8", string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("00:1A:2B", MAC))
                        {
                            this.wlanjazztel("C0030DA", "C001D20", "C64680C", ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //    x2 = true;
                        }
                        else if (this.comparaMAC("00:19:CB", MAC))
                        {
                            this.wlanjazztel("Z0002CF", "Z0019CB", string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            // x2 = true;
                        }
                        else if (this.comparaMAC("00:19:15", MAC))
                        {
                            this.wlanjazztel("C0030DA", string.Empty, string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("00:30:DA", MAC))
                        {
                            this.wlanjazztel("C0030DA", string.Empty, string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("00:1D:20", MAC))
                        {
                            this.wlanjazztel("C001D20", string.Empty, string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("00:23:F8", MAC))
                        {
                            this.wlanjazztel("Z0023F8", "Z404A03", string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("00:01:36", MAC))
                        {
                            this.wlanjazztel("X000138", string.Empty, string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("00:03:DA", MAC))
                        {
                            this.wlanjazztel("C0030DA", string.Empty, string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("00:1F:9F", MAC))
                        {
                            this.wlanjazztel("T5YF69A", string.Empty, string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("E0:91:53", MAC))
                        {
                            this.wlanjazztel("XE09153", string.Empty, string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("40:4A:03", MAC))
                        {
                            this.wlanjazztel("Z404A03", "Z5067F0", string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("50:67:F0", MAC))
                        {
                            this.wlanjazztel("Z5067F0", "Z404A03", string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //x2 = true;
                        }
                        else if (this.comparaMAC("C8:6C:87", MAC))
                        {
                            this.wlanjazztel("ZC86C87", string.Empty, string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //  x2 = true;
                        }
                        else
                        {
                            error1("Mac no soportada por Wlandecrypter", "Error", 80);
                            ERROR = true;
                        }
                    }
                    else
                    {
                        error1("Mac o Essid inválidos", "Error", 25); ERROR = true;
                    }
                    break;



                case Generadores.generadores.jazzteldecrypter: //jazteldecrypter
                    //longitud = 13;
                    if (isValidMac)
                    {
                        if (this.comparaMAC("00:1A:2B", MAC))
                        {
                            this.wlanjazztel("E001D20", "E64680C", string.Empty, ESSID.Substring(ESSID.Length - 2).ToUpper());
                            //    x2 = true;
                        }
                        else
                        {
                            error1("Mac no soportada por Jazzteldecrypter", "Error", 90);
                            ERROR = true;
                        }
                    }
                    else
                    {
                        error1("Se esperaba una mac del tipo XX:XX:XX:XX:XX:XX", "Error", 92);
                        ERROR = true;
                    }
                    break;
                case Generadores.generadores.thompsonxxxxxx://ThomsonXXXXXX stkeys 
                    //longitud = 10;
                    if (ESSID.Length != 13)
                    {
                        error1("Se esperaba un ssid del estilo ThomsonXXXXXX", "Error", 95);
                        ERROR = true;
                    }
                    else
                    {
                        if (this.is_hex(ESSID.Substring(ESSID.Length - 6)))
                        {
                            // label4.Visible = true;
                            this.stkeys(ESSID.Substring(ESSID.Length - 6), 7);
                            //x2 = true;
                        }
                        else
                        {
                            error1("Essid inválido", "Error", 15);
                            ERROR = true;
                        }
                    }

                    break;
                case Generadores.generadores.speedtouchxxxxxx:// SpeedTouch stkeys
                    //longitud = 10;
                    if (ESSID.Length != 16)
                    {
                        error1("Se esperaba un ssid del estilo SpeedTouchXXXXXX", "Error", 95); ERROR = true;
                    }
                    else
                    {
                        if (this.is_hex(ESSID.Substring(ESSID.Length - 6)))
                        {
                            //label4.Visible = true;
                            //x2 = true;
                            this.stkeys(ESSID.Substring(ESSID.Length - 6), 7);
                        }
                        else
                        {
                            error1("Essid inválido", "Error", 15); ERROR = true;
                        }
                    }
                    break;
                case Generadores.generadores.wlan4xx://WLANXXXXXX
                    //longitud = 13;
                    if (isValidMac && ESSID.Length == 10)
                    {
                        this.wlan4xx(ESSID, MAC);// x2 = true;
                    }
                    else
                    {
                        error1("MAC o ESSID inválido", "Error", 25); ERROR = true;
                    }
                    break;
                case Generadores.generadores.wifixxxxxxx://WifiXXXXXX
                    //longitud = 13;
                    if (isValidMac && ESSID.Length == 10)
                    {
                        this.wlan4xx(ESSID, MAC);// x2 = true;
                    }
                    else
                    {
                        error1("MAC o ESSID inválido", "Error", 25); ERROR = true;
                    }
                    break;
                case Generadores.generadores.yacomxxxxxx://YaComXXXXXX
                    //longitud = 13;
                    if (isValidMac && ESSID.Length == 11)
                    {
                        this.wlan4xx(ESSID, MAC);// x2 = true;
                    }
                    else
                    {
                        error1("MAC o ESSID inválido", "Error", 25); ERROR = true;
                    }

                    break;
                case Generadores.generadores.ono://ONOXXXX
                    //if (opc == this.opcio.WEP) longitud = 26; else longitud = 14;
                    if (isValidMac && ESSID.Length == 7)
                    {
                        this.generaONO(ESSID, MAC, opc);

                        //x2 = true;
                    }
                    else
                    {
                        error1("Se esperaba una mac del tipo XX:XX:XX:XX:XX:XX", "Error", 92); ERROR = true;
                    }
                    break;

                case Generadores.generadores.dlinkdecrypter://Dlink
                    //longitud = 26;
                    if (isValidMac)
                    {
                        this.dlinkdecrypter(MAC); //x2 = true;
                    }
                    else
                    {
                        error1("Se esperaba una mac del tipo XX:XX:XX:XX:XX:XX", "Error", 92); ERROR = true;
                    }
                    break;
                case Generadores.generadores.rwlandecrypter://Rwlan
                    if (MAC.Length <= 1)
                    {
                        this.Rwlan(MAC); //x2 = true;
                    }
                    else
                    {
                        error1("La longitud de la cadena debe ser 0 o 1 carácter", "Error", 95); ERROR = true;
                    }
                    break;
                case Generadores.generadores.tele2dic://Tele2

                    if (MAC.Length == 2)
                    {
                        this.tele2dic(MAC, claves, anio);
                        //x2 = true;
                    }
                    else
                    {

                        error1("Longitud del año no válida", "Error", 40); ERROR = true;

                    }
                    break;

                case Generadores.generadores.orangexxxxxx://Orange XXXXXX

                    //longitud = 10;
                    if (ESSID.Length != 12) error1("Se esperaba un ssid del estilo OrangeXXXXXX", "Error", 90);
                    else
                    {
                        if (this.is_hex(ESSID.Substring(ESSID.Length - 6)))
                        {
                            //label4.Visible = true;
                            this.stkeys(ESSID.Substring(ESSID.Length - 6), 9);
                            //x2 = true;
                        }
                        else
                        {
                            error1("Essid inválido", "Error", 10); ERROR = true;
                        }
                    }
                    break;
                case Generadores.generadores.verizon://VerizonXXXXX
                    //longitud = 10;
                    if (ESSID.Length != 12)
                    {
                        error1("Essid inválido", "Error", 10); ERROR = true;
                    }
                    else
                    {
                        this.Verizon(ESSID.Substring(ESSID.Length - 5));
                        //x2 = true;
                    }
                    break;
                case Generadores.generadores.mac2wepkey: //Infinitum
                    // longitud = 10;
                    if (isValidMac)
                    {
                        this.mac2wepkey(MAC.Replace(":", string.Empty));
                        //x2 = true;
                    }
                    else
                    {
                        error1("Se esperaba una mac del tipo XX:XX:XX:XX:XX:XX", "Error", 92); ERROR = true;
                    }

                    break;//Discus
                case Generadores.generadores.discus:
                    // longitud=9;
                    if (ESSID.Length != 14)
                    {
                        error1("Se esperaba un Essid del tipo Discus--XXXXXX", "Error", 95); ERROR = true;
                    }
                    else
                    {
                        this.discus(ESSID.Substring(ESSID.Length - 6));
                        //x2 = true; 
                    }
                    break;

                case Generadores.generadores.Jmagicwlandecrypter://JAzztel magic
                    //  longitud = 20;
                    if (isValidMac && this.comparaMAC("64:68:0C", MAC) | this.comparaMAC("00:1F:A4", MAC) | this.comparaMAC("00:1A:2B", MAC))
                    {
                        this.mwlan(ESSID.Substring(ESSID.Length - 4), MAC);
                        //x2 = true;
                    }
                    else { error1("MAC no soportada", "Error", 15); ERROR = true; }

                    break;
                case Generadores.generadores.magicwlandecrypter://WLAN MAGIC


                    //  longitud = 20;
                    if (isValidMac && ESSID != string.Empty && (this.comparaMAC("64:68:0C", MAC) | this.comparaMAC("00:1D:20", MAC)) | this.comparaMAC("00:1F:A4", MAC) | this.comparaMAC("38:72:C0", MAC) | this.comparaMAC("00:1A:2B", MAC) | this.comparaMAC("F4:3E:61", MAC))
                    {
                        this.mwlan(ESSID.Substring(ESSID.Length - 4), MAC);
                        //x2 = true;
                    }
                    else if (isValidMac && this.comparaMAC("00:19:15", MAC))
                    {
                        this.genTekom(MAC);

                    }
                    else
                    {
                        error1("MAC no soportada", "Error", 15);
                        ERROR = true;
                    }

                    break;
                case generadores.TPLINK_PureGenKeys2:
                    
                    DateTime tmp;
                    try
                    {
                        tmp= Convert.ToDateTime(MAC);
                        tmp = Convert.ToDateTime(ESSID);
                        PureGenkey(MAC, ESSID, true);
                    }
                    catch {
                        error1("Se esperaba fecha del estilo dd/MM/aaaa.", "Error", 94); ERROR = true;
                    }
                    break;
                case generadores.Linksys_DLINK_PureGenkeys2:
                    try
                    {
                        tmp = Convert.ToDateTime(MAC);
                        tmp = Convert.ToDateTime(ESSID);
                        PureGenkey(MAC, ESSID, false);
                    }
                    catch {
                         error1("Se esperaba fecha del estilo dd/MM/aaaa.", "Error", 94); ERROR = true;
                    }

                    
                    
                    break;


                case Generadores.generadores.skyv1: //SKYv1

                    this.SKYv1(MAC.Replace(":", string.Empty));
                    break;

                case Generadores.generadores.OrangeXXXX://OrangeXXXX

                    if (isValidMac && this.comparaMAC("50:7E:5D", MAC) | this.comparaMAC("1C:C6:3C", MAC) | this.comparaMAC("74:31:70", MAC) | this.comparaMAC("84:9C:A6", MAC) | this.comparaMAC("88:03:55", MAC))
                    {
                        if (ESSID == Main.labelOrangeXXXX) ESSID = string.Empty;
                        this.OrangeXXXX(ESSID);

                    }
                    else { error1("MAC no soportada", "Error", 15); ERROR = true; }

                    break;


                    /******************************************  *******************************/
                case Generadores.generadores.EasyBox_es :
                    if (isValidMac && this.comparaMAC("74:31:70", MAC) | this.comparaMAC("84:9C:A6", MAC) | this.comparaMAC("88:03:55", MAC) | this.comparaMAC("1C:C6:3C", MAC) | this.comparaMAC("50:7E:5D", MAC) | this.comparaMAC("00:12:BF", MAC))
                    {
                        this.VodafoneXXXX(MAC,true);
                    }
                    else { error1("MAC no soportada", "Error", 15); ERROR = true; }

                    break;
                case Generadores.generadores.EasyBox_de:
                    if (isValidMac && this.comparaMAC("74:31:70", MAC) | this.comparaMAC("84:9C:A6", MAC) | this.comparaMAC("88:03:55", MAC) | this.comparaMAC("1C:C6:3C", MAC) | this.comparaMAC("50:7E:5D", MAC) | this.comparaMAC("00:12:BF", MAC))
                    {
                        this.VodafoneXXXX(MAC, false);
                    }
                    else { error1("MAC no soportada", "Error", 15); ERROR = true; }

                    break;
                case Generadores.generadores.Belkin4xx:
                    if (isValidMac)
                    {
                        this.Belkin4xx(MAC,ESSID,BelkinAllKeys);
                    }
                    else { error1("MAC no soportada", "Error", 15); ERROR = true; }

                    break;
                case Generadores.generadores.WiFiArnetXXXX:
                    if (isValidMac)
                    {
                        this.WiFiArnetXXXX(MAC, BelkinAllKeys);
                    }
                    else { error1("MAC no soportada", "Error", 15); ERROR = true; }

                    break;
                      

                default:

                    error1("No implementado aún", "Error", 20); ERROR = true;

                    break;


            }
        }



        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            if (WorkerFirstTime == true)
            {
                progresso = new swifi_keygen.boxProgreso();
                progresso.Show();

                WorkerFirstTime = false;
            }
            try
            {
                progresso.progressBar1.Value = e.ProgressPercentage;
            }
            catch { }



        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                progresso.Close();
                progresso.Dispose();
            }
            catch
            {


            }
            if (!ERROR)
                error1("La creación del diccionario ha finalizado", "Advertencia", 92);

        }
        #endregion

        #region errores
        private void derror1(string error)
        {

            using (swifi_keygen.dialogError dlgError = new swifi_keygen.dialogError("Error"))
            {
                dlgError.label1.Text = error;

                dlgError.ShowDialog();
            }


        }
        private void error1(string error, string title, int offset)
        {

            using (swifi_keygen.dialogError dlgError = new swifi_keygen.dialogError(title))
            {
                dlgError.label1.Text = error;
                dlgError.label1.Left = dlgError.label1.Left - offset;

                dlgError.ShowDialog();

            }

        }
        #endregion

    }
}
