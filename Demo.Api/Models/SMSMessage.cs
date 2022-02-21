using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Api.Models
{
    public class SMSMessage
    {
        public List<Receiver> receivers { get; set; }
        public string Message { get; set; }
    }
}