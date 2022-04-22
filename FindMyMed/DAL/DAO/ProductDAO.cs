using FindMyMed.DTO;
using FindMyMed.Models;
using Microsoft.Data.SqlClient;

namespace FindMyMed.DAL
{
    public class ProductDAO : IProductsRepository
    {
        String connect = "Server=tcp:test-sql-lesipl-pds.database.windows.net,1433;Initial Catalog=FindMyMed_db;Persist Security Info=False;User ID=Ipca_Server;Password=Soueu1999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public bool CreateProduct(Product product)
        {
            bool success = false;
            String queryString = $"INSERT INTO dbo.Products (Name, Price, Description, Reference) VALUES (@Name, @Price, @Description, @Reference)";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = product.Name;
                    sqlCommand.Parameters.Add("@Price", System.Data.SqlDbType.Float).Value = product.Price;
                    sqlCommand.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar).Value = product.Description;
                    sqlCommand.Parameters.Add("@Reference", System.Data.SqlDbType.NVarChar).Value = product.Reference;
                   
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

        public IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string sqlStatement = $"SELECT Id, Name, Price, Description, Reference FROM dbo.Products";
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
                        Product prd = new Product();
                        prd.Id = reader.GetFieldValue<int>(0);
                        prd.Name = reader.GetFieldValue<string>(1);
                        prd.Price = reader.GetFieldValue<double>(2);
                        prd.Description = reader.GetFieldValue<string>(3);
                        prd.Reference = reader.GetFieldValue<string>(4);
                        products.Add(prd);
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
            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = new Product();
            string sqlStatement = $"SELECT Id, Name, Price, Description, Reference FROM dbo.Products WHERE Id = {id}";
            using (SqlConnection connection = new SqlConnection(connect))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        product.Id = reader.GetFieldValue<int>(0);
                        product.Name = reader.GetFieldValue<string>(1);
                        product.Price = reader.GetFieldValue<double>(2);
                        product.Description = reader.GetFieldValue<string>(3);
                        product.Reference = reader.GetFieldValue<string>(4);
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
            return product;
        }
        public UpdateProductDTO UpdateProduct(int id, UpdateProductDTO productDTO)
        {
            String queryString = $"UPDATE dbo.Products SET Name=@Name, Price=@Price, Description=@Description WHERE Id = {id}";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = productDTO.Name;
                    sqlCommand.Parameters.Add("@Price", System.Data.SqlDbType.Float).Value = productDTO.Price;
                    sqlCommand.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar).Value = productDTO.Description;

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
            return productDTO;
        }
    }
}


