using FindMyMed.DAL.Repositories;
using FindMyMed.DTO.Update;
using FindMyMed.Models;
using Microsoft.Data.SqlClient;

namespace FindMyMed.DAL.DAO
{
    public class ReminderDAO : IRemindersRepository
    {
        String connect = "Server=tcp:test-sql-lesipl-pds.database.windows.net,1433;Initial Catalog=FindMyMed_db;Persist Security Info=False;User ID=Ipca_Server;Password=Soueu1999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public bool CreateReminder(Reminder reminder)
        {
            bool success = false;
            String queryString = $"INSERT INTO dbo.Reminders (Text, Repeat, Hours, Status, MessageSid) VALUES (@Text, @Repeat, @Hours, @Status, @MessageSid)";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Text", System.Data.SqlDbType.NVarChar).Value = reminder.Text;
                    sqlCommand.Parameters.Add("@Repeat", System.Data.SqlDbType.NVarChar).Value = reminder.Repeat;
                    sqlCommand.Parameters.Add("@Hours", System.Data.SqlDbType.Int).Value = reminder.Hours;
                    sqlCommand.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar).Value = reminder.Status;
                    sqlCommand.Parameters.Add("@MessageSid", System.Data.SqlDbType.NVarChar).Value = reminder.Status;

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

        public IEnumerable<Reminder> GetReminders()
        {
            List<Reminder> reminders = new List<Reminder>();
            string sqlStatement = $"SELECT Id, Text, Repeat, Hours, Status, MessageSid FROM dbo.Reminders";
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
                        Reminder rm = new Reminder();
                        rm.Id = reader.GetFieldValue<int>(0);
                        rm.Text = reader.GetFieldValue<string>(1);
                        rm.Repeat = Enum.Parse<Repetition>(reader.GetFieldValue<string>(2));
                        rm.Hours = reader.GetFieldValue<int>(3);
                        rm.Status = Enum.Parse<Status>(reader.GetFieldValue<string>(4));
                        rm.MessageSid = reader.GetFieldValue<string>(5);
                        reminders.Add(rm);
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
            return reminders;
        }

        public Reminder GetReminderById(int id)
        {
            Reminder rm = new Reminder();
            string sqlStatement = $"SELECT Id, Text, Repeat, Hours, Status, MessageSid FROM dbo.Reminders WHERE Id = {id}";
            using (SqlConnection connection = new SqlConnection(connect))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        rm.Id = reader.GetFieldValue<int>(0);
                        rm.Text = reader.GetFieldValue<string>(1);
                        rm.Repeat = Enum.Parse<Repetition>(reader.GetFieldValue<string>(2));
                        rm.Hours = reader.GetFieldValue<int>(3);
                        rm.Status = Enum.Parse<Status>(reader.GetFieldValue<string>(4)); 
                        rm.MessageSid = reader.GetFieldValue<string>(5);

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
            return rm;
        }

        public UpdateReminderDTO CancelReminder(int id, UpdateReminderDTO reminderDTO)
        {
            String queryString = $"UPDATE dbo.Reminders SET Status=@Status WHERE Id = {id}";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    reminderDTO.Status = Status.Inactive;
                    sqlCommand.Parameters.Add("@Status", System.Data.SqlDbType.Int).Value = reminderDTO.Status;

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
            return reminderDTO;
        }
    }
}
}
