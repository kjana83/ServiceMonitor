using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceWatcher.Models
{
    public class ServiceResultsDto
    {
        public int ServiceId { get; set; }
        public string Status { get; set; }
        public string Response { get; set; }
        public DateTime Date { get; set; }
    }
}