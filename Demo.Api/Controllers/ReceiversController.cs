using Demo.Api.Models;
using Demo.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using NLog;

namespace Demo.Controllers
{
    public class ReceiversController : ApiController
    {
        private SqlConnection conn = new SqlConnection(Api.Properties.Settings.Default.DBConnection.ToString());
        Logger logger = LogManager.GetCurrentClassLogger();
        public IHttpActionResult GetAllReceivers()
        {
            logger.Info("Dohvat primatelja");
            IList<Receiver> receivers = null;

            try
            {
                receivers = (from receiverRow in DBOperations.GetReceivers(conn).AsEnumerable()
                             select new Receiver()
                             {
                                 Id = receiverRow.Field<int>("Id"),
                                 Name = receiverRow.Field<string>("Name"),
                                 MobilePhone = receiverRow.Field<string>("MobilePhone"),
                             }
                             ).ToList();
                conn.Close();

                return Ok(receivers);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Greška kod dohvata primatelja.");
                return NotFound();
            }
        }


        public IHttpActionResult PostNewReceiver([FromBody] Receiver receiver)
        {
            logger.Info("Unos primatelja.");
            try
            {
                DBOperations.InsertReceivers(conn, receiver.Name, receiver.MobilePhone);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Greška kod unosa primatelja.");
                return NotFound();
            }
            return Ok();
        }
    }
}