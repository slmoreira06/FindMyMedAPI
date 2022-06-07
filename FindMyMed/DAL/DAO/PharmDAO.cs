using FindMyMed.DTO;
using FindMyMed.Models;
using Microsoft.Data.SqlClient;

namespace FindMyMed.DAL
{
    public class PharmDAO : IPharmsRepository
    {
        String connect = "Server=tcp:test-sql-lesipl-pds.database.windows.net,1433;Initial Catalog=FindMyMed_db;Persist Security Info=False;User ID=Ipca_Server;Password=Soueu1999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public IEnumerable<Pharmacy> GetPharms()
        {
            List<Pharmacy> pharms = new List<Pharmacy>();
            string sqlStatement = $"SELECT Id, CompanyName, Email, UserName, Password, Phone, Address, VAT, Longitude, Latitude FROM dbo.Pharmacies";
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
                        Pharmacy pharm = new Pharmacy();
                        pharm.Id = reader.GetFieldValue<int>(0);
                        pharm.CompanyName = reader.GetFieldValue<string>(1);
                        pharm.Email = reader.GetFieldValue<string>(2);
                        pharm.UserName = reader.GetFieldValue<string>(3);
                        pharm.Password = reader.GetFieldValue<string>(4);
                        pharm.Phone = reader.GetFieldValue<int>(5);
                        pharm.Address = reader.GetFieldValue<string>(6);
                        pharm.VAT = reader.GetFieldValue<int>(7);
                        pharm.Longitude = reader.GetFieldValue<double>(8);
                        pharm.Latitude = reader.GetFieldValue<double>(9);
                        pharms.Add(pharm);
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
            return pharms;
        }

        public Pharmacy GetPharmById(int id)
        {
            Pharmacy pharm = new Pharmacy();
            string sqlStatement = $"SELECT Id, CompanyName, Email, UserName, Password, Phone, Address, VAT, Longitude, Latitude FROM dbo.Pharmacies WHERE Id = {id}";
            using (SqlConnection connection = new SqlConnection(connect))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        pharm.Id = reader.GetFieldValue<int>(0);
                        pharm.CompanyName = reader.GetFieldValue<string>(1);
                        pharm.Email = reader.GetFieldValue<string>(2);
                        pharm.UserName = reader.GetFieldValue<string>(3);
                        pharm.Password = reader.GetFieldValue<string>(4);
                        pharm.Phone = reader.GetFieldValue<int>(5);
                        pharm.Address = reader.GetFieldValue<string>(6);
                        pharm.VAT = reader.GetFieldValue<int>(7);
                        pharm.Longitude = reader.GetFieldValue<double>(8);
                        pharm.Latitude = reader.GetFieldValue<double>(9);
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
            return pharm;
        }

        public UpdatePharmDTO UpdatePharmProfile(int id, UpdatePharmDTO pharmDTO)
        {
            String queryString = $"UPDATE dbo.Pharmacies SET CompanyName=@CompanyName, UserName=@UserName, Password=@Password, Phone=@Phone, Address=@Address, Longitude=@Longitude, Latitude=@Latitude WHERE Id = {id}";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@CompanyName", System.Data.SqlDbType.NVarChar).Value = pharmDTO.CompanyName;
                    sqlCommand.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar).Value = pharmDTO.UserName;
                    sqlCommand.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = pharmDTO.Password;
                    sqlCommand.Parameters.Add("@Phone", System.Data.SqlDbType.Int).Value = pharmDTO.Phone;
                    sqlCommand.Parameters.Add("@Address", System.Data.SqlDbType.NVarChar).Value = pharmDTO.Address;
                    sqlCommand.Parameters.Add("@Longitude", System.Data.SqlDbType.Float).Value = pharmDTO.Longitude;
                    sqlCommand.Parameters.Add("@Latitude", System.Data.SqlDbType.Float).Value = pharmDTO.Latitude;


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
                String queryAccount = $"UPDATE dbo.Accounts SET UserName=@UserName, Password=@Password WHERE Email = '{pharmDTO.Email}'";

                using (SqlCommand sqlCommand = new SqlCommand(queryAccount, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar).Value = pharmDTO.UserName;
                    sqlCommand.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = pharmDTO.Password;

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
            return pharmDTO;
        }
    }
}


