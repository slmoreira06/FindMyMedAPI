using FindMyMed.DTO;
using FindMyMed.Models;
using Microsoft.Data.SqlClient;

namespace FindMyMed.DAL
{
    public class InventoryDAO : IInventoriesRepository
    {
        String connect = "Server=tcp:test-sql-lesipl-pds.database.windows.net,1433;Initial Catalog=FindMyMed_db;Persist Security Info=False;User ID=Ipca_Server;Password=Soueu1999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public bool CreateInventory(Inventory inventory)
        {
            bool success = false;
            String queryString = $"INSERT INTO dbo.Inventories (Quantity, ProductId, PharmacyId) VALUES (@Quantity, @ProductId, @PharmacyId)";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Quantity", System.Data.SqlDbType.NVarChar).Value = inventory.Quantity;
                    sqlCommand.Parameters.Add("@ProductId", System.Data.SqlDbType.Int).Value = inventory.ProductId;
                    sqlCommand.Parameters.Add("@PharmacyId", System.Data.SqlDbType.Int).Value = inventory.PharmacyId;

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

        public IEnumerable<Inventory> GetInventories()
        {
            List<Inventory> inventories = new List<Inventory>();
            string sqlStatement = $"SELECT Id, Quantity, ProductId, PharmacyId FROM dbo.Inventories";
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
                        Inventory inv = new Inventory();
                        inv.Id = reader.GetFieldValue<int>(0);
                        inv.Quantity = reader.GetFieldValue<int>(1);
                        inv.ProductId = reader.GetFieldValue<int>(2);
                        inv.PharmacyId = reader.GetFieldValue<int>(3);
                        inventories.Add(inv);
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
            return inventories;
        }

        public Inventory GetInventoryByProduct(int id)
        {
            Inventory inv = new Inventory();
            string sqlStatement = $"SELECT Id, Quantity, ProductId, PharmacyId FROM dbo.Inventories WHERE ProductId = {id}";
            using (SqlConnection connection = new SqlConnection(connect))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        inv.Id = reader.GetFieldValue<int>(0);
                        inv.Quantity = reader.GetFieldValue<int>(1);
                        inv.ProductId = id;
                        inv.PharmacyId = reader.GetFieldValue<int>(3);
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
            return inv;
        }

        public UpdateInventoryDTO UpdateInventory(int id, UpdateInventoryDTO inventoryDTO)
        {
            String queryString = $"UPDATE dbo.Inventories SET Quantity=@Quantity WHERE Id = {id}";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Quantity", System.Data.SqlDbType.Int).Value = inventoryDTO.Quantity;

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
            return inventoryDTO;
        }
    }
}


