using ActividadPractica.Data;
using ActividadPractica.Domain;
using System.Collections.Generic;

namespace ActividadPractica.Services
{
    public class ArticuloService
    {
        private IArticuloRepository _repository;

        public ArticuloService()
        {
            _repository = new ArticuloRepository();
        }

        public List<Articulo> GetArticulos()
        {
            return _repository.GetAll();
        }

        public bool SaveArticulo(Articulo articulo)
        {
            return _repository.Save(articulo);
        }
    }
}
