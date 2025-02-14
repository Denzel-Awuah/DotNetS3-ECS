namespace DotNetWebAPIS3.Models
{
    public class APIResponse
    {
        public string FileName { get; set; }

        public string Message { get; set; }

        public APIResponse(string filename, string message)
        {
            FileName = filename;
            Message = message;
        }

    }
}
