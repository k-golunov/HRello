using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Example.Dto.Request;

public record SignInModelRequest
{
    [Required]
    [EmailAddress]
    [JsonProperty("Email")]
    public string Email { get; init; }
    
    [Required]
    [JsonProperty("Password")]
    public string Password { get; set; }
}