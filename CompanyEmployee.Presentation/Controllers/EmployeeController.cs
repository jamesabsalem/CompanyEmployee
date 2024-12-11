using System.Xml.Xsl;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

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
    [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
    public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
    {
        var employees = services.EmployeeService.GetEmployee(companyId, id, false);
        return Ok(employees);
    }

    [HttpPost]
    public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
    {
        if (employee is null)
            return BadRequest("EmployeeForCreationsDto object is null");

        var employeeToReturn = services.EmployeeService.CreateEmployeeForCompany(companyId, employee, false);

        return CreatedAtAction("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id)
    {
        services.EmployeeService.DeleteEmployeeForCompany(companyId,id,trackChanges:false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDto employee)
    {
        if (employee is null)
            return BadRequest("EmployeeForUpdateDto object is null");

        services.EmployeeService.UpdateEmployeeForCompany(companyId, id, employee, comTrackChanges: false,
            empTrackChanges: true);

        return NoContent();
    }
}