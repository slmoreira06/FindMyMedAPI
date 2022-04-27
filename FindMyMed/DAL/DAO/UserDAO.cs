using FindMyMed.DTO;
using FindMyMed.Models;
using Microsoft.Data.SqlClient;

namespace FindMyMed.DAL
{
    public class UserDAO : IUsersRepository
    {
        String connect = "Server=tcp:test-sql-lesipl-pds.database.windows.net,1433;Initial Catalog=FindMyMed_db;Persist Security Info=False;User ID=Ipca_Server;Password=Soueu1999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public IEnumerable<User> GetUsers()
        {
            List<User> users = new List<User>();
            string sqlStatement = $"SELECT Id, FirstName, LastName, Email, UserName, Password, Birthday, Phone, VAT, UserPoints FROM dbo.Users";
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
                        User user = new User();
                        user.Id = reader.GetFieldValue<int>(0);
                        user.FirstName = reader.GetFieldValue<string>(1);
                        user.LastName = reader.GetFieldValue<string>(2);
                        user.Email = reader.GetFieldValue<string>(3);
                        user.UserName = reader.GetFieldValue<string>(4);
                        user.Password = reader.GetFieldValue<string>(5);
                        user.Birthday = reader.GetFieldValue<DateTime>(6);
                        user.Phone = reader.GetFieldValue<int>(7);
                        user.VAT = reader.GetFieldValue<int>(8);
                        user.UserPoints = reader.GetFieldValue<int>(9);
                        users.Add(user);
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
            return users;
        }

        public User GetUserById(int id)
        {
            User user = new User();
            string sqlStatement = $"SELECT Id, FirstName, LastName, Email, UserName, Password, Birthday, Phone, VAT, UserPoints FROM dbo.Users WHERE Id = {id}";
            using (SqlConnection connection = new SqlConnection(connect))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        user.Id = reader.GetFieldValue<int>(0);
                        user.FirstName = reader.GetFieldValue<string>(1);
                        user.LastName = reader.GetFieldValue<string>(2);
                        user.Email = reader.GetFieldValue<string>(3);
                        user.UserName = reader.GetFieldValue<string>(4);
                        user.Password = reader.GetFieldValue<string>(5);
                        user.Birthday = reader.GetFieldValue<DateTime>(6);
                        user.Phone = reader.GetFieldValue<int>(7);
                        user.VAT = reader.GetFieldValue<int>(8);
                        user.UserPoints = reader.GetFieldValue<int>(9);
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
            return user;
        }

        public UpdateUserDTO UpdateUserProfile(int id, UpdateUserDTO userDTO)
        {
            String queryString = $"UPDATE dbo.Users SET FirstName=@FirstName, LastName=@LastName, UserName=@UserName, Password=@Password, Birthday=@Birthday, Phone=@Phone, VAT=@VAT, UserPoints=@UserPoints WHERE Id = {id}";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar).Value = userDTO.FirstName;
                    sqlCommand.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar).Value = userDTO.LastName;
                    sqlCommand.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar).Value = userDTO.UserName;
                    sqlCommand.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = userDTO.Password;
                    sqlCommand.Parameters.Add("@Birthday", System.Data.SqlDbType.DateTime).Value = userDTO.Birthday;
                    sqlCommand.Parameters.Add("@Phone", System.Data.SqlDbType.Int).Value = userDTO.Phone;
                    sqlCommand.Parameters.Add("@VAT", System.Data.SqlDbType.Int).Value = userDTO.VAT;
                    sqlCommand.Parameters.Add("@UserPoints", System.Data.SqlDbType.Int).Value = userDTO.UserPoints;

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
                String queryAccount = $"UPDATE dbo.Accounts SET UserName=@UserName, Password=@Password  WHERE Email = '{userDTO.Email}'";

                using (SqlCommand sqlCommand = new SqlCommand(queryAccount, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar).Value = userDTO.UserName;
                    sqlCommand.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = userDTO.Password;

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
            return userDTO;
        }
    }
}


