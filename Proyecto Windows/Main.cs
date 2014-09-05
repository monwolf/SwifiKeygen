using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;


namespace Proyecto_Windows
{
    public partial class Main : Form
    {
        
        Generadores.opcio opc;
        Generadores gen=new Generadores();

        public static string labelOrangeXXXX ="Char Inicial";


        public Main()
        {
            InitializeComponent();
            anio.Visible = false;
            claves.Visible = false;
            this.Text += System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
           
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            gen.Set_File(archivo.Text);
            //int j;
            Nombre.Text = Nombre.Text.ToUpper() ;
            MAC.Text = MAC.Text.ToUpper();
            Genera.Enabled = false;
            gen.startGame(Nombre.Text, MAC.Text,opc,claves.Checked,anio.Checked,chkAllKeys.Checked);
            Genera.Enabled = true;
        
            
        }

        private void tipored_SelectedIndexChanged(object sender, EventArgs e)
        {
  
            archivo.Enabled = true;
            Genera.Enabled = true;
            anio.Visible = false;
            WEP.Visible = false;
            WPA.Visible = false;
            anio.Visible = false;
            claves.Visible = false;
            Nombre.Enabled = true;
            MAC.Enabled = true;
            chkAllKeys.Visible = false;
            label3.Text = "                        Escribe la MAC:";
            if ((string)tipored.SelectedItem == "WLAN_XX")
            {
                //i = 0;
                gen.gen_selected = Generadores.generadores.wlandecrypter;
                MAC.Text = "";
                
                Nombre.Text = "WLAN_XX";
                
                
            }
            else if((string)tipored.SelectedItem == "JAZZTEL_XX"){
                //i = 1;
                gen.gen_selected = Generadores.generadores.jazzteldecrypter;
                MAC.Text="00:1A:2B:XX:XX:XX";
                Nombre.Text = "JAZZTEL_XX";
               
            }
            else if ((string)tipored.SelectedItem == "ThomsonXXXXXX")
            {
                //i = 2;
                gen.gen_selected = Generadores.generadores.thompsonxxxxxx;
                MAC.Text = "";
                Nombre.Text = "ThomsonXXXXXX";
                
            }
            else if ((string)tipored.SelectedItem == "SpeedTouchXXXXXX")
          {
              gen.gen_selected = Generadores.generadores.speedtouchxxxxxx;
                //i = 3;
                MAC.Text = "";
                Nombre.Text = "SpeedTouchXXXXXX";
            }
             else if ((string)tipored.SelectedItem == "WlanXXXXXX")
            {
              //  i = 4;
                gen.gen_selected = Generadores.generadores.wlan4xx;
                MAC.Text = "";
                Nombre.Text = "WlanXXXXXX";
            }
            else if ((string)tipored.SelectedItem == "WiFiXXXXXX")
          {
              gen.gen_selected = Generadores.generadores.wifixxxxxxx;
               // i = 5;
                MAC.Text = "";
                Nombre.Text = "WiFiXXXXXX";
            }
            else if ((string)tipored.SelectedItem == "YaComXXXXXX")
            {
                gen.gen_selected = Generadores.generadores.yacomxxxxxx;
                //i = 6;
                MAC.Text = "";
                Nombre.Text = "YaComXXXXXX";
            }
            else if ((string)tipored.SelectedItem == "ONOXXXX")
            {
               // i = 7;
                gen.gen_selected = Generadores.generadores.ono;
                MAC.Text = "00:01:38:XX:XX:XX";
                Nombre.Text = "ONOXXXX";
                //Habilitamos las opciones de ONOXXXX
                WEP.Visible = true;
                WPA.Visible = true;
            }
            else if ((string)tipored.SelectedItem == "Dlink")
          {
              gen.gen_selected = Generadores.generadores.dlinkdecrypter;
                //i = 8;
                MAC.Text = "";
                Nombre.Text = "Dlink";
            }
            else if ((string)tipored.SelectedItem == "RWlan")
          {
              gen.gen_selected = Generadores.generadores.rwlandecrypter;
                //i = 9;
                MAC.Text = "";
                Nombre.Text = "RWlanX";
                
                label3.Text = "Relleno por defecto (1 Carácter)"
                    ;
            }
            else if ((string)tipored.SelectedItem == "Tele2")
          {
              gen.gen_selected = Generadores.generadores.tele2dic;
                //i = 10;
                Nombre.Text = "Tele2";
                MAC.Text = "Pon el Año(2 dígitos)";
                anio.Visible = true;
                claves.Visible = true;
            }
          else if ((string)tipored.SelectedItem == "OrangeXXXXXX")
          {
              gen.gen_selected = Generadores.generadores.orangexxxxxx;
              //i = 11;
              MAC.Text = "";
              Nombre.Text = "OrangeXXXXXX(exp)";

          }
          else if ((string)tipored.SelectedItem == "VerizonXXXXX")
          {
              gen.gen_selected = Generadores.generadores.verizon;
              //i = 12;
              MAC.Enabled = false;
              Nombre.Text = "VerizonXXXXX";

          }

          else if ((string)tipored.SelectedItem == "INFINITUMxxxx")
          {
              gen.gen_selected = Generadores.generadores.mac2wepkey;
            //  i = 13;
              MAC.Text = "XX:XX:XX:XX:XX:XX";
              Nombre.Text = "INFINITUMxxxx";
              

          }
          else if ((string)tipored.SelectedItem == "Discus--XXXXXX")
          {
              gen.gen_selected = Generadores.generadores.discus;
             // i = 14;
              MAC.Enabled = false;
              Nombre.Text = "discus--XXXXXX";
              

          }
          else if ((string)tipored.SelectedItem == "JAZZTEL_XXXX")
          {
              gen.gen_selected = Generadores.generadores.Jmagicwlandecrypter;
             // i = 15;
              Nombre.Text = "JAZZTEL_XXXX";
              MAC.Text = "64:68:0C:XX:XX:XX";
          }

          else if ((string)tipored.SelectedItem == "WLAN_XXXX")
          {
              //i = 16;
              gen.gen_selected = Generadores.generadores.magicwlandecrypter;
              Nombre.Text = "WLAN_XXXX";
              MAC.Text = "XX:XX:XX:XX:XX:XX";
          }

          else if ((string)tipored.SelectedItem == "SKY_v1")
          {
              gen.gen_selected = Generadores.generadores.skyv1;
              //i = 17;
              Nombre.Text = "SKY_v1";
              MAC.Text = "XX:XX:XX:XX:XX:XX";
          }
            else if ((string)tipored.SelectedItem == "DLINK_XXXXXX" || (string)tipored.SelectedItem == "LINKSYS_XXXXXX")
            {
                string txt = "Fecha final:";
                label3.Text = txt.PadLeft(label3.Text.Length + 4);
                gen.gen_selected = Generadores.generadores.Linksys_DLINK_PureGenkeys2;
                Nombre.Text = "01/01/2010";
                MAC.Text = DateTime.Now.Date.ToString("d/MM/yyyy");

            }
            else if ((string)tipored.SelectedItem == "TP-LINK_XXXXXX")
            {
                string txt = "Fecha final:";
                label3.Text = txt.PadLeft(label3.Text.Length+4) ;
                gen.gen_selected = Generadores.generadores.TPLINK_PureGenKeys2;
                Nombre.Text = "01/01/2010";
                MAC.Text = DateTime.Now.Date.ToString("d/MM/yyyy");

            }
            else if ((string)tipored.SelectedItem == "OrangeXXXX")
            {
                gen.gen_selected = Generadores.generadores.OrangeXXXX;
                Nombre.Text = labelOrangeXXXX;
                MAC.Text = "XX:XX:XX:XX:XX:XX";
            }
            else if ((string)tipored.SelectedItem == "VodafoneXXXX(ES)")
            {
                gen.gen_selected = Generadores.generadores.EasyBox_es;
                Nombre.Text = "VodafoneXXXX";
                MAC.Text = "XX:XX:XX:XX:XX:XX";
            }
            else if ((string)tipored.SelectedItem == "VodafoneXXXX(DE)")
            {
                gen.gen_selected = Generadores.generadores.EasyBox_de;
                Nombre.Text = "VodafoneXXXX";
                MAC.Text = "XX:XX:XX:XX:XX:XX";
            }
            else if ((string)tipored.SelectedItem == "Belkin.XXXX" || (string)tipored.SelectedItem == "Belkin_XXXXXX" || (string)tipored.SelectedItem == "belkin.xxxx" || (string)tipored.SelectedItem == "belkin.xxx")
            {
                chkAllKeys.Visible = true;
                gen.gen_selected = Generadores.generadores.Belkin4xx;
                Nombre.Text = (string)tipored.SelectedItem;
                MAC.Text = "XX:XX:XX:XX:XX:XX";
            }

        }
       
