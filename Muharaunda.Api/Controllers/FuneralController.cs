using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Muharaunda.Core.Models;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Models;
using Munharaunda.Core.Utilities;
using Munharaunda.Domain.Contracts;
using Munharaunda.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Munharaunda.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FuneralController : ControllerBase
    {
        private readonly IFuneralService _funeralService;
        static readonly string[] scopeRequiredByApi = new string[] { "userAccess" };

        public FuneralController(IFuneralService funeralService)
        {
            _funeralService = funeralService;

        }
        // GET: api/<FuneralController>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _funeralService.GetListOfActiveFuneralsAsync();            

            return CommonUtilites.GenerateResponse(result);
        }

               
        // GET api/<FuneralController>/Profile/5
        [HttpGet("Profile/{id}")]
        public async Task<IActionResult> GetFuneralDetailsByProfileId(int id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var funeral =  await _funeralService.GetFuneralDetailsByProfileIdAsync(id);

            return CommonUtilites.GenerateResponse(funeral);

        }



        // POST api/<FuneralController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateFuneralRequest request )
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result =  await _funeralService.CreateFuneralAsync(request);

            return CommonUtilites.GenerateResponse(result);
        }

        // PUT api/<FuneralController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Funeral funeral)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _funeralService.UpdateFuneralAsync(funeral);

            return CommonUtilites.GenerateResponse(result);
        }

        // PUT api/<FuneralController>/profiles/5
        [HttpPut("profiles/{id}")]
        public async Task<IActionResult> UpdateProfiles(string id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _funeralService.UpdateProfiles(id);

            return CommonUtilites.GenerateResponse(result);
        }
        
       

        // DELETE api/<FuneralController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _funeralService.DeleteFuneralAsync(id);

            return CommonUtilites.GenerateResponse(result);
        }


        
    }
}
