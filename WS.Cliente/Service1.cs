using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WS.Cliente.Interfaces;
using WS.Cliente.Repositorio;

namespace WS.Cliente
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;        

        public Service1()
        {
            InitializeComponent();            
        }

        protected override void OnStart(string[] args)
        {
            // Configure el temporizador            
            timer = new Timer();
            //timer.Interval = 5 * 60 * 1000; // 5 minutos en milisegundos
            timer.Interval = 1 * 60 * 1000; // 1 minuto en milisegundos
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {            
            EjecutarTarea();
        }

        protected override void OnStop()
        {
            timer.Stop();
            timer.Dispose();
        }

        private void EjecutarTarea()
        {
            try
            {
                Repositorio_Utilidades repositorio = new Repositorio_Utilidades();

                
            }
            catch (Exception ex)
            {
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
