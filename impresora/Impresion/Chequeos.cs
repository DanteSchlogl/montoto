using System;

namespace Impresion
{
    // Anota las verificaciones y muestra el resultado al final.
    class Chequeos
    {
        private static int bien;
        private static int mal;

        public static void Mirar(string que, bool resultado)
        {
            if (resultado)
            {
                bien = bien + 1;
                Console.WriteLine("bien   " + que);
            }
            else
            {
                mal = mal + 1;
                Console.WriteLine("mal    " + que);
            }
        }

        public static void Mostrar()
        {
            Console.WriteLine();
            Console.WriteLine("Resultado: " + bien + " bien y " + mal + " mal");
        }
    }
}
