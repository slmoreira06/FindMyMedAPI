using FindMyMed.DTO;
using FindMyMed.Models;
using Microsoft.Data.SqlClient;

namespace FindMyMed.DAL
{
    public class OrderItemDAO : IOrderItemsRepository
    {
        String connect = "Server=tcp:test-sql-lesipl-pds.database.windows.net,1433;Initial Catalog=FindMyMed_db;Persist Security Info=False;User ID=Ipca_Server;Password=Soueu1999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

          public IEnumerable<OrderItem> GetOrderItemsByOrder(int id)
        {
            List<OrderItem> orders = new List<OrderItem>();
            string sqlStatement = $"SELECT Id, Quantity, Reference, ProductId FROM dbo.OrderItems WHERE OrderId = {id}";
            using (SqlConnection connection = new SqlConnection(connect))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    // Call Read before accessing data.
                    while (reader.Read())
                    {
                        OrderItem orderItem = new OrderItem();
                        orderItem.Id = reader.GetFieldValue<int>(0);
                        orderItem.Quantity = reader.GetFieldValue<int>(1);
                        orderItem.Reference = reader.GetFieldValue<string>(2);
                        orderItem.ProductId = reader.GetFieldValue<int>(3);
                        orders.Add(orderItem);
                    }
                    // Call Close when done reading.
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return orders;
        }

        public OrderItem GetOrderItemById(int id)
        {
            OrderItem orderItem = new OrderItem();
            string sqlStatement = $"SELECT Id, Quantity, Reference, OrderId, ProductId FROM dbo.OrderItems WHERE Id = {id}";
            using (SqlConnection connection = new SqlConnection(connect))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        orderItem.Id = reader.GetFieldValue<int>(0);
                        orderItem.Quantity = reader.GetFieldValue<int>(1);
                        orderItem.Reference = reader.GetFieldValue<string>(2);
                        orderItem.OrderId = reader.GetFieldValue<int>(3);
                        orderItem.ProductId = reader.GetFieldValue<int>(4);
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return orderItem;
        }

        public UpdateOrderItemDTO UpdateOrderItem(int id, UpdateOrderItemDTO orderItemDTO)
        {
            String queryString = $"UPDATE dbo.OrderItems SET Quantity=@Quantity WHERE Id = {id}";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Quantity", System.Data.SqlDbType.Int).Value = orderItemDTO.Quantity;
            
                    try
                    {
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            return orderItemDTO;
        }
    }
}

