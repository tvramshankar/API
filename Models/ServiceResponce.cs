public class ServiceResponce<T>
{
    public T? Data { get; set; }
    public bool IsSucess { get; set; } = true;
    public string Message { get; set; } = string.Empty;
}