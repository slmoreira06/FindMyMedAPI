using FindMyMed.Models;
using Microsoft.Data.SqlClient;

namespace FindMyMed.DAL
{
    public class SupportDAO : ISupportsRepository
    {
        String connect = "Server=tcp:test-sql-lesipl-pds.database.windows.net,1433;Initial Catalog=FindMyMed_db;Persist Security Info=False;User ID=Ipca_Server;Password=Soueu1999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public bool CreateSupport(Support support)
        {
            bool success = false;
            String queryString = $"INSERT INTO dbo.Supports (Name, Email, Phone, Subject, Message) VALUES (@Name, @Email, @Phone, @Subject, @Message)";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = support.Name;
                    sqlCommand.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = support.Email;
                    sqlCommand.Parameters.Add("@Phone", System.Data.SqlDbType.Int).Value = support.Phone;
                    sqlCommand.Parameters.Add("@Subject", System.Data.SqlDbType.NVarChar).Value = support.Subject;
                    sqlCommand.Parameters.Add("@Message", System.Data.SqlDbType.NVarChar).Value = support.Message;

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

        public IEnumerable<Support> GetSupports()
        {
            List<Support> supports = new List<Support>();
            string sqlStatement = $"SELECT Id, Name, Email, Phone, Subject, Message FROM dbo.Supports";
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
                        Support spp = new Support();
                        spp.Id = reader.GetFieldValue<int>(0);
                        spp.Name = reader.GetFieldValue<string>(1);
                        spp.Email = reader.GetFieldValue<string>(2);
                        spp.Phone = reader.GetFieldValue<int>(3);
                        spp.Subject = reader.GetFieldValue<string>(4);
                        spp.Message = reader.GetFieldValue<string>(5);
                        supports.Add(spp);
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
            return supports;
        }

        public Support GetSupportById(int id)
        {
            Support support = new Support();
            string sqlStatement = $"SELECT Id, Name, Email, Phone, Subject, Message FROM dbo.Supports WHERE Id = {id}";
            using (SqlConnection connection = new SqlConnection(connect))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        support.Id = reader.GetFieldValue<int>(0);
                        support.Name = reader.GetFieldValue<string>(1);
                        support.Email = reader.GetFieldValue<string>(2);
                        support.Phone = reader.GetFieldValue<int>(3);
                        support.Subject = reader.GetFieldValue<string>(4);
                        support.Message = reader.GetFieldValue<string>(5);
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
            return support;
        }
    }
}


