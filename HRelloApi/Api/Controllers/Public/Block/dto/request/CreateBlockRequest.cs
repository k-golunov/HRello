﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Block.dto.request;

/// <summary>
/// модель данных на запрос создания блока задач
/// </summary>
public record CreateBlockRequest
{
    /// <summary>
    /// значение блока
    /// </summary>
    [Required]
    [JsonProperty("Value")]
    public required string Value { get; init; }
}