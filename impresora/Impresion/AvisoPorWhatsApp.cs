using System;

namespace Impresion
{
    class AvisoPorWhatsApp : IMedioAviso
    {
        public void Avisar(string dueno, string archivo)
        {
            Console.WriteLine("   WhatsApp a " + dueno + ": listo " + archivo);
        }
    }
}
