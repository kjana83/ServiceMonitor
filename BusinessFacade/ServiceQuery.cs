using System.Linq;
using BusinessFacade.Interface;
using BusinessFacade.Models;
using ServiceStack.Redis;
using System.Collections.Generic;

namespace BusinessFacade
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