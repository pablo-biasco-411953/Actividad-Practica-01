using ActividadPractica.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ActividadPractica.Data.Utility;
using ActividadPractica.Data.Util;

namespace ActividadPractica.Data
{
    public class ArticuloRepository : IArticuloRepository
    {
        public List<Articulo> GetAll()
        {
            List<Articulo> lst = new List<Articulo>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_Recuperar_Articulos");

            foreach (DataRow row in dt.Rows)
            {
                Articulo a = new Articulo
                {
                    Codigo = (int)row["id_articulo"],
                    Nombre = (string)row["nombre"],
                    Precio = Convert.ToDouble(row["precioUnitario"]),

                };
                lst.Add(a);
            }
            return lst;
        }

        public Articulo GetById(int id)
        {
            var dt = DataHelper.GetInstance().ExecuteSPQuery(
                "sp_Articulo_Consultar",
                new SqlParameter("@id_articulo", id)
            );

            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];
            return new Articulo
            {
                Codigo = (int)row["id_articulo"],
                Nombre = (string)row["nombre"],
                Precio = Convert.ToDouble(row["precioUnitario"]),

            };
        }

        public bool Save(Articulo articulo)
        {
            var parametros = new List<Parameter>
    {
        new Parameter("@nombre", articulo.Nombre),
        new Parameter("@precioUnitario", articulo.Precio),

    };

            int filas = DataHelper.GetInstance().ExecuteSPNonQuery("sp_Articulo_Insertar", parametros);

            return filas > 0;
        }

        public bool Delete(int id)
        {
            var parametros = new List<Parameter>
    {
        new Parameter("@id_articulo", id)
    };

            int filas = DataHelper.GetInstance().ExecuteSPNonQuery("sp_Articulo_Eliminar", parametros);

            return filas > 0;
        }
    }
}
