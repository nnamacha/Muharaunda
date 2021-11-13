using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Muharaunda.Core.Models;
using Muharaunda.Domain.Models;
using Munharaunda.Application.Orchestration.Contracts;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using Munharaunda.Domain.Contracts;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Munharaunda.Api.Controllers
{
    [Authorize]
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
        public async Task<ResponseModel<IProfileBase>> Get()
        {
            return await _profileService.GetListOfActiveProfilesAsync();
        }

        [HttpGet("Unauthorised")]
        public async Task<ResponseModel<IProfileBase>> GetUnauthorisedProfiles()
        {
            return await _profileService.GetUnauthorisedProfilesAsync();
        }

        // GET api/<ProfileController>/5
        [HttpGet("{id}")]
        public async Task<ResponseModel<IProfileBase>> Get(int id)
        {
            return await _profileService.GetProfileDetailsAsync(id);
        }

        // POST api/<ProfileController>
        [HttpPost]
        public async Task<ResponseModel<IProfileBase>> Post([FromBody] IProfileBase profile)
        {
            return await _profileService.CreateProfileAsync(profile);
        }

        // PUT api/<ProfileController>/
        [HttpPut("{id}")]
        public async Task<ResponseModel<bool>> Put([FromBody] Profile profile)
        {
            return await _profileService.UpdateProfileAsync(profile);
        }

        // DELETE api/<ProfileController>/5
        [HttpDelete("{id}")]
        public async Task<ResponseModel<bool>> Delete(int id)
        {
            return await _profileService.DeleteProfileAsync(id);
        }
    }
}
