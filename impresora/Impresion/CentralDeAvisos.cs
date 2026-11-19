using System.Collections.Generic;

namespace Impresion
{
    // Guarda los medios y avisa por todos.
    // La impresora le habla a esta clase, no a los medios.
    class CentralDeAvisos
    {
        private readonly List<IMedioAviso> medios = new List<IMedioAviso>();

        public int Avisos { get; set; }

        public int Medios
        {
            get { return medios.Count; }
        }

        public void Sumar(IMedioAviso medio)
        {
            medios.Add(medio);
        }

        public void AvisarTodos(string dueno, string archivo)
        {
            foreach (IMedioAviso medio in medios)
            {
                medio.Avisar(dueno, archivo);
                Avisos = Avisos + 1;
            }
        }
    }
}
