namespace Entities.Exceptions;

public class EmployeeNotFoundException(Guid employeeId)
    : NotFoundException($"Employee with id: {employeeId} doesn't exit in the database.");