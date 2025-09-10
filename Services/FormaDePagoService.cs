using ActividadPractica.Data;
using ActividadPractica.Domain;
using System.Collections.Generic;

namespace ActividadPractica.Services
{
    public class FormaPagoService
    {
        private IFormaPagoRepository _repository;

        public FormaPagoService()
        {
            _repository = new FormaPagoRepository();
        }

        public List<FormaPago> GetFormasPago()
        {
            return _repository.GetAll();
        }

        public bool SaveFormaPago(FormaPago formaPago)
        {
            return _repository.Save(formaPago);
        }
    }
}
