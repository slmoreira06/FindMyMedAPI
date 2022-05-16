using FindMyMed.DTO;
using FindMyMed.Models;
using Microsoft.Data.SqlClient;

namespace FindMyMed.DAL
{
    public class OrderDAO : IOrdersRepository
    {
        String connect = "Server=tcp:test-sql-lesipl-pds.database.windows.net,1433;Initial Catalog=FindMyMed_db;Persist Security Info=False;User ID=Ipca_Server;Password=Soueu1999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public bool CreateOrder(Order order)
        {
            bool success = false;
            int id = 0;
            String queryString = $"INSERT INTO dbo.Orders (CreationDate, Status) VALUES (@CreationDate, @Status); SELECT SCOPE_IDENTITY()";
            String orderItemString = "INSERT INTO dbo.OrderItems (Quantity, OrderId, ProductId, Reference) VALUES (@Quantity, @OrderId, @ProductId, (SELECT Reference FROM dbo.Products WHERE Id = @ProductId))";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@CreationDate", System.Data.SqlDbType.DateTime).Value = order.CreationDate;
                    sqlCommand.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar).Value = order.Status;

                    try
                    {
                        sqlConnection.Open();
                        id = Convert.ToInt32(sqlCommand.ExecuteScalar());
                        sqlConnection.Close();
                        success = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    };
                    using (SqlCommand command = new SqlCommand(orderItemString, sqlConnection))
                    {
                        foreach (OrderItem orderItem in order.Items)
                        {
                            command.Parameters.Add("@Quantity", System.Data.SqlDbType.Int).Value = orderItem.Quantity;
                            command.Parameters.Add("@ProductId", System.Data.SqlDbType.Int).Value = orderItem.ProductId;
                            command.Parameters.Add("@OrderId", System.Data.SqlDbType.Int).Value = id;
                        }
                        try
                        {
                            sqlConnection.Open();
                            command.ExecuteNonQuery();
                            order.Id = id;
                            sqlConnection.Close();
                            success = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        };
                    }
                }
            }
            return success;
        }

        public IEnumerable<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            string sqlStatement = $"SELECT Id, CreationDate, Status FROM dbo.Orders";
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
                        order.Status = Enum.Parse<OrderStatus>(reader.GetFieldValue<string>(2));
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

        public Order GetOrderById(int id)
        {
            Order order = new Order();
            string sqlStatement = $"SELECT Id, CreationDate, Status FROM dbo.Orders WHERE Id = {id}";
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
                        order.Status = Enum.Parse<OrderStatus>(reader.GetFieldValue<string>(2));
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

        public bool OrderCheckout(int id)
        {
            bool success = false;
            String queryString = $"UPDATE dbo.Orders SET Status=@Status WHERE Id = {id}";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar).Value = OrderStatus.Completed;

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
                    }
                }
            }
            return success;
        }

        public bool CancelOrder(int id)
        {
            bool success = false;
            String queryString = $"UPDATE dbo.Orders SET Status=@Status WHERE Id = {id}";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar).Value = OrderStatus.Cancelled;

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
                    }
                }
            }
            return success;
        }
    }
}

