namespace ActividadPractica.Domain
{
    public class FacturaDetalle
    {
        public int IdDetalle { get; set; }
        public Articulo Articulo{ get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }

        public double Subtotal => Cantidad * PrecioUnitario;

        public override string ToString()
        {
            return $"{Articulo?.Nombre} x{Cantidad} = {Subtotal:C}";
        }
    }
}
