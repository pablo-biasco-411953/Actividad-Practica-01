using ActividadPractica.Data;
using ActividadPractica.Domain;
using System;
using System.Collections.Generic;

namespace ActividadPractica.Services
{
    public class FacturaService
    {
        private FacturaRepository _repo;

        public FacturaService()
        {
            _repo = new FacturaRepository();
        }

        public bool SaveFactura(Factura factura)
        {
            return _repo.Save(factura);
        }

        public List<Factura> GetFacturas()
        {
            return _repo.GetAll();
        }

    
    }
}
