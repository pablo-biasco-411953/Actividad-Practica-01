using ActividadPractica.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using ActividadPractica.Data.Utility;

namespace ActividadPractica.Data
{
    public class ClienteRepository : IClienteRepository
    {
        public List<Cliente> GetAll()
        {
            List<Cliente> lst = new List<Cliente>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_Recuperar_Clientes");

            foreach (DataRow row in dt.Rows)
            {
                Cliente c = new Cliente
                {
                    IdCliente = (int)row["id_cliente"],
                    Nombre = (string)row["nombre"],
                    Apellido = (string)row["apellido"],
                    Email = (string)row["email"]
                };
                lst.Add(c);
            }
            return lst;
        }

        public Cliente GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
