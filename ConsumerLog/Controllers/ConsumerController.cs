using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsumerLog.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConsumerLog.Controllers
{
    [Route("api/[controller]")]
    public class ConsumerController : Controller
    {
        private readonly IConsumerService _service;
        public ConsumerController(IConsumerService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Consume(string topic)
        {
            try
            {
                _service.StartConsumeData(topic);
                return Created("", "Success");
            }
            catch(Exception e)
            {
                Console.WriteLine( e.Message);
                return BadRequest();
            }
        }
    }
}
