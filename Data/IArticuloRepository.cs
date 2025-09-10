using ActividadPractica.Domain;
using System.Collections.Generic;

namespace ActividadPractica.Data
{
    public interface IArticuloRepository
    {
        List<Articulo> GetAll();
        Articulo GetById(int id);
        bool Save(Articulo articulo);
        bool Delete(int id);
    }
}
