using FindMyMed.Models;
using Microsoft.Data.SqlClient;

namespace FindMyMed.DAL
{
    public class AccountDAO
    {
        public Account LoadAccById(int id)
        {
            Account account = new Account();
            string sqlStatement = $"SELECT Id, Email, UserName, Password, Status, Type, UserId FROM dbo.Accounts WHERE Id = {id}";
            using (SqlConnection connection = new SqlConnection("AppDBContextConnection"))
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
    

