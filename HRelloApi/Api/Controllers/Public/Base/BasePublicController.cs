using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Public.Base;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("api/v1/public/[controller]")]
public abstract class BasePublicController: ControllerBase
{
    
}