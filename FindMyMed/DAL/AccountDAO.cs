using FindMyMed.Models;
using Microsoft.Data.SqlClient;

namespace FindMyMed.DAL
{
    public class AccountDAO : IAccountsRepository
    {
        public bool CreateAccount(Account account)
        {
            bool success = false;
            String queryString = $"INSERT INTO dbo.Accounts (Email, UserName, Password, Status, Type) VALUES (@Email, @UserName, @Password, @Status, @Type)";
            using (SqlConnection sqlConnection = new SqlConnection("AppConnection"))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = account.Email;
                    sqlCommand.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar).Value = account.UserName;
                    sqlCommand.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = account.Password;
                    sqlCommand.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar).Value = account.Status;
                    sqlCommand.Parameters.Add("@Type", System.Data.SqlDbType.NVarChar).Value = account.Type;

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
                if (account.Type == Enum.Parse<Types>("User"))
                {
                    String queryStringUser = $"INSERT INTO dbo.Users (Email, UserName, Password) VALUES (@Email, @UserName, @Password)";
                    using (SqlCommand sqlCommand = new SqlCommand(queryStringUser, sqlConnection))
                    {
                        User user = new User()
                        {
                            UserName = account.UserName,
                            Password = account.Password,
                            Email = account.Email,
                        };
                        
                        sqlCommand.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = account.Email;
                        sqlCommand.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar).Value = account.UserName;
                        sqlCommand.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = account.Password;

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
                else if (account.Type == Enum.Parse<Types>("Pharm"))
                {
                    String queryStringPharm = $"INSERT INTO dbo.Pharmacy (Email, UserName, Password) VALUES (@Email, @UserName, @Password)";
                    using (SqlCommand sqlCommand = new SqlCommand(queryStringPharm, sqlConnection))
                    {


                        sqlCommand.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = account.Email;
                        sqlCommand.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar).Value = account.UserName;
                        sqlCommand.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = account.Password;

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
            }
            return success;
        }

        public IEnumerable<Account> GetAccounts()
        {
            List<Account> accounts = new List<Account>();
            string sqlStatement = $"SELECT Id, Email, UserName, Password, Status, Type FROM dbo.Accounts";
            using (SqlConnection connection = new SqlConnection("AppConnection"))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    // Call Read before accessing data.
                    while (reader.Read())
                    {
                        Account acc = new Account();
                        acc.Id = reader.GetFieldValue<int>(0);
                        acc.Email = reader.GetFieldValue<string>(1);
                        acc.UserName = reader.GetFieldValue<string>(2);
                        acc.Password = reader.GetFieldValue<string>(3);
                        acc.Status = Enum.Parse<StatusEnum>(reader.GetFieldValue<string>(4));
                        acc.Type = Enum.Parse<Types>(reader.GetFieldValue<string>(5));
                        accounts.Add(acc);
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
            return accounts;
        }

        public Account GetAccountById(int id)
        {
            Account account = new Account();
            string sqlStatement = $"SELECT Id, Email, UserName, Password, Status, Type FROM dbo.Accounts WHERE Id = {id}";
            using (SqlConnection connection = new SqlConnection("AppConnection"))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        account.Id = reader.GetFieldValue<int>(0);
                        account.Email = reader.GetFieldValue<string>(1);
                        account.UserName = reader.GetFieldValue<string>(2);
                        account.Password = reader.GetFieldValue<string>(3);
                        account.Status = Enum.Parse<StatusEnum>(reader.GetFieldValue<string>(4));
                        account.Type = Enum.Parse<Types>(reader.GetFieldValue<string>(4));
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
            return account;
        }
    }
}


