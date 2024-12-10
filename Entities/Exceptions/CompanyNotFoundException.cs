namespace Entities.Exceptions;

public sealed class CompanyNotFoundException(Guid companyId)
    : NotFoundException($"The company with id: {companyId} doesn't exit it the database.");