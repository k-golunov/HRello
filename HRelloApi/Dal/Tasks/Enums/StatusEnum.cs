﻿namespace Dal.Tasks.Enum;

public enum StatusEnum
{
    OnChecking = 0,
    OnRework = 1,
    InWork = 2,
    CompletionCheck = 3,
    Completed = 4,
    AwaitingCancellation = 5,    
    Canceled = 6,
}