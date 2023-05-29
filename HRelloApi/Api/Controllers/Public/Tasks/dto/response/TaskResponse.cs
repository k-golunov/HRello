using System.ComponentModel.DataAnnotations;
using Dal.Tasks.Enum;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;

namespace HRelloApi.Controllers.Public.Tasks.dto.response;

/// <summary>
/// Модель данных задачи для ответа 
/// </summary>
public record TaskResponse
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    [Required]
    [JsonProperty("id")]
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Название задачи
    /// </summary>
    [Required]
    [JsonProperty("Name")]
    [MaxLength(255)]
    public required string Name { get; init; }
    
    /// <summary>
    /// Год задачи
    /// </summary>
    [Required]
    [JsonProperty("Year")]
    [MaxLength(4)]
    [MinLength(4)]
    public required int Year { get; init; }
    
    /// <summary>
    /// Квартал задачи, от 1 до 4
    /// </summary>
    [Required]
    [JsonProperty("Quarter")]
    [Range(1,4)]
    public required int Quarter { get; init; }
    
    /// <summary>
    /// Категория задачи enum
    /// </summary>
    [Required]
    [JsonProperty("Category")]
    public required CategoryEnum Category { get; init; }
    
    /// <summary>
    /// Блок задачи enum
    /// </summary>
    [Required]
    [JsonProperty("Block")]
    public required string Block { get; init; }
    
    /// <summary>
    /// Планируемый вес задачи, предполагаектся значение процента
    /// Например если значение 50, то это означает, что вес 50%
    /// </summary>
    [Range(0, 1000)]
    [Required]
    [JsonProperty("PlannedWeight")]
    public required int PlannedWeight { get; init; }
    
    /// <summary>
    /// Ожидаемый результат по итогам задачи
    /// </summary>
    [MaxLength(1023)]
    [Required]
    [JsonProperty("WaitResult")]
    public required string WaitResult { get; init; }
    
    /// <summary>
    /// Статус задачи
    /// </summary>
    [Required]
    [JsonProperty("Status")]
    public required StatusEnum Status { get; init; }
    
    [Required]
    [JsonProperty("UserId")]
    public required Guid UserId { get; init; }
    
    [Required]
    [JsonProperty("DepartmentId")]
    public required int DepartmentId { get; init; }
    
    [Required]
    [JsonProperty("UserResult")]
    public required UserResultResponse? UserResult { get; set; }
    
    [Required]
    [JsonProperty("BossResult")]
    public required BossResultResponse? BossResult { get; set; }
}