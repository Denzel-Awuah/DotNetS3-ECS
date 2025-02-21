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

        /// <summary>
        /// Constructor to create a new APIResponse object
        /// </summary>
        /// <param name="filename">Name of the file</param>
        /// <param name="message">Message to return in response</param>
        public APIResponse(string filename, string message)
        {
            FileName = filename;
            Message = message;
        }

    }
}
