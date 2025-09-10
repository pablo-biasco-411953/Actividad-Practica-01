using System;
using System.Collections.Generic;

namespace ActividadPractica.Domain
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; } 
        public FormaPago FormaPago { get; set; }
        public List<FacturaDetalle> Detalles { get; set; }

        public Factura()
        {
            Detalles = new List<FacturaDetalle>();
        }

        public double CalcularTotal()
        {
            double total = 0;
            foreach (var d in Detalles)
                total += d.Subtotal;
            return total;
        }

        public override string ToString()
        {
            return $"Factura N° {IdFactura} - {Fecha.ToShortDateString()} - Cliente: {Cliente}";
        }
    }
}
