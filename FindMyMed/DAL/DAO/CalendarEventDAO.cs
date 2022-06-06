using FindMyMed.DAL.Repositories;
using FindMyMed.DTO.Update;
using FindMyMed.Models;
using Microsoft.Data.SqlClient;

namespace FindMyMed.DAL.DAO
{
    public class CalendarEventDAO : ICalendarEventsRepository
    {
        String connect = "Server=tcp:test-sql-lesipl-pds.database.windows.net,1433;Initial Catalog=FindMyMed_db;Persist Security Info=False;User ID=Ipca_Server;Password=Soueu1999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public bool CreateCalendarEvent(CalendarEvent calendarEvent)
        {
            bool success = false;
            String queryString = $"INSERT INTO dbo.CalendarEvents (Start, End, Title, Description, Color) VALUES (@Start, @End, @Title, @Description, @Color)";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Start", System.Data.SqlDbType.DateTime).Value = calendarEvent.Start;
                    sqlCommand.Parameters.Add("@End", System.Data.SqlDbType.DateTime).Value = calendarEvent.End;
                    sqlCommand.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar).Value = calendarEvent.Title;
                    sqlCommand.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar).Value = calendarEvent.Description;
                    sqlCommand.Parameters.Add("@Color", System.Data.SqlDbType.NVarChar).Value = calendarEvent.Color;

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

        public IEnumerable<CalendarEvent> GetEvents()
        {
            List<CalendarEvent> events = new List<CalendarEvent>();
            string sqlStatement = $"SELECT Id, Start, End, Title, Description, Color FROM dbo.CalendarEvents";
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
                        CalendarEvent evt = new CalendarEvent();
                        evt.Id = reader.GetFieldValue<int>(0);
                        evt.Start = reader.GetFieldValue<DateTime>(1);
                        evt.End = reader.GetFieldValue<DateTime>(2);
                        evt.Title = reader.GetFieldValue<string>(3);
                        evt.Description = reader.GetFieldValue<string>(4);
                        evt.Color = reader.GetFieldValue<string>(5);
                        events.Add(evt);
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
            return events;
        }

        public CalendarEvent GetCalendarEventById(int id)
        {
            CalendarEvent evt = new CalendarEvent();
            string sqlStatement = $"SELECT Id, Start, End, Title, Description, Color FROM dbo.CalendarEvents WHERE Id = {id}";
            using (SqlConnection connection = new SqlConnection(connect))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        evt.Id = reader.GetFieldValue<int>(0);
                        evt.Start = reader.GetFieldValue<DateTime>(1);
                        evt.End = reader.GetFieldValue<DateTime>(2);
                        evt.Title = reader.GetFieldValue<string>(3);
                        evt.Description = reader.GetFieldValue<string>(4);
                        evt.Color = reader.GetFieldValue<string>(5);
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
            return evt;
        }

        public UpdateCalendarEventDTO UpdateCalendarEvent(int id, UpdateCalendarEventDTO calendarEventDTO)
        {
            String queryString = $"UPDATE dbo.CalendarEvents SET Start=@Start, End=@End, Title=@Title, Description=@Description, Color=@Color WHERE Id = {id}";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@Start", System.Data.SqlDbType.DateTime).Value = calendarEventDTO.Start;
                    sqlCommand.Parameters.Add("@End", System.Data.SqlDbType.DateTime).Value = calendarEventDTO.End;
                    sqlCommand.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar).Value = calendarEventDTO.Title;
                    sqlCommand.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar).Value = calendarEventDTO.Description;
                    sqlCommand.Parameters.Add("@Color", System.Data.SqlDbType.NVarChar).Value = calendarEventDTO.Color;

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
            return calendarEventDTO;
        }

        public bool DeleteCalendarEvent(int id)
        {
            bool success = false;
            String queryString = $"DELETE FROM dbo.CalendarEvents WHERE Id = {id}";
            using (SqlConnection sqlConnection = new SqlConnection(connect))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
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
