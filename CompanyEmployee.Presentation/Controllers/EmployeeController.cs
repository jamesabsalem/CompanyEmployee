using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployee.Presentation.Controllers;

[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeeController(IServiceManager services) : ControllerBase
{
    [HttpGet]
    public IActionResult GetEmployeesForCompany(Guid companyId)
    {
        var employees = services.EmployeeService.GetEmployee(companyId, trackChanges: false);
        return Ok(employees);
    }
    [HttpGet("{id:guid}")]
    public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
    {
        var employees = services.EmployeeService.GetEmployee(companyId, id, false);
        return Ok(employees);
    }
}