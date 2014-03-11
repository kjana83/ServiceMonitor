using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ServiceStack.Redis;

namespace ConsoleApplication
{
    class Program
    {

        private static RedisClient client = new RedisClient();
        private static List<ServiceDto> services;

        static void Main(string[] args)
        {

            var clientServices = client.As<ServiceDto>();
            services = clientServices.Lists["SERVICES"].ToList();
            services.ForEach(service => SaveResults(InvokeService(service)));
        }


        private static ServiceResultsDto InvokeService(ServiceDto service)
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

        private static void SaveResults(ServiceResultsDto serviceResults)
        {
            client.Store(serviceResults);
        }


    }
}
