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
            try
            {
                Organization organization = Organization.InitalizeOrganizations(username).Result;

                return Ok(organization);
            }
            catch (Exception)
            {
                return BadRequest(500);
            }
            
        }


        [HttpGet("repositories/{username}")]
        public ActionResult GetAllRepositories(string username)
        {
            try
            {
                Organization organization = Organization.InitalizeOrganizations(username).Result;

                return Ok(organization.GetAllRepositories().Result);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("repositories/{username}/{languageType}/{num}")]
        public ActionResult GetRepositoriesType(string username, string languageType,int num) 
        {
            try
            {
                Organization organization = Organization.InitalizeOrganizations(username).Result;

                return Ok(organization.repositories.GetRepositories(organization, languageType, num).Result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }

}
