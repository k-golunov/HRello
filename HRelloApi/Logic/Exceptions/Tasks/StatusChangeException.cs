﻿using System.Net;
using Logic.Exceptions.Base;

namespace Logic.Exceptions.Tasks;

/// <summary>
/// Ошибка при смене статута задачи
/// </summary>
public class StatusChangeException : BaseException
{
    public StatusChangeException(string currentStatus, string nextStatus) : base("StatusChangeException", $"Невозможно сменить статус {currentStatus} на статус {nextStatus}", 400)
    {
        
    }
}