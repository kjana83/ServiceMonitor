using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ServiceWatcher.Facade.Interface;
using ServiceWatcher.Models;
using ServiceStack.Redis;

namespace ServiceWatcher.Facade
{
    public class ServiceCommandHandler : ICommandHandler<ServiceDto>
    {
        private RedisClient client = new RedisClient();

        public void Execute(ServiceDto input)
        {
            client.Store(input);
        }
    }
}
