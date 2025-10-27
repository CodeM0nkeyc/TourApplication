namespace TourApp.Application.Models;

public class Result<T>
{
    public T Data { get; set; }
    public Dictionary<string, string> Errors { get; } = new Dictionary<string, string>();
}