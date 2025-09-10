using ActividadPractica.Data;
using ActividadPractica.Domain;
using System.Collections.Generic;

namespace ActividadPractica.Services
{
    public class FacturaDetalleService
    {
        private IFacturaRepository _repository;

        public FacturaDetalleService()
        {
            _repository = new FacturaRepository();
        }

        public List<FacturaDetalle> GetDetalles(int idFactura)
        {
            var factura = _repository.GetById(idFactura);
            return factura != null ? factura.Detalles : new List<FacturaDetalle>();
        }

   
        public bool SaveDetalles(Factura factura)
        {
            return _repository.Save(factura);
        }
    }
}
