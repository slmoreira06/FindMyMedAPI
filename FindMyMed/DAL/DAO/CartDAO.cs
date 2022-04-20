using FindMyMed.DTO;
using FindMyMed.Models;
using Microsoft.Data.SqlClient;

namespace FindMyMed.DAL
{
    public class CartDAO : ICartsRepository
    {
        String connect = "Server=tcp:test-sql-lesipl-pds.database.windows.net,1433;Initial Catalog=FindMyMed_db;Persist Security Info=False;User ID=Ipca_Server;Password=Soueu1999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public UpdateCartDTO SaveCart(UpdateCartDTO cartDTO)
        {
            String queryString = $"UPDATE dbo.Carts SET PaymentMethod=@PaymentMethod, TotalPrice=@TotalPrice, UserPoints=@UsedPoints, Status=@Status, OrderId=@OrderId WHERE Id = 1";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@PaymentMethod", System.Data.SqlDbType.NVarChar).Value = cartDTO.PaymentMethod;
                    sqlCommand.Parameters.Add("@TotalPrice", System.Data.SqlDbType.Float).Value = cartDTO.TotalPrice;
                    sqlCommand.Parameters.Add("@UsedPoints", System.Data.SqlDbType.Int).Value = cartDTO.UsedPoints;
                    sqlCommand.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar).Value = cartDTO.Status;
                    sqlCommand.Parameters.Add("@OrderId", System.Data.SqlDbType.Int).Value = cartDTO.OrderId;

                    try
                    {
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    };
                }
            }
            return cartDTO;
        }
    }
}


