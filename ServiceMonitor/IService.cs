using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceMonitor.Models;

namespace ServiceMonitor
{
    public interface IService
    {
        ServiceResultsDto Invoke(ServiceDto service);
    }
}
