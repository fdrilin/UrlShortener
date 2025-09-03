namespace UrlShortener.Models
{
    public class ResultItem
    {
        public string Message { get; set; }

        public ResultItem(string message)
        {
            Message = message;
        }
    }
}
