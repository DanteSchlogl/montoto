using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Impresion
{
    class Program
    {
        private static readonly Stopwatch reloj = Stopwatch.StartNew();

        public static void Escribir(string texto)
        {
            Console.WriteLine(reloj.Elapsed.TotalSeconds.ToString("0.00") + "  " + texto);
        }

        static void Main()
        {
            const int LUGARES = 4;
            const int CANTIDAD_USUARIOS = 3;
            const int POR_USUARIO = 5;

            CentralDeAvisos central = new CentralDeAvisos();
            central.Sumar(new AvisoPorSms());
            central.Sumar(new AvisoPorWhatsApp());
            central.Sumar(new AvisoPorEmail());

            Impresora impresora = Impresora.Unica(LUGARES, central);

            List<Thread> gente = new List<Thread>();
            for (int i = 1; i <= CANTIDAD_USUARIOS; i++)
            {
                Usuario u = new Usuario("Usuario " + i, impresora, POR_USUARIO, i * 7);
                gente.Add(new Thread(u.Ejecutar));
            }

            Thread hiloImpresora = new Thread(impresora.Trabajar);
            hiloImpresora.Start();
            foreach (Thread h in gente) h.Start();

            foreach (Thread h in gente) h.Join();
            impresora.Terminar();
            hiloImpresora.Join();

            int esperados = CANTIDAD_USUARIOS * POR_USUARIO;

            Console.WriteLine();
            Console.WriteLine("Mandan: " + impresora.Recibidos + " | Imprimen: " + impresora.Impresos);
            Console.WriteLine("Avisos: " + impresora.Avisos + " en " + central.Medios + " medios");

            Chequeos.Mirar("llegaron todos los trabajos", impresora.Recibidos == esperados);
            Chequeos.Mirar("se imprimieron todos", impresora.Impresos == esperados);
            Chequeos.Mirar("salieron en el orden que llegaron", impresora.Ordenado);
            Chequeos.Mirar("nunca se paso de la capacidad", impresora.LlenoMaximo <= LUGARES);
            Chequeos.Mirar("se aviso por los tres medios", impresora.Avisos == esperados * 3);
            Chequeos.Mirar("la impresora es una sola", ReferenceEquals(impresora, Impresora.Unica(LUGARES, central)));

            // Probamos que pasa si se quiere imprimir cuando no hay nada.
            try
            {
                impresora.Imprimir(null);
                Chequeos.Mirar("avisa cuando no hay trabajo", false);
            }
            catch (ImpresionException)
            {
                Chequeos.Mirar("avisa cuando no hay trabajo", true);
            }

            Chequeos.Mostrar();
        }
    }
}
