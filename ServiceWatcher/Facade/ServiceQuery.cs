using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Redis;
using ServiceWatcher.Facade.Interface;
using ServiceWatcher.Models;

namespace ServiceWatcher.Facade
{
    /// <summary>
    /// Query to fetch the services
    /// </summary>
    public class ServiceQuery : IQueryFor<EmptyParameter, IEnumerable<ServiceDto>>
    {
        /// <summary>
        /// Executes the query with.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public IEnumerable<ServiceDto> ExecuteQueryWith(EmptyParameter input)
        {
            using (var redisClient = new RedisClient())
            {
                var serviceClient = redisClient.As<ServiceDto>();
                return serviceClient.Lists[GeneralConstants.SERVICE].ToList();
            }

        }
    }
}