using System;
using System.Data;
using System.Data.SqlClient;
using NLog;

namespace Demo.Data
{
    public static class DBOperations
    {
        public static DataTable GetReceivers(SqlConnection connection)
        {
            DataTable dataTable = new DataTable();
            Logger logger = LogManager.GetCurrentClassLogger();

            try
            {
                using (SqlCommand command = new SqlCommand("GetReceivers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command))
                    {
                        sqlDataAdapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return dataTable;
        }
        public static void InsertReceivers(SqlConnection connection, string Name, string MobilePhone)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            
            try
            {
                using (SqlCommand command = new SqlCommand("InsertReceivers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@MobilePhone", MobilePhone);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
        public static bool InsertSMSMessage(SqlConnection connection, int receiverId, string FileName)
        {
            Logger logger = LogManager.GetCurrentClassLogger();

            try
            {
                using (SqlCommand command = new SqlCommand("SendSMS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ReceiverId", receiverId);
                    command.Parameters.AddWithValue("@FileName", FileName);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    return true;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }
    }


}