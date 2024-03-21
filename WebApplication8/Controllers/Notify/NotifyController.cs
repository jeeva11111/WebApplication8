using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace WebApplication8.Controllers.Notify
{
    public class Notifaction
    {
        public string GetAllNotify(Models.Notify.Notify notify)
        {
            SqlConnection sqlConnection = null;
            string result = "";

            try
            {
                sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ServerLink"].ConnectionString);

                SqlCommand command = new SqlCommand("sp_notifyGetAll", sqlConnection);

                sqlConnection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Title", notify.Title);
                command.Parameters.AddWithValue("@Message", notify.Message);
                command.Parameters.AddWithValue("@IsActive", notify.IsActive);
                command.Parameters.AddWithValue("@Staus", notify.Status);

                result = Convert.ToString(command.ExecuteScalar());

                if (result == null)
                {
                    result = "No records found";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }

            return result;
        }
    }
}
