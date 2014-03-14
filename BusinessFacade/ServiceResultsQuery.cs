using System;
using System.Configuration;
using BusinessFacade.Interface;
using BusinessFacade.Models;
using ServiceStack.Redis;
using System.Collections.Generic;
using System.Linq;

namespace BusinessFacade
{
    /// <summary>
    /// To get the service results.
    /// </summary>
    public class ServiceResultsQuery : IQueryFor<EmptyParameter, IEnumerable<ServiceResultsDto>>
    {
        /// <summary>
        /// Executes the query with.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public IEnumerable<ServiceResultsDto> ExecuteQueryWith(EmptyParameter input)
        {
            using (var redisClient = new RedisClient())
            {
                redisClient.ChangeDb(Int32.Parse(ConfigurationManager.AppSettings["DBNAME"]));
                var serviceClient = redisClient.As<ServiceResultsDto>();
                return serviceClient.Lists[GeneralConstants.SERVICE_RESULTS].ToList();
            }
        }
    }
}