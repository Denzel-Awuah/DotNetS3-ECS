using Amazon.S3.Model;
using Amazon.S3.Util;
using Amazon.S3;
using DotNetWebAPIS3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetWebAPIS3.Controllers
{

    /// <summary>
    /// Controller to facilitate communications with an AWS S3 service.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AWSS3Controller : ControllerBase
    {

        /// <summary>
        /// The client service that is used to communicate with AWS S3.
        /// </summary>
        private readonly IAmazonS3 s3Client;

        /// <summary>
        /// The name of the storage unit within AWS S3
        /// </summary>
        private readonly string BUCKET_NAME = "dotnet-webapibucket";


        /// <summary>
        /// Contructor to initilize the API controller and its dependencies.
        /// </summary>
        /// <param name="s3Client">Service for handling commuinication with AWS S3</param>
        public AWSS3Controller(IAmazonS3 s3Client)
        {
            this.s3Client = s3Client;
        }


        /// <summary>
        /// Gets a file from AWS S3 by file name.
        /// </summary>
        /// <param name="fileName">The name of the file in AWS S3</param>
        /// <returns>Returns the contents of the file</returns>
        [HttpGet("file")]
        public async Task<IActionResult> File(string fileName)
        {
            var response = await s3Client.GetObjectAsync(BUCKET_NAME, fileName);
            return File(response.ResponseStream, response.Headers.ContentType);
        }

        /// <summary>
        /// Creates a new bucket in AWS S3
        /// </summary>
        /// <param name="name">The name of the bucket</param>
        /// <returns>An APIResponse object with the name of the new object create in AWS S3</returns>
        [HttpPost("bucket")]
        public async Task<IActionResult> PostBucket(string name)
        {

            var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(s3Client, name);

            var bucketRequest = new PutBucketRequest()
            {
                BucketName = name,
                UseClientRegion = true
            };

            if (!bucketExists)
            {
                var bucketResponse = await s3Client.PutBucketAsync(bucketRequest);
            }

            return Ok(new APIResponse(name, "Bucket added to AWS S3"));
        }


        /// <summary>
        /// Gets a list of all the files within a bucket in AWS S3
        /// </summary>
        /// <param name="prefix">A prefix to apply to the objects being retrieved</param>
        /// <returns>All the objects in the bucket</returns>
        [HttpGet("files")]
        public async Task<IActionResult> Files(string? prefix)
        {
            var listObjectsRequest = new ListObjectsV2Request()
            {
                BucketName = BUCKET_NAME,
                Prefix = prefix
            };
            var listObjectsResponse = await s3Client.ListObjectsV2Async(listObjectsRequest);
            return Ok(listObjectsResponse.S3Objects);
        }

    
        [HttpPost("object")]
        public async Task<IActionResult> PostObject(string? inputBucketName, IFormFile file)
        {
            var putObjectRequest = new PutObjectRequest()
            {
                BucketName = inputBucketName ?? BUCKET_NAME,
                Key = file.FileName,
                InputStream = file.OpenReadStream()
            };
            var putObjectResponse = await s3Client.PutObjectAsync(putObjectRequest);
            return Ok(new APIResponse(file.FileName, "Upload complete"));
        }
    }
}
