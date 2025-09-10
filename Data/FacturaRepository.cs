using ActividadPractica.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ActividadPractica.Data.Utility;
using ActividadPractica.Data.Util;

namespace ActividadPractica.Data
{
    public class FacturaRepository : IFacturaRepository
    {
        public List<Factura> GetAll()
        {
            List<Factura> lst = new List<Factura>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("sp_Facturas_Listar");

            foreach (DataRow row in dt.Rows)
            {
                Factura f = new Factura
                {
                    IdFactura = (int)row["id_nroFactura"],
                    Fecha = (DateTime)row["fecha"],
                    Cliente = (string)row["cliente"],
                    FormaPago = new FormaPago
                    {
                        Nombre = row["FormaPago"].ToString(),
                    }
                };
                lst.Add(f);
            }
            return lst;
        }

        public Factura GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(Factura factura)
        {
            bool result = true;
            SqlConnection cnn = null;
            SqlTransaction t = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();

                using (var cmd = new SqlCommand("sp_Factura_Insertar", cnn, t))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fecha", factura.Fecha);
                    cmd.Parameters.AddWithValue("@id_formaPago", factura.FormaPago.IdFormaPago);
                    cmd.Parameters.AddWithValue("@cliente", factura.Cliente);

                    SqlParameter paramOut = new SqlParameter("@id_nroFactura", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(paramOut);

                    cmd.ExecuteNonQuery();
                    int idFactura = (int)paramOut.Value;

                    foreach (var detalle in factura.Detalles)
                    {
                        using (var cmdDetalle = new SqlCommand("sp_DetalleFactura_Insertar", cnn, t))
                        {
                            cmdDetalle.CommandType = CommandType.StoredProcedure;

                            cmdDetalle.Parameters.AddWithValue("@id_nroFactura", idFactura);
                            cmdDetalle.Parameters.AddWithValue("@id_articulo", detalle.Articulo.Codigo);
                            cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);

                            cmdDetalle.ExecuteNonQuery();
                        }
                    }
                }

                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null)
                    t.Rollback();

                result = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            return result;
        }
    }
}
