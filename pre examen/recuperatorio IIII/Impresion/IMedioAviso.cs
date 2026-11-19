namespace Impresion
{
    // Cada medio de aviso sabe avisar a su manera.
    interface IMedioAviso
    {
        void Avisar(string dueno, string archivo);
    }
}
