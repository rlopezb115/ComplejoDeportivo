namespace servicio.api.ADO.conexion
{
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public interface IConexionSQLServer
    {
        Task<SqlConnection> ObtenerConexionDbAsync();

        SqlConnection ObtenerConexionDb();
    }
}
