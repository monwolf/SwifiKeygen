using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace Generadores
{
    public abstract class IGeneradores:BackgroundWorker,IDisposable
    {

        internal Filter filtro;
        internal GenOption opciones;
        internal int MaxProgres;
        internal IGeneradores() {
            MaxProgres = 0;
            filtro = new Filter();
            opciones = new GenOption();
            DoWork += IGeneradores_DoWork;
        }

        private void IGeneradores_DoWork(object sender, DoWorkEventArgs e)
        {
            generaDicc();            
        }
        
        internal IGeneradores(GenOption _opcions)
        {
            MaxProgres = 0;
            filtro = new Filter();
            opciones = _opcions;
        }
        public abstract bool wlanMatch(string essid, string mac);
        public  Filter getFilter(){
            return filtro;

        }
        internal abstract void generaDicc();

        public void Generate() {


            if (!wlanMatch(opciones.ESSID, opciones.BSSID)) {

                throw new NotSupportedException("Esta red no es compatible con el programa");
            }
            
            if (!IsBusy)
                RunWorkerAsync();
        
        }
        
        public class NotSupportedException : Exception {        
                     public NotSupportedException()
                {
                }

                public NotSupportedException(string message)
                    : base(message)
                {
                }

                public NotSupportedException(string message, Exception inner)
                    : base(message, inner)
                {
                }
                    }
        protected override void Dispose(bool disposing)
        {
            

            base.Dispose(disposing);
        }

    }

    public class Filter {
        public string[] supportedMAC; 
        public string[] supportedSsid;
    }

    public class GenOption {
        public string ESSID { get; set; }
        public string BSSID { get; set; }
        public string Filename { get; set; }
        public string Padding { get; set; }
        public TipoRed TipoRed { get; set; }
    
    }

}
