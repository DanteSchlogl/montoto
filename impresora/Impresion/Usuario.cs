using System;
using System.Threading;

namespace Impresion
{
    // Cada usuario es un hilo que manda archivos para imprimir.
    class Usuario
    {
        private static readonly string[] Archivos =
            { "Presupuesto", "Remito", "Listado", "Carta", "Recibo" };

        private readonly string nombre;
        private readonly Impresora impresora;
        private readonly int cantidad;
        private readonly Random azar;

        public Usuario(string nombre, Impresora impresora, int cantidad, int semilla)
        {
            this.nombre = nombre;
            this.impresora = impresora;
            this.cantidad = cantidad;
            azar = new Random(semilla);
        }

        public void Ejecutar()
        {
            for (int i = 1; i <= cantidad; i++)
            {
                // Espera un ratito antes de mandar el siguiente.
                Thread.Sleep(azar.Next(250, 650));

                string archivo = Archivos[(i - 1) % Archivos.Length];
                impresora.Mandar(nombre, archivo);
            }
            Program.Escribir(nombre + " ya mando todo");
        }
    }
}
