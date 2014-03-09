using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Redis;
using ServiceWatcher.Facade.Interface;
using ServiceWatcher.Models;

namespace ServiceWatcher.Facade
{
    public class ServiceQuery : IQueryFor<EmptyParameter, IEnumerable<ServiceDto>>
    {
        private RedisClient redisClient = new RedisClient();

        public IEnumerable<ServiceDto> ExecuteQueryWith(EmptyParameter input)
        {
            return this.redisClient.GetAll<ServiceDto>();
        }
    }
}