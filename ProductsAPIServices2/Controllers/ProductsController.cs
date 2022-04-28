using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery;

namespace ProductsAPIServices.Controllers
{
    [Route("api/[controller]")]
   
    public class ProductsController : Controller
    {
        DiscoveryHttpClientHandler _handler;

        public ProductsController(IDiscoveryClient client)
        {
            _handler = new DiscoveryHttpClientHandler(client);
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
