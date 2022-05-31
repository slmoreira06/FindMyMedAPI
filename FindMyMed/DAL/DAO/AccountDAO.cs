using FindMyMed.DTO;
using FindMyMed.Models;
using Microsoft.Data.SqlClient;

namespace FindMyMed.DAL
{   
    public class AccountDAO : IAccountsRepository
    {
        String connect = "Server=tcp:test-sql-lesipl-pds.database.windows.net,1433;Initial Catalog=FindMyMed_db;Persist Security Info=False;User ID=Ipca_Server;Password=Soueu1999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public bool CreateAccount(Account account)
        {
            bool success = false;
            int id = 0;
            String queryString = $"INSERT INTO dbo.Accounts (Email, UserName, Password, Status, Type) VALUES (@Email, @UserName, @Password, @Status, @Type) ​SELECT scope_identity()";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
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
                        id = Convert.ToInt32(sqlCommand.ExecuteScalar());
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
                    String queryStringUser = $"INSERT INTO dbo.Users (FirstName, LastName, Email, UserName, Password, Birthday, Phone, VAT, UserPoints, AccountId) VALUES (@FirstName, @LastName, @Email, @UserName, @Password, @Birthday, @Phone, @VAT, @UserPoints, @AccountId)";
                    using (SqlCommand sqlCommand = new SqlCommand(queryStringUser, sqlConnection))
                    {
                        User user = new User()
                        {
                            UserName = account.UserName,
                            Password = account.Password,
                            Email = account.Email,
                        };

                        sqlCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar).Value = "";
                        sqlCommand.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar).Value = "";
                        sqlCommand.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = account.Email;
                        sqlCommand.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar).Value = account.UserName;
                        sqlCommand.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = account.Password;
                        sqlCommand.Parameters.Add("@Birthday", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                        sqlCommand.Parameters.Add("@Phone", System.Data.SqlDbType.Int).Value = 0;
                        sqlCommand.Parameters.Add("@VAT", System.Data.SqlDbType.Int).Value = 0;
                        sqlCommand.Parameters.Add("@UserPoints", System.Data.SqlDbType.Int).Value = 0;
                        sqlCommand.Parameters.Add("@AccountId", System.Data.SqlDbType.Int).Value = id;

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
                    String queryStringPharm = $"INSERT INTO dbo.Pharmacies (CompanyName, Email, UserName, Password, Phone, Address, VAT, AccountId) VALUES (@CompanyName, @Email, @UserName, @Password, @Phone, @Address, @VAT, @AccountId)";
                    using (SqlCommand sqlCommand = new SqlCommand(queryStringPharm, sqlConnection))
                    {
                        Pharmacy pharmacy = new Pharmacy()
                        {
                            UserName = account.UserName,
                            Password = account.Password,
                            Email = account.Email,
                        };

                        sqlCommand.Parameters.Add("@CompanyName", System.Data.SqlDbType.NVarChar).Value = "";
                        sqlCommand.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = account.Email;
                        sqlCommand.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar).Value = account.UserName;
                        sqlCommand.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = account.Password;
                        sqlCommand.Parameters.Add("@Phone", System.Data.SqlDbType.Int).Value = 0;
                        sqlCommand.Parameters.Add("@Address", System.Data.SqlDbType.NVarChar).Value = "";
                        sqlCommand.Parameters.Add("@VAT", System.Data.SqlDbType.Int).Value = 0;
                        sqlCommand.Parameters.Add("@AccountId", System.Data.SqlDbType.Int).Value = id;

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
            using (SqlConnection connection = new SqlConnection(connect))
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
                        account.Type = Enum.Parse<Types>(reader.GetFieldValue<string>(5));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginAccount"></param>
        /// <returns></returns>
        public Account GetAccount(LoginAccount loginAccount)
        {
            Account acc = new Account();
            string sqlStatement = $"SELECT Id, Email, Password, Type FROM dbo.Accounts WHERE Email = '{loginAccount.Email}'";
            using (SqlConnection connection = new SqlConnection(connect))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (loginAccount.Password == reader.GetFieldValue<string>(2) && loginAccount.Status == StatusEnum.Activo)
                        {
                            acc.Id = reader.GetFieldValue<int>(0);
                            acc.Email = reader.GetFieldValue<string>(1);
                            acc.Password = reader.GetFieldValue<string>(2);
                            acc.Type = Enum.Parse<Types>(reader.GetFieldValue<string>(3));

                        }
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
            return acc;
        }

        public UpdateAccountDTO DeactivateAccount(int id, UpdateAccountDTO account)
        {
            String queryString = $"UPDATE dbo.Accounts SET Status=@Status WHERE Id = {id}";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    account.Status = StatusEnum.Inactivo;
                    sqlCommand.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar).Value = account.Status;

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
            return account;
        }
    }
}


