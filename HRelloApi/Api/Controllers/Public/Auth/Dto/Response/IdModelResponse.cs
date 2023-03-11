using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Auth.Dto.Response;

/// <summary>
/// Модель ответа на запрос создания пользователя
/// </summary>
public record IdModelResponse
{
    /// <summary>
    /// Id созданного пользователя
    /// </summary>
    [Required]
    [JsonProperty("Id")]
    public Guid Id;

    public IdModelResponse(Guid id)
    {
        Id = id;
    }
}