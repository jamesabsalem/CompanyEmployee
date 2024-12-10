using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployee.Presentation.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesController(IServiceManager services) : ControllerBase
{
    [HttpGet]
    public IActionResult GetCompanies()
    {
        var companies = services.CompanyService.GetAllCompanies(false);
        return Ok(companies);
    }

    [HttpGet("{id:guid}", Name = "CompanyById")]
    public IActionResult GetCompany(Guid id)
    {
        var company = services.CompanyService.GetCompany(id, false);

        if (company is null)
            return NotFound($"Company with id {id} not found.");

        return Ok(company);
    }

    [HttpPost]
    public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
    {
        if (company is null)
            return BadRequest("CompanyForCreationDto object is null.");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var createdCompany = services.CompanyService.CreateCompany(company);

        if (createdCompany?.Id == Guid.Empty || createdCompany == null)
            return StatusCode(500, "Failed to create the company. Please try again.");

        return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
    }
}