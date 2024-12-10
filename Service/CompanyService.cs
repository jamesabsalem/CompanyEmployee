﻿using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    : ICompanyService
{
    public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
    {
        var companies = repository.Company.GetAllCompanies(trackChanges);
        var companiesDto = mapper.Map<IEnumerable<CompanyDto>>(companies);
        return companiesDto;
    }

    public CompanyDto GetCompany(Guid id, bool trackChanges)
    {
        var company = repository.Company.GetCompany(id, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(id);

        var companyDto = mapper.Map<CompanyDto>(company);
        return companyDto;
    }

    public CompanyDto CreateCompany(CompanyForCreationDto company)
    {
        var companyEntity = mapper.Map<Company>(company);

        repository.Company.CreateCompany(companyEntity);
        repository.Save();

        var companyToReturn = mapper.Map<CompanyDto>(companyEntity);

        return companyToReturn;
    }
}