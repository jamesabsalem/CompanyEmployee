using Entities.Responses;

namespace CompanyEmployee.Extensions;

public static class ApiBaseResponseExtensions
{
    public static TResultType GetResult<TResultType>(this ApiBaseResponse apiBaseResponse)
    {
        return ((ApiOkResponse<TResultType>)apiBaseResponse).Result;
    }
}