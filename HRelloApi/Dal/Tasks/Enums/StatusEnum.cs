namespace Dal.Tasks.Enum;

public enum Status
{
    ForRevision = 0,
    UnderReview = 1,
    InWork = 2,
    CompletionCheck = 3,
    Completed = 4,
    AwaitingCancellation = 5,    
    Canceled = 6,
}