using CompanyEmployee.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployee.Presentation.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController(IServiceManager service) : ControllerBase
{
    private readonly IServiceManager _service = service;

    [HttpPost("refresh")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
        var tokenDtoToReturn = await _service.AuthenticationService.RefreshToken(tokenDto);
        return Ok(tokenDtoToReturn);
    }
}