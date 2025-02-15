## .NET 8 Web API with S3 and ECS
A .NET 8 Web API that stores and retrieves objects from AWS S3. Web API is deployed to AWS ECS Fargate Cluster.

## Deployment 
Containerized the .NET Web API application using Docker then pushed the image to Elastic Container Registry. The image was then pulled from Elastic Container Service and deployed to Amazon Elastic Container Services (ECS)

The Elastic Container Services Cluster was deployed using AWS Fargate Instances. The Cluster deployment also contained an Application Load Balancer.


## Deployment Strategy
![Application](/AWS-ECS-DotNet-System-Infrastructure.jpg)
