using Azure.Storage.Blobs;

namespace MVC.StorageAccount.Demo.Services
{
    public class BlobStorageService
    {
        private readonly IConfiguration _configuration;
        private string containerName = "attendeeimages";

        public BlobStorageService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        private async Task<BlobContainerClient> GetBlobContainerClient()
        {
            try
            {
                var container = new BlobContainerClient(_configuration["StorageConnectionString"], containerName);
                await container.CreateIfNotExistsAsync();

                return container;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
