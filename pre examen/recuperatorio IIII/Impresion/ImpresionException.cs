using System;

namespace Impresion
{
    // Error del sistema de impresion, para no largar los errores de .NET.
    class ImpresionException : Exception
    {
        public ImpresionException(string mensaje) : base(mensaje)
        {
        }
    }
}
