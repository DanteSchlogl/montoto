using System;
using System.IO;

namespace ControlBateria
{
    // Anota cada cambio en un archivo de texto, uno por dia.
    public class RegistroBateria
    {
        public void Anotar(object? origen, EstadoBateriaEventArgs datos)
        {
            try
            {
                // La rotacion es automatica: la fecha va en el nombre.
                string archivo = "registro_bateria_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                string estado;
                if (datos.Enchufada)
                {
                    estado = "ENCHUFADA";
                }
                else
                {
                    estado = "CON BATERIA";
                }

                string detalle;
                if (datos.Enchufada && datos.Porcentaje >= 100)
                {
                    detalle = "completa";
                }
                else
                {
                    detalle = datos.Minutos + " min";
                }

                string linea = "[" + DateTime.Now.ToString("HH:mm:ss") + "] " +
                               datos.Motivo + " | " + estado +
                               " | Carga: " + datos.Porcentaje + "%" +
                               " | " + detalle;

                File.AppendAllText(archivo, linea + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo anotar en el registro: " + ex.Message);
            }
        }
    }
}
