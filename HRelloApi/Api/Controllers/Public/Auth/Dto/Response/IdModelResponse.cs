using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Example.Dto.Response;

/// <summary>
/// 
/// </summary>
public record IdModelResponse
{
    [Required]
    [JsonProperty("Id")]
    public Guid Id;

    public IdModelResponse(Guid id)
    {
        Id = id;
    }
}