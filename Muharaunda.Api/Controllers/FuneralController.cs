using Microsoft.AspNetCore.Mvc;
using Muharaunda.Core.Models;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Models;
using Munharaunda.Core.Utilities;
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
        public async Task<IActionResult> Get()
        {
            var result = await _funeralService.GetListOfActiveFuneralsAsync();            

            return CommonUtilites.GenerateResponse(result);
        }

               
        // GET api/<FuneralController>/Profile/5
        [HttpGet("Profile/{id}")]
        public async Task<IActionResult> GetFuneralDetailsByProfileId(int id)
        {
            var funeral =  await _funeralService.GetFuneralDetailsByProfileIdAsync(id);

            return CommonUtilites.GenerateResponse(funeral);

        }



        // POST api/<FuneralController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Funeral funeral)
        {
            var result =  await _funeralService.CreateFuneralAsync(funeral);

            return CommonUtilites.GenerateResponse(result);
        }

        // PUT api/<FuneralController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Funeral funeral)
        {
            var result = await _funeralService.UpdateFuneralAsync(funeral);

            return CommonUtilites.GenerateResponse(result);
        }
        
       

        // DELETE api/<FuneralController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _funeralService.DeleteFuneralAsync(id);

            return CommonUtilites.GenerateResponse(result);
        }


        
    }
}
