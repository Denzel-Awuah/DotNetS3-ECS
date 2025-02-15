using Amazon.S3;
using Amazon.S3.Model;
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
        [HttpPost]
        public async Task<IActionResult> GetInstructorJson(Instructor instructor)
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

         
            List<Instructor>? Instructors = JsonConvert.DeserializeObject<List<Instructor>>(jsonString);

            Instructors?.Add(instructor);

            var putObjectRequest = new PutObjectRequest
            {
                BucketName = BUCKET_NAME,
                Key = "Instructors.json",
                InputStream = TransformInputStreamFromString(JsonConvert.SerializeObject(Instructors))
            };
            var putObjectResponse = await s3Client.PutObjectAsync(putObjectRequest);
            return Ok(Instructors);

        }

        private static Stream TransformInputStreamFromString(string instructorsString)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(instructorsString);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


        //GET /api/instructor/{id}


        //POST /api/instructor


        //PUT /api/instructor/{id}


        //DELETE /api/instructor/{id}


    }
}
