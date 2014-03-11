﻿using ServiceWatcher.Facade.Interface;
using ServiceWatcher.Models;
using ServiceStack.Redis;

namespace ServiceWatcher.Facade
{
    /// <summary>
    /// Command for save the services.
    /// </summary>
    public class ServiceCommandHandler : ICommandHandler<ServiceDto>
    {
        /// <summary>
        /// Executes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public void Execute(ServiceDto input)
        {
            using (var client = new RedisClient())
            {
                client.Increment(GeneralConstants.SERVICE_ID,1);
                input.Id=client.Get<int>(GeneralConstants.SERVICE_ID);
                var clientService = client.As<ServiceDto>();
                clientService.Lists[GeneralConstants.SERVICE].Add(input);
            }

        }
    }
}
