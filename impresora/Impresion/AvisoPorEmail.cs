using System;

namespace Impresion
{
    class AvisoPorEmail : IMedioAviso
    {
        public void Avisar(string dueno, string archivo)
        {
            Console.WriteLine("   Email a " + dueno + ": listo " + archivo);
        }
    }
}
