namespace Proyecto_Windows
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Nombre = new System.Windows.Forms.TextBox();
            this.MAC = new System.Windows.Forms.TextBox();
            this.archivo = new System.Windows.Forms.TextBox();
            this.Genera = new System.Windows.Forms.Button();
            this.tipored = new System.Windows.Forms.ComboBox();
            this.WEP = new System.Windows.Forms.RadioButton();
            this.WPA = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.anio = new System.Windows.Forms.CheckBox();
            this.claves = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.chkAllKeys = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Nombre
            // 
            this.Nombre.Enabled = false;
            this.Nombre.Location = new System.Drawing.Point(182, 72);
            this.Nombre.Name = "Nombre";
            this.Nombre.Size = new System.Drawing.Size(107, 20);
            this.Nombre.TabIndex = 2;
            // 
            // MAC
            // 
            this.MAC.Enabled = false;
            this.MAC.Location = new System.Drawing.Point(182, 98);
            this.MAC.Name = "MAC";
            this.MAC.Size = new System.Drawing.Size(107, 20);
            this.MAC.TabIndex = 3;
            // 
            // archivo
            // 
            this.archivo.Enabled = false;
            this.archivo.Location = new System.Drawing.Point(182, 124);
            this.archivo.Name = "archivo";
            this.archivo.Size = new System.Drawing.Size(107, 20);
            this.archivo.TabIndex = 4;
            this.archivo.Text = "diccionario.txt";
            this.archivo.Click += new System.EventHandler(this.archivo_TextClick);
            // 
            // Genera
            // 
            this.Genera.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Genera.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Genera.Enabled = false;
            this.Genera.ForeColor = System.Drawing.Color.Black;
            this.Genera.Location = new System.Drawing.Point(93, 190);
            this.Genera.Name = "Genera";
            this.Genera.Size = new System.Drawing.Size(75, 23);
            this.Genera.TabIndex = 5;
            this.Genera.Text = "&Generar";
            this.Genera.UseVisualStyleBackColor = false;
            this.Genera.Click += new System.EventHandler(this.button1_Click);
            // 
            // tipored
            // 
            this.tipored.FormattingEnabled = true;
            this.tipored.Items.AddRange(new object[] {
            "WLAN_XX",
            "JAZZTEL_XX",
            "ThomsonXXXXXX",
            "SpeedTouchXXXXXX",
            "WlanXXXXXX",
            "WiFiXXXXXX",
            "YaComXXXXXX",
            "ONOXXXX",
            "Dlink",
            "RWlan",
            "Tele2",
            "OrangeXXXXXX",
            "VerizonXXXXX",
            "INFINITUMxxxx",
            "Discus--XXXXXX",
            "JAZZTEL_XXXX",
            "WLAN_XXXX",
            "TP-LINK_XXXXXX",
            "DLINK_XXXXXX",
            "LINKSYS_XXXXXX",
            "OrangeXXXX",
            "SKY_v1",
            "VodafoneXXXX(ES)",
            "VodafoneXXXX(DE)",
            "Belkin.XXXX",
            "Belkin_XXXXXX",
            "belkin.xxxx",
            "belkin.xxx"});
            this.tipored.Location = new System.Drawing.Point(16, 72);
            this.tipored.Name = "tipored";
            this.tipored.Size = new System.Drawing.Size(151, 21);
            this.tipored.TabIndex = 1;
            this.tipored.Text = "Selecciona el tipo de red:";
            this.tipored.SelectedIndexChanged += new System.EventHandler(this.tipored_SelectedIndexChanged);
            // 
            // WEP
            // 
            this.WEP.AutoSize = true;
            this.WEP.BackColor = System.Drawing.Color.Transparent;
            this.WEP.Checked = true;
            this.WEP.Location = new System.Drawing.Point(182, 150);
            this.WEP.Name = "WEP";
            this.WEP.Size = new System.Drawing.Size(48, 17);
            this.WEP.TabIndex = 6;
            this.WEP.TabStop = true;
            this.WEP.Text = "Wep";
            this.WEP.UseVisualStyleBackColor = false;
            this.WEP.Visible = false;
            this.WEP.CheckedChanged += new System.EventHandler(this.WEP_CheckedChanged);
            // 
            // WPA
            // 
            this.WPA.AutoSize = true;
            this.WPA.BackColor = System.Drawing.Color.Transparent;
            this.WPA.Location = new System.Drawing.Point(234, 150);
            this.WPA.Name = "WPA";
            this.WPA.Size = new System.Drawing.Size(48, 17);
            this.WPA.TabIndex = 6;
            this.WPA.Text = "Wpa";
            this.WPA.UseVisualStyleBackColor = false;
            this.WPA.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(65, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Nombre del archivo:";
            // 
            // anio
            // 
            this.anio.AutoSize = true;
            this.anio.BackColor = System.Drawing.Color.Transparent;
            this.anio.Location = new System.Drawing.Point(182, 173);
            this.anio.Name = "anio";
            this.anio.Size = new System.Drawing.Size(130, 17);
            this.anio.TabIndex = 10;
            this.anio.Text = "Usar 2 dígitos del año";
            this.anio.UseVisualStyleBackColor = false;
            // 
            // claves
            // 
            this.claves.AutoSize = true;
            this.claves.BackColor = System.Drawing.Color.Transparent;
            this.claves.Location = new System.Drawing.Point(182, 196);
            this.claves.Name = "claves";
            this.claves.Size = new System.Drawing.Size(98, 17);
            this.claves.TabIndex = 10;
            this.claves.Text = "Claves IX1VPV";
            this.claves.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(12, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "                        Escribe la MAC:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(13, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(215, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Seleccione y rellene los campos necesarios.";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(12, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "&Acerca";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // chkAllKeys
            // 
            this.chkAllKeys.AutoSize = true;
            this.chkAllKeys.BackColor = System.Drawing.Color.Transparent;
            this.chkAllKeys.Location = new System.Drawing.Point(182, 219);
            this.chkAllKeys.Name = "chkAllKeys";
            this.chkAllKeys.Size = new System.Drawing.Size(122, 17);
            this.chkAllKeys.TabIndex = 15;
            this.chkAllKeys.Text = "Todos los algoritmos";
            this.chkAllKeys.UseVisualStyleBackColor = false;
            this.chkAllKeys.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::swifi_keygen.Properties.Resources.Form1Background;
            this.ClientSize = new System.Drawing.Size(307, 240);
            this.Controls.Add(this.chkAllKeys);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.claves);
            this.Controls.Add(this.anio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.WPA);
            this.Controls.Add(this.WEP);
            this.Controls.Add(this.tipored);
            this.Controls.Add(this.Genera);
            this.Controls.Add(this.archivo);
            this.Controls.Add(this.MAC);
            this.Controls.Add(this.Nombre);
            this.ForeColor = System.Drawing.Color.DarkOrange;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SWifi Keygen ";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Nombre;
        private System.Windows.Forms.TextBox MAC;
        private System.Windows.Forms.TextBox archivo;
        private System.Windows.Forms.Button Genera;
        private System.Windows.Forms.ComboBox tipored;
        private System.Windows.Forms.RadioButton WEP;
        private System.Windows.Forms.RadioButton WPA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox anio;
        private System.Windows.Forms.CheckBox claves;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox chkAllKeys;
    }
}

