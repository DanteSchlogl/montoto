using System;

namespace ControlBateria
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CONTROL DE BATERIA");
            Console.WriteLine();

            Bateria bateria = new Bateria(40, false);

            PantallaBateria pantalla = new PantallaBateria();
            RegistroBateria registro = new RegistroBateria();

            // Los dos escuchan el mismo evento.
            bateria.Cambio += pantalla.Mostrar;
            bateria.Cambio += registro.Anotar;

            // La notebook esta con bateria y se va descargando.
            bateria.Cargar(30);

            // La enchufo.
            bateria.Enchufar();

            // Va cargando.
            bateria.Cargar(65);
            bateria.Cargar(100);

            // La desenchufo y empieza a bajar.
            bateria.Desenchufar();
            bateria.Cargar(85);

            Console.WriteLine();
            Console.WriteLine("Fin de la prueba. Revisar el registro.");
        }
    }
}
