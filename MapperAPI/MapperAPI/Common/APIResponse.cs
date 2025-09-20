namespace MapperAPI.Common
{
    public class APIResponse<T>
    {
        public T? data { get; set; }
        public bool IsSuccess { get; set; }
        public string? errorMessage { get; set; }
       
    }
}