        private void WEP_CheckedChanged(object sender, EventArgs e)
        {
            if (WEP.Checked == true) opc = Generadores.opcio.WEP;
            else opc =  Generadores.opcio.WPA;

        }



        private void archivo_TextClick(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text File|*.txt";
            saveFileDialog1.Title = "Guarda el diccionario";
            saveFileDialog1.ShowDialog();
            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                archivo.Text = saveFileDialog1.FileName;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (swifi_keygen.AboutBox1 about = new swifi_keygen.AboutBox1())
            {
                about.ShowDialog();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            #if !DEBUG           //en el debug no hace falta que de por culo el actualizador!
            backgroundWorker1.RunWorkerAsync();
            #endif
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            worker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            worker.DoWork -= new DoWorkEventHandler(backgroundWorker1_DoWork);
            worker.Dispose();

        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            WebClient webClient = new WebClient();
            byte[] Archivo=null;
            try
            {
                Archivo = webClient.DownloadData(new Uri("http://www.bitsdelocos.es/SW/SWifi_Keygen.LASTEST"));
            }
            catch {
                //MessageBox.Show("Cagada");
            }
            if (Archivo != null) {
                String contenido = System.Text.Encoding.UTF8.GetString(Archivo);
                String[] version=contenido.Split('.');
                String[] versionBinario = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');

                int i,numVersion,numVersionBinario;
                for (i = 0; i < version.Length; i++) {

                    numVersion = int.Parse(version[i]);
                    numVersionBinario = int.Parse(versionBinario[i]);

                    if (numVersion > numVersionBinario) {

                        using (swifi_keygen.newUpdate upt = new swifi_keygen.newUpdate(contenido))
                        {
                            upt.ShowDialog();
                        }
                    }
                }
            }
        } 
    }
}
