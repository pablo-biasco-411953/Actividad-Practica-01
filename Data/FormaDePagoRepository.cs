using ActividadPractica.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ActividadPractica.Data.Utility;
using ActividadPractica.Data.Util;

namespace ActividadPractica.Data
{
    public class FormaPagoRepository : IFormaPagoRepository
    {
        public List<FormaPago> GetAll()
        {
            List<FormaPago> lst = new List<FormaPago>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_Recuperar_FormasPago");

            foreach (DataRow row in dt.Rows)
            {
                FormaPago f = new FormaPago
                {
                    IdFormaPago = (int)row["id_formaPago"],
                    Nombre = (string)row["nombre"]
                };
                lst.Add(f);
            }
            return lst;
        }

        public bool Save(FormaPago formaPago)
        {
            var parametros = new List<Parameter>
    {
        new Parameter("@nombre", formaPago.Nombre)
    };

            int filas = DataHelper.GetInstance().ExecuteSPNonQuery("sp_FormaPago_Insertar", parametros);

            return filas > 0;
        }

        public bool Delete(int id)
        {
            var parametros = new List<Parameter>
    {
        new Parameter("@id_formaPago", id)
    };

            int filas = DataHelper.GetInstance().ExecuteSPNonQuery("sp_FormaPago_Eliminar", parametros);

            return filas > 0;
        }

        public FormaPago GetById(int id)
        {
            var dt = DataHelper.GetInstance().ExecuteSPQuery(
                "sp_FormaPago_Consultar",
                new Microsoft.Data.SqlClient.SqlParameter("@id_formaPago", id)
            );

            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];
            return new FormaPago
            {
                IdFormaPago = (int)row["id_formaPago"],
                Nombre = (string)row["nombre"]
            };
        }

    }
}
