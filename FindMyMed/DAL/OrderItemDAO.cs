using FindMyMed.DTO;
using FindMyMed.Models;
using Microsoft.Data.SqlClient;

namespace FindMyMed.DAL
{
    public class OrderItemDAO : IOrderItemsRepository
    {
        String connect = "Server=tcp:test-sql-lesipl-pds.database.windows.net,1433;Initial Catalog=FindMyMed_db;Persist Security Info=False;User ID=Ipca_Server;Password=Soueu1999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public bool CreateOrderItem(OrderItem orderItem)
        {
            bool success = false;
            String queryString = $"INSERT INTO dbo.OrderItems (Quantity, Reference) VALUES (@Quantity, @Reference)";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Quantity", System.Data.SqlDbType.Int).Value = orderItem.Quantity;
                    sqlCommand.Parameters.Add("@Reference", System.Data.SqlDbType.NVarChar).Value = orderItem.Reference;

                    try
                    {
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                        success = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    };
                }
            }
            return success;
        }

        public IEnumerable<OrderItem> GetOrderItems()
        {
            List<OrderItem> orders = new List<OrderItem>();
            string sqlStatement = $"SELECT Id, Quantity, Reference FROM dbo.OrderItems";
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
                        Order order = new Order();
                        order.Id = reader.GetFieldValue<int>(0);
                        order.CreationDate = reader.GetFieldValue<DateTime>(1);
                        order.TotalPrice = reader.GetFieldValue<float>(2);
                        order.Status = Enum.Parse<OrderStatus>(reader.GetFieldValue<string>(3));
                        orders.Add(order);
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
            Order order = new Order();
            string sqlStatement = $"SELECT Id, CreationDate, TotalPrice, Status FROM dbo.Orders WHERE Id = {id}";
            using (SqlConnection connection = new SqlConnection(connect))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        order.Id = reader.GetFieldValue<int>(0);
                        order.CreationDate = reader.GetFieldValue<DateTime>(1);
                        order.TotalPrice = reader.GetFieldValue<float>(2);
                        order.Status = Enum.Parse<OrderStatus>(reader.GetFieldValue<string>(3));
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
            return order;
        }

        public UpdateOrderItemDTO UpdateOrderItem(int id, UpdateOrderItemDTO orderItemDTO)
        {
            String queryString = $"UPDATE dbo.Orders SET CreationDate=@CreationDate, TotalPrice=@TotalPrice, Status=@Status WHERE Id = {id}";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@CreationDate", System.Data.SqlDbType.DateTime).Value = orderDTO.CreationDate;
                    sqlCommand.Parameters.Add("@TotalPrice", System.Data.SqlDbType.Float).Value = orderDTO.TotalPrice;
                    sqlCommand.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar).Value = orderDTO.Status;

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
            return orderDTO;
        }
    }
}

