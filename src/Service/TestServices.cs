using System.Data.SqlClient;
using System.Reflection.Metadata;
using Common;

namespace Service
{
    public class TestServices
    {
        // Usamos la clase testServicios para testiar la conexion con la base de datos 
        public static void TestConnection () 
        {
            //para hacer cadena con sql nesecitamos sqlConnection el cual tenemos que decargar con nuggets System.Data.SqlClient
            // Using al finalizar el codigo que desarollemos de sql dentro este se liberara y cerrara la base de datos 

            //try catch para testiar la conexion, en caso de que falle se mostrara una exepcion
            try
            {
                using (var context = new SqlConnection(Parameters.ConnectionString))
                {
                    context.Open ();
                    Console.WriteLine("Sql connection successful");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sql Server : {ex.Message}" );
            }

        }

    }
}