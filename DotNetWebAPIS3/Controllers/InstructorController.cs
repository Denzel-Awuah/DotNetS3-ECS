using Amazon.S3;
using DotNetWebAPIS3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace DotNetWebAPIS3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private static string BUCKET_NAME = "dotnet-webapibucket";
        private IAmazonS3 s3Client;

        public InstructorController(IAmazonS3 s3Client)
        {
            this.s3Client = s3Client;
        }

        //GET /api/instructor
        [HttpGet]
        public async Task<IActionResult> GetInstructorJson()
        {

            //var response = await s3Client.GetObjectAsync(BUCKET_NAME, "Instructors.json");

            //using var stream = response.ResponseStream;
            //using var reader = new StreamReader(stream);
            //string jsonString = await reader.ReadToEndAsync();

            //return Ok(JsonSerializer.Deserialize<List<Instructor>>(jsonString));

            var response = await s3Client.GetObjectAsync(BUCKET_NAME, "Instructors.json");

            using var stream = response.ResponseStream;
            using var reader = new StreamReader(stream);
            string jsonString = await reader.ReadToEndAsync();

         
            IEnumerable<Instructor>? Instructors = JsonConvert.DeserializeObject<IEnumerable<Instructor>>(jsonString);

            return Ok(Instructors);

        }


        //GET /api/instructor/{id}


        //POST /api/instructor


        //PUT /api/instructor/{id}


        //DELETE /api/instructor/{id}


    }
}
