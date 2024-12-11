using CompanyEmployee.Presentation.ModelBinders;
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

    [HttpGet("collection/({ids})", Name = "CompanyCollection")]
    public IActionResult GetCompanyCollection([ModelBinder(BinderType =
        typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
    {
        if (ids == null || !ids.Any())
            return BadRequest("Ids parameter is null or empty.");

        var companies = services.CompanyService.GetByIds(ids, trackChanges: false);

        if (companies == null || !companies.Any())
            return NotFound("No companies found for the provided ids.");

        return Ok(companies);
    }

    [HttpPost("collection")]
    public IActionResult CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
    {
        if (companyCollection == null || !companyCollection.Any())
            return BadRequest("Company collection is null or empty.");

        var result = services.CompanyService.CreateCompanyCollection(companyCollection);

        return CreatedAtRoute("CompanyCollection", new { ids = string.Join(",", result.ids) }, result.companies);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteCompany(Guid id)
    {
        services.CompanyService.DeleteCompany(id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
    {
        if (company is null)
            return BadRequest("CompanyForUpdateDto object is null");

        services.CompanyService.UpdateCompany(id, company, trackChanges:true);
        return NoContent();
    }

}