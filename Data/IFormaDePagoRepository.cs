using ActividadPractica.Domain;
using System.Collections.Generic;

namespace ActividadPractica.Data
{
    public interface IFormaPagoRepository
    {
        List<FormaPago> GetAll();
        FormaPago GetById(int id);
        bool Save(FormaPago formaPago);
        bool Delete(int id);
    }
}
