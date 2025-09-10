using System;

namespace ActividadPractica.Domain
{
    public class FormaPago
    {
        public int IdFormaPago { get; set; }
        public string Nombre { get; set; }

        public override string ToString()
        {
            return IdFormaPago + " - " + Nombre;
        }
    }
}
