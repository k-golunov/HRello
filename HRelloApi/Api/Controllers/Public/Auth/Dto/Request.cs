using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HRelloApi.Controllers.Public.Example.Dto;

public class Request
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}