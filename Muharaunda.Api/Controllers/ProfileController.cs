using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Muharaunda.Domain.Models;
using Munharaunda.Application.Orchestration.Contracts;
using Munharaunda.Application.Orchestration.Services;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using Munharaunda.Domain.Contracts;
using Munharaunda.Domain.Models;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Munharaunda.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IAppSettings _appSettings;
        static readonly string[] scopeRequiredByApi = new string[] { "userAccess" };

        public ProfileController(IProfileService profileService, IAppSettings appSettings)
        {
            _profileService = profileService;
            _appSettings = appSettings;
        }
        // GET: api/<ProfileController>
        [HttpGet]
        public async Task<ResponseModel<ProfileBase>> Get()
        {
           //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return await _profileService.GetListOfActiveProfilesAsync();
        }

        [HttpGet("Unauthorised")]
        public async Task<ResponseModel<ProfileBase>> GetUnauthorisedProfiles()
        {
           //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return await _profileService.GetUnauthorisedProfilesAsync();
        }

        // GET api/<ProfileController>/5
        [HttpGet("{id}")]
        public async Task<ResponseModel<ProfileBase>> Get(int id)
        {
           //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return await _profileService.GetProfileDetailsAsync(id);
        }

        // POST api/<ProfileController>
        [HttpPost]
        public async Task<ResponseModel<ProfileBase>> Post([FromBody] ProfileBase  profile)
        {
           //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return await _profileService.CreateProfileAsync(profile);
        }// POST api/<ProfileController>
        [HttpPost("dummy")]
        public async Task<string> CreateDummyData()
        {
            var dummy = new DummyData(_appSettings);

            var profiles = dummy.GenerateDummyProfiles(1);
           //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            await _profileService.CreateBulkProfilesAsync(profiles);

            return "Completed";
        }

        // PUT api/<ProfileController>/
        [HttpPut("{id}")]
        public async Task<ResponseModel<bool>> Put([FromBody] Profile profile)
        {
           //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return await _profileService.UpdateProfileAsync(profile);
        }

        // DELETE api/<ProfileController>/5
        [HttpDelete("{id}")]
        public async Task<ResponseModel<bool>> Delete(int id)
        {
           //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            return await _profileService.DeleteProfileAsync(id);
        }
    }
}
