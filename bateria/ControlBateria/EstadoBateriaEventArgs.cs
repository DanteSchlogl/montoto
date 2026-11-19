using System;

namespace ControlBateria
{
    public class EstadoBateriaEventArgs : EventArgs
    {
        public bool Enchufada { get; }
        public int Porcentaje { get; }
        public int Minutos { get; }
        public string Motivo { get; }

        public EstadoBateriaEventArgs(bool enchufada, int porcentaje, int minutos, string motivo)
        {
            Enchufada = enchufada;
            Porcentaje = porcentaje;
            Minutos = minutos;
            Motivo = motivo;
        }
    }
}
