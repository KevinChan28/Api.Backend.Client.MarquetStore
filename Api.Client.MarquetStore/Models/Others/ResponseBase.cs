namespace Api.Client.MarquetStore.Models.Others
{
    public class ResponseBase
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
    }
}
