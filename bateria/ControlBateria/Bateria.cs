using System;

namespace ControlBateria
{
    public class Bateria
    {
        // Evento al que se enganchan la pantalla y el registro.
        public event EventHandler<EstadoBateriaEventArgs>? Cambio;

        private bool enchufada;
        private int porcentaje;
        private int minutos;

        public Bateria(int porcentajeInicial, bool enchufadaInicial)
        {
            porcentaje = porcentajeInicial;
            enchufada = enchufadaInicial;
            minutos = 0;

            if (enchufada)
            {
                minutos = (100 - porcentaje) * 2;
            }
            else
            {
                minutos = porcentaje * 2;
            }
        }

        public bool Enchufada
        {
            get { return enchufada; }
        }

        public int Porcentaje
        {
            get { return porcentaje; }
        }

        public int Minutos
        {
            get { return minutos; }
        }

        // Pongo el cargador: la bateria empieza a cargarse.
        public void Enchufar()
        {
            enchufada = true;
            minutos = (100 - porcentaje) * 2;
            Avisar("Se enchufo el cargador");
        }

        // Saco el cargador: la bateria empieza a descargarse.
        public void Desenchufar()
        {
            enchufada = false;
            minutos = porcentaje * 2;
            Avisar("Se desenchufo la bateria");
        }

        // Cambio la carga. Siempre queda entre 0 y 100.
        public void Cargar(int nuevoPorcentaje)
        {
            if (nuevoPorcentaje < 0)
            {
                nuevoPorcentaje = 0;
            }

            if (nuevoPorcentaje > 100)
            {
                nuevoPorcentaje = 100;
            }

            porcentaje = nuevoPorcentaje;

            if (enchufada)
            {
                minutos = (100 - porcentaje) * 2;
            }
            else
            {
                minutos = porcentaje * 2;
            }

            Avisar("Cambio la carga a " + porcentaje + "%");
        }

        private void Avisar(string motivo)
        {
            EstadoBateriaEventArgs datos =
                new EstadoBateriaEventArgs(enchufada, porcentaje, minutos, motivo);

            // El ?. cubre el caso de que no haya nadie escuchando.
            Cambio?.Invoke(this, datos);
        }
    }
}
