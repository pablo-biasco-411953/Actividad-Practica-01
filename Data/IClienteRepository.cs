using ActividadPractica.Domain;
using System.Collections.Generic;

namespace ActividadPractica.Data
{
    public interface IClienteRepository
    {
        List<Cliente> GetAll();
        Cliente GetById(int id);
        bool Save(Cliente cliente);
        bool Delete(int id);
    }
}
