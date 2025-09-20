namespace MapperAPI.Common
{
    public class APIResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
       
    }
}

