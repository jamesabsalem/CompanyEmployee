﻿using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using Shared.RequestFeatures.MetaData;

namespace Service;

internal sealed class EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    : IEmployeeService
{
    public async Task<(IEnumerable<EmployeeDto> employees, MetaData metaData)> GetEmployeesAsync(Guid companyId,
        EmployeeParameters employeeParameters, bool trackChanges)
    {
        if (!employeeParameters.ValidAgeRange)
            throw new MaxAgeRangeBadRequestException();

        await CheckIfCompanyExists(companyId, trackChanges);

        var employeesWithMetaData = await repository.Employee.GetEmployeesAsync(companyId, employeeParameters, trackChanges);
        var employeesDto = mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);
        return (employees: employeesDto, metaData: employeesWithMetaData.MetaData);
    }

    public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
    {
        await CheckIfCompanyExists(companyId, trackChanges);
        var employeeDb = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);

        var employee = mapper.Map<EmployeeDto>(employeeDb);
        return employee;
    }

    public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId,
        EmployeeForCreationDto employeeForCreation,
        bool trackChanges)
    {
        await CheckIfCompanyExists(companyId, trackChanges);

        var employeeEntity = mapper.Map<Employee>(employeeForCreation);

        repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
        await repository.SaveAsync();

        var employeeToReturn = mapper.Map<EmployeeDto>(employeeEntity);
        return employeeToReturn;
    }

    public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges)
    {
        await CheckIfCompanyExists(companyId, trackChanges);

        var employeeDb = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);

        repository.Employee.DeleteEmployee(employeeDb);
        await repository.SaveAsync();
    }

    public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate,
        bool comTrackChanges,
        bool empTrackChanges)
    {
        await CheckIfCompanyExists(companyId, comTrackChanges);
        var employeeDb = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);
        mapper.Map(employeeForUpdate, employeeDb);
        await repository.SaveAsync();
    }

    public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(
        Guid companyId, Guid id,
        bool compTrackChanges, bool empTrackChanges)
    {
        await CheckIfCompanyExists(companyId, compTrackChanges);
        var employeeDb = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);
        var employeeToPatch = mapper.Map<EmployeeForUpdateDto>(employeeDb);
        return (employeeToPatch, employeeEntity: employeeDb);
    }

    public async Task SaveChangesForPatch(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
    {
        mapper.Map(employeeToPatch, employeeEntity);
        await repository.SaveAsync();
    }

    private async Task CheckIfCompanyExists(Guid companyId, bool trackChanges)
    {
        var company = await repository.Company.GetCompanyAsync(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);
    }

    private async Task<Employee> GetEmployeeForCompanyAndCheckIfItExists(Guid companyId, Guid id, bool trackChanges)
    {
        var employeeDb = await repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
        if (employeeDb is null)
            throw new EmployeeNotFoundException(id);
        return employeeDb;
    }
}