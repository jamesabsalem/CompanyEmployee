﻿using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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

    [HttpGet("{id:guid}")]
    public IActionResult GetCompany(Guid id)
    {
        var company = services.CompanyService.GetCompany(id, trackChanges: false);
        return Ok(company);
    }
}