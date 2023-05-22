using Dal.Tasks.Entities;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.History.dto.Response;

/// <summary>
/// Модель для отдачи всей истории по задаче
/// </summary>
public record AllHistoryByTaskIdResponse
{
    /// <summary>
    /// Вся история
    /// </summary>
    [JsonProperty("allHistory")]
    public List<HistoryDal> AllHistory { get; init; }  
}