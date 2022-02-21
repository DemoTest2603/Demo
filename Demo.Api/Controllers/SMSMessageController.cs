using Demo.Api.Models;
using Demo.Data;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.Http;
using NLog;

namespace Demo.Controllers
{
    public class SMSMessageController : ApiController
    {
        private SqlConnection conn = new SqlConnection(Api.Properties.Settings.Default.DBConnection.ToString());

        Logger logger = LogManager.GetCurrentClassLogger();
        public void Post([FromBody] SMSMessage smsmessage)
        {
            logger.Info("Slanje SMS poruke");
            foreach (var receiver in smsmessage.receivers)
            {
                string FileName = $"demo_{receiver.MobilePhone}.txt";

                try
                {
                    if (DBOperations.InsertSMSMessage(conn, receiver.Id, FileName))
                    {
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(Api.Properties.Settings.Default.SMSMessagePath, FileName), false))
                        {
                            outputFile.WriteLine(smsmessage.Message);
                        }

                        logger.Info($"Poslana SMS poruka na broj {receiver.MobilePhone}");
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Greška kod slanja SMS poruke");
                }
            }
        }
    }
}