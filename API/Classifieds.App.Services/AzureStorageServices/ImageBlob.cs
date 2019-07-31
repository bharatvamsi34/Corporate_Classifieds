using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;


namespace Classifieds.App.Services.AzureStorageServices
{
    public class ImageBlob
    {
        private readonly IConfiguration configuration;
        public ImageBlob(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> ImageUploading(string imageBase64String)
        {
            string destination = Guid.NewGuid() + ".jpg";
            string storageConnectionString = configuration["BlobStorage"];
            CloudStorageAccount storageAccount = null;

            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = cloudBlobClient.GetContainerReference(configuration["container"]);
                    await container.CreateIfNotExistsAsync();
                    using (var memoryStream = new MemoryStream(Convert.FromBase64String(imageBase64String)))
                    {
                        Image.FromStream(memoryStream).Save(destination);
                    }
                    CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(destination);
                    await cloudBlockBlob.UploadFromFileAsync(destination);
                    return cloudBlockBlob.Uri.AbsoluteUri;
                }
                finally
                {
                    File.Delete(destination);
                }
            }
            else
            {
                Console.WriteLine(" Error occured while connecting to the CloudStorageAccount ");
            }
            return null;
        }
        public void BlobImageDelete(string data)
        {
            string storageConnectionString = configuration["BlobStorage"];
            CloudStorageAccount storageAccount = null;

            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = cloudBlobClient.GetContainerReference(configuration["container"]);
                    container.GetBlockBlobReference(data).DeleteIfExists();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                Console.WriteLine(" Error occured while connecting to the CloudStorageAccount ");
            }
        }
    }
}