using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Example.Dto.Request;

/// <summary>
/// 
/// </summary>
public record CreateUserModelRequest
{
    [Required]
    [EmailAddress]
    [JsonProperty("Email")]
    public string Email { get; init; }
    
    [Required]
    [JsonProperty("UserName")]
    public string UserName { get; init; } 
    
    [Required]
    [JsonProperty("DepartmentId")]
    public int DepartamentId { get; init; }
}