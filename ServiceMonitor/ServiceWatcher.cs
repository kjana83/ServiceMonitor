﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ServiceMonitor.Models;
using ServiceStack.Redis;

namespace ServiceMonitor
{
    public partial class ServiceWatcher : ServiceBase
    {

        RedisClient client = new RedisClient();
        private IEnumerable<ServiceDto> services;

        public ServiceWatcher()
        {
            InitializeComponent();
            services = this.client.GetAll<ServiceDto>();
        }

        protected override void OnStart(string[] args)
        {
            services.ToList().ForEach(p => SaveResults(InvokeService(p)));
        }

        protected override void OnStop()
        {
        }

        private ServiceResultsDto InvokeService(ServiceDto service)
        {
            var serviceResults = new ServiceResultsDto
            {
                ServiceId = service.Id,
                Date = DateTime.Now,
                Status = "Amber"
            };

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(service.Url);
                request.ContentType = service.ContentType;
                request.Method = service.Method;

                if (service.Method == "Post")
                {
                    var dataStream = request.GetRequestStream();
                    var byteArray = Encoding.UTF8.GetBytes(service.Request);
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }

                var response = (HttpWebResponse)request.GetResponse();
                var responseStream = response.GetResponseStream();
                var result = new byte[response.ContentLength];
                var content = responseStream.Read(result, 0, (int)response.ContentLength);
                var resultString = Encoding.UTF8.GetString(result);
                if (resultString.Contains(service.Keyword))
                {
                    serviceResults.Status = "Green";
                }
                serviceResults.Response = resultString;

            }
            catch (Exception exception)
            {
                serviceResults.Status = "Red";
                serviceResults.Response = exception.Message;
            }

            return serviceResults;
        }

        private void SaveResults(ServiceResultsDto serviceResults)
        {
            client.Store(serviceResults);
        }
    }
}
