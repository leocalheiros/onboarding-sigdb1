namespace OnboardingSIGDB1.Domain.Entities.Common;

public class BaseResponse<T>
{
    public List<string> Errors { get; set; }
    public T Data { get; set; }
}