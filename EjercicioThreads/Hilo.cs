using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace EjercicioThreads
{
    public class Hilo
    {
        public Hilo(int milisegundos, ProgressBar progressBar)
        {
            this.hilo = new Thread(new ThreadStart(ActualizaProgressBar));
            this.milisegundos = milisegundos;
            this.progressBar = progressBar;
        }

        public void Inicia()
        {
            hilo.Start();
        }

        public void Termina()
        {
            hilo.Abort();
        }

        private void ActualizaProgressBar()
        {
            while (true)
            {
                MethodInvoker m = new MethodInvoker(() => 
                {
                    int valor = progressBar.Value;
                    if (valor == 100)
                    {
                        valor = 0;
                    }
                    else
                    {
                        ++valor;
                    }
                    progressBar.Value = valor;
                    Thread.Sleep(milisegundos);
                });
                if (progressBar.InvokeRequired)
                {
                    progressBar.Invoke(m);
                }
                else
                {
                    m.Invoke();
                }
            }
        }

        private Thread hilo;
        private int milisegundos;
        private ProgressBar progressBar;
    }
}
