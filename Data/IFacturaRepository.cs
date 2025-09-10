using ActividadPractica.Domain;
using System.Collections.Generic;

namespace ActividadPractica.Data
{
    public interface IFacturaRepository
    {
        List<Factura> GetAll();
        Factura GetById(int id);
        bool Save(Factura factura);
    }
}
