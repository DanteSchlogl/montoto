using System;

namespace Impresion
{
    class AvisoPorSms : IMedioAviso
    {
        public void Avisar(string dueno, string archivo)
        {
            Console.WriteLine("   SMS a " + dueno + ": listo " + archivo);
        }
    }
}
