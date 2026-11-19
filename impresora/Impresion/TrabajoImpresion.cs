namespace Impresion
{
    // Un trabajo para imprimir: numero, quien lo manda y que archivo es.
    class TrabajoImpresion
    {
        public int Numero;
        public string Dueno;
        public string Archivo;

        public TrabajoImpresion(int numero, string dueno, string archivo)
        {
            Numero = numero;
            Dueno = dueno;
            Archivo = archivo;
        }

        public override string ToString()
        {
            return Numero + " - " + Archivo + " de " + Dueno;
        }
    }
}
