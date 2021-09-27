using Microsoft.AspNetCore.Mvc;
using Muharaunda.Core.Models;
using Munharaunda.Application.Orchestration.Contracts;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using Munharaunda.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Munharaunda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        // GET: api/<ProfileController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("Unauthorised")]
        public async Task<ResponseModel<ProfileBase>> GetUnauthorisedProfiles()
        {
            return await _profileService.GetUnauthorisedProfilesAsync();
        }

        // GET api/<ProfileController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProfileController>
        [HttpPost]
        public async Task<ResponseModel<ProfileBase>> Post([FromBody] CreateProfileRequest profile)
        {
            return await _profileService.CreateProfileAsync(profile);
        }

        // PUT api/<ProfileController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProfileController>/5
        [HttpDelete("{id}")]
        public async Task<ResponseModel<bool>> Delete(int id)
        {
            return await _profileService.DeleteProfileAsync(id);
        }
    }
}
