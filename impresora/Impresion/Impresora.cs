using System.Collections.Generic;
using System.Threading;

namespace Impresion
{
    // La impresora de la oficina. Es una sola, por eso el Singleton.
    // Guarda la cola y la mueve con Monitor.
    class Impresora
    {
        private static Impresora unica;
        private static readonly object candadoEstatico = new object();

        private readonly List<TrabajoImpresion> cola = new List<TrabajoImpresion>();
        private readonly object candado = new object();
        private readonly int capacidad;
        private readonly CentralDeAvisos central;

        private int recibidos;
        private int impresos;
        private int llenoMaximo;
        private int anterior;
        private bool ordenado = true;

        private Impresora(int capacidad, CentralDeAvisos central)
        {
            this.capacidad = capacidad;
            this.central = central;
        }

        // Siempre devuelve la misma: si todavia no existe, la crea.
        public static Impresora Unica(int capacidad, CentralDeAvisos central)
        {
            lock (candadoEstatico)
            {
                if (unica == null)
                {
                    unica = new Impresora(capacidad, central);
                }
                return unica;
            }
        }

        public int Recibidos
        {
            get { return recibidos; }
        }

        public int Impresos
        {
            get { return impresos; }
        }

        public int LlenoMaximo
        {
            get { return llenoMaximo; }
        }

        public bool Ordenado
        {
            get { return ordenado; }
        }

        public int Avisos
        {
            get { return central.Avisos; }
        }

        // Lo usa un usuario. Si no hay lugar, se queda esperando.
        public void Mandar(string dueno, string archivo)
        {
            bool tomado = false;
            try
            {
                Monitor.Enter(candado, ref tomado);

                while (cola.Count == capacidad)
                {
                    Program.Escribir(dueno + " espera, la cola esta llena " + Cola());
                    Monitor.Wait(candado);
                }

                recibidos = recibidos + 1;
                cola.Add(new TrabajoImpresion(recibidos, dueno, archivo));

                if (cola.Count > llenoMaximo)
                {
                    llenoMaximo = cola.Count;
                }

                Program.Escribir(dueno + " manda " + archivo + " " + Cola());
                Monitor.PulseAll(candado);
            }
            finally
            {
                if (tomado) Monitor.Exit(candado);
            }
        }

        // Saca el primero de la cola. Si esta vacia, espera.
        public TrabajoImpresion Siguiente()
        {
            bool tomado = false;
            try
            {
                Monitor.Enter(candado, ref tomado);

                while (cola.Count == 0)
                {
                    Monitor.Wait(candado);
                }

                TrabajoImpresion t = cola[0];
                cola.RemoveAt(0);   // el primero que llego es el primero que sale
                Monitor.PulseAll(candado);
                return t;
            }
            finally
            {
                if (tomado) Monitor.Exit(candado);
            }
        }

        public void Imprimir(TrabajoImpresion t)
        {
            if (t == null)
            {
                throw new ImpresionException("No hay nada para imprimir.");
            }

            Program.Escribir("Imprimiendo " + t);
            Thread.Sleep(600);

            if (t.Numero < anterior)
            {
                ordenado = false;
            }
            anterior = t.Numero;
            impresos = impresos + 1;

            Program.Escribir("Termine " + t);
            central.AvisarTodos(t.Dueno, t.Archivo);
        }

        // El hilo de la impresora: saca trabajos hasta que llega el aviso de fin.
        public void Trabajar()
        {
            while (true)
            {
                TrabajoImpresion t = Siguiente();
                if (t.Numero == -1)
                {
                    break;
                }
                Imprimir(t);
            }
            Program.Escribir("Se apaga la impresora");
        }

        // Mete un trabajo con numero -1: quiere decir que no hay mas nada.
        public void Terminar()
        {
            bool tomado = false;
            try
            {
                Monitor.Enter(candado, ref tomado);

                while (cola.Count == capacidad)
                {
                    Monitor.Wait(candado);
                }

                cola.Add(new TrabajoImpresion(-1, "sistema", "fin"));
                Monitor.PulseAll(candado);
            }
            finally
            {
                if (tomado) Monitor.Exit(candado);
            }
        }

        // Se llama con el candado tomado.
        private string Cola()
        {
            string dibujo = "";
            int i = 0;
            while (i < capacidad)
            {
                if (i < cola.Count)
                {
                    dibujo = dibujo + "#";
                }
                else
                {
                    dibujo = dibujo + ".";
                }
                i = i + 1;
            }
            return "[" + dibujo + "]";
        }
    }
}
