namespace servicio.api.ADO.servicio
{
    using ADO.conexion;
    using ADO.entidades;
    using ADO.contrato;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class RepositorioJefe : IRepositorioJefe
    {
        private readonly IConexionSQLServer _conexionDb;

        public RepositorioJefe(IConexionSQLServer conexionDb)
        {
            _conexionDb = conexionDb;
        }

        public async Task<List<Jefe>> ObtenerListadoAsync()
        {
            var response = new List<Jefe>();
            using (SqlConnection conexion = _conexionDb.ObtenerConexionDb())
            {
                using (SqlCommand consulta = new SqlCommand("spObtenerJefes", conexion))
                {
                    consulta.CommandType = CommandType.StoredProcedure;
                    await conexion.OpenAsync();

                    using (var reader = await consulta.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapEntidad(reader));
                        }
                    }
                }
            }

            return response;
        }

        private Jefe MapEntidad(SqlDataReader data)
        {
            return new Jefe()
            {
                JefeId = (int)data["JefeId"],
                Nombre = data["Nombre"].ToString()
            };
        }
    }
}
