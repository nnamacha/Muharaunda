using Microsoft.AspNetCore.Mvc;
using Muharaunda.Core.Models;
using Munharaunda.Core.Models;
using Munharaunda.Domain.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Munharaunda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuneralController : ControllerBase
    {
        private readonly IFuneralService _funeralService;

        public FuneralController(IFuneralService funeralService)
        {
            _funeralService = funeralService;
        }
        // GET: api/<FuneralController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FuneralController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FuneralController>
        [HttpPost]
        public async Task<ResponseModel<Funeral>> Post([FromBody] Funeral funeral)
        {
            return await _funeralService.CreateFuneralAsync(funeral);
        }

        // PUT api/<FuneralController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FuneralController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
