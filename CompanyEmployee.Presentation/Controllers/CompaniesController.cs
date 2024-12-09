using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployee.Presentation.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController(IServiceManager services) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = services.CompanyService.GetAllCompanies(trackChanges: false);
                return Ok(companies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
