using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        
        [HttpGet("{username}")]
        public ActionResult GetInfos(string username)
        {
            Organization organization = Organization.InitalizeOrganizations(username).Result;

            return Ok(organization);
        }


        [HttpGet("repositories/{username}")]
        public ActionResult GetAllRepositories(string username)
        {

            Organization organization = Organization.InitalizeOrganizations(username).Result;

            return Ok(organization.GetAllRepositories().Result);
        }

        [HttpGet("repositories/{username}/{languageType}/{num}")]
        public ActionResult GetRepositoriesType(string username, string languageType,int num) 
        {

            Organization organization = Organization.InitalizeOrganizations(username).Result;

            return Ok(organization.repositories.GetRepositories(organization, languageType, num).Result);
        }
    }

}
