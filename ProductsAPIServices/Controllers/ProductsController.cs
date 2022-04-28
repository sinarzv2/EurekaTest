﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Steeltoe.Common.Discovery;
using Steeltoe.Common.Http.LoadBalancer;
using Steeltoe.Common.LoadBalancer;
using Steeltoe.Discovery;

namespace ProductsAPIServices.Controllers
{
    [Route("api/[controller]")]
   
    public class ProductsController : Controller
    {
        readonly DiscoveryHttpClientHandler _handler;
        //readonly LoadBalancerHttpClientHandler _handler;

        public ProductsController(IDiscoveryClient client)
        {
            _handler = new DiscoveryHttpClientHandler(client);
            //var loadBalancer = new RoundRobinLoadBalancer(client);
            // _handler = new LoadBalancerHttpClientHandler(loadBalancer);
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] {"Surface Book 2", "Mac Book Pro"};

        } 
        [HttpGet("customer")]
        public async Task<string> GetCustomer(int id)
        {
            
            var client = new HttpClient(_handler, false);
            return await client.GetStringAsync($"http://CustomerService/api/Customers/{id}");
        }
        
        [HttpPost]
        public IEnumerable<string> Post([FromBody]Person person)
        {
            return new string[] { "Hello ", person.Name, person.Age.ToString() };
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
