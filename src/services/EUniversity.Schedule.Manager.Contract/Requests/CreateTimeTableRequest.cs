namespace EUniversity.Schedule.Manager.Contract.Requests;

public class CreateTimeTableRequest
{
    public CreateTimeTableRequest() { }

    public CreateTimeTableRequest(List<LessonTimeDetails> lessonTimes)
    {
        LessonTimes = lessonTimes;
    }

    public List<LessonTimeDetails> LessonTimes { get; set; }
}

public class LessonTimeDetails
{
    public LessonTimeDetails() { }

    public LessonTimeDetails(int lessonNumber, TimeOnly startAt, TimeOnly endAt)
    {
        LessonNumber = lessonNumber;
        StartAt = startAt;
        EndAt = endAt;
    }

    public required int LessonNumber { get; set; }
    public required TimeOnly StartAt { get; set; }
    public required TimeOnly EndAt { get; set; }
}