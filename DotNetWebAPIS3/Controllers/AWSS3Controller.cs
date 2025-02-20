using Amazon.S3.Model;
using Amazon.S3.Util;
using Amazon.S3;
using DotNetWebAPIS3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetWebAPIS3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AWSS3Controller : ControllerBase
    {

        private readonly IAmazonS3 s3Client;
        private readonly string BUCKET_NAME = "dotnet-webapibucket";

        public AWSS3Controller(IAmazonS3 s3Client)
        {
            this.s3Client = s3Client;
        }


        [HttpGet("file")]
        public async Task<IActionResult> File(string fileName)
        {
            var response = await s3Client.GetObjectAsync(BUCKET_NAME, fileName);
            return File(response.ResponseStream, response.Headers.ContentType);
        }


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
