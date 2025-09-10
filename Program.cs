using ActividadPractica.Domain;
using ActividadPractica.Services;

ArticuloService articuloService = new ArticuloService();
FormaPagoService formaPagoService = new FormaPagoService();
FacturaService facturaService = new FacturaService();

Console.WriteLine("Insertando Formas de Pago...");
formaPagoService.SaveFormaPago(new FormaPago { Nombre = "Efectivo" });
formaPagoService.SaveFormaPago(new FormaPago { Nombre = "Tarjeta" });

Console.WriteLine("Insertando Artículos...");
articuloService.SaveArticulo(new Articulo { Nombre = "Laptop", Precio = 1200 });
articuloService.SaveArticulo(new Articulo { Nombre = "Mouse", Precio = 25 });


Console.WriteLine("Creando Factura...");
Factura factura = new Factura
{
    Fecha = DateTime.Now,
    Cliente = "Pablo",
    FormaPago = new FormaPago { IdFormaPago = 1 },
    Detalles = new List<FacturaDetalle>
    {
        new FacturaDetalle
        {
            Articulo = new Articulo { Codigo = 1 }, 
            Cantidad = 2,
            PrecioUnitario = 1200
        },
        new FacturaDetalle
        {
            Articulo = new Articulo { Codigo = 2 }, 
            Cantidad = 3,
            PrecioUnitario = 25
        }
    }
};
facturaService.SaveFactura(factura);
Console.WriteLine("\n=== Listado de Facturas ===");
List<Factura> facturas = facturaService.GetFacturas();
foreach (var f in facturas)
{
    Console.WriteLine(f);
    foreach (var d in f.Detalles)
        Console.WriteLine("   " + d);
}
