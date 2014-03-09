using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServiceStack.Commands;
using ServiceWatcher.Facade;
using ServiceWatcher.Facade.Interface;
using ServiceWatcher.Models;

namespace ServiceWatcher.Controllers
{
    public class ServiceAdminController : ApiController
    {
        private readonly IQueryFor<EmptyParameter, IEnumerable<ServiceDto>> serviceAdmin;
        private readonly ICommandHandler<ServiceDto> serviceCommandHandler;

        public ServiceAdminController()
        {
            serviceAdmin=new ServiceQuery();
            serviceCommandHandler=new ServiceCommandHandler();
        }

        //public ServiceAdminController(IQueryFor<EmptyParameter, IEnumerable<ServiceDto>> serviceAdmin)
        //{
        //    serviceAdmin = serviceAdmin;
        //}

        public HttpResponseMessage Post([FromBody]ServiceDto service)
        {
            this.serviceCommandHandler.Execute(service);
            return this.Request.CreateResponse(HttpStatusCode.Created);
        }

        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK,
                this.serviceAdmin.ExecuteQueryWith(new EmptyParameter()));
        }
    }
}
