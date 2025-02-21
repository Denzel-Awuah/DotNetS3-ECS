namespace DotNetWebAPIS3.Models
{
    /// <summary>
    /// Custome API Response to return to clients of the API
    /// </summary>
    public class APIResponse
    {
        /// <summary>
        /// The name of the file that was created or modified
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The string message to return in the response
        /// </summary>
        public string Message { get; set; }

        public APIResponse(string filename, string message)
        {
            FileName = filename;
            Message = message;
        }

    }
}
