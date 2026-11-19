using System;

namespace ControlBateria
{
    // Muestra cada cambio de la bateria en la consola.
    public class PantallaBateria
    {
        public void Mostrar(object? origen, EstadoBateriaEventArgs datos)
        {
            string estado;

            if (datos.Enchufada)
            {
                estado = "ENCHUFADA";
            }
            else
            {
                estado = "CON BATERIA";
            }

            Console.WriteLine(datos.Motivo);
            Console.WriteLine("   Estado: " + estado + " | Carga: " + datos.Porcentaje + "%");

            if (datos.Enchufada)
            {
                if (datos.Porcentaje >= 100)
                {
                    Console.WriteLine("   Bateria completa");
                }
                else
                {
                    Console.WriteLine("   Faltan " + datos.Minutos + " minutos para el 100%");
                }
            }
            else
            {
                Console.WriteLine("   Quedan " + datos.Minutos + " minutos de uso");
            }
        }
    }
}
