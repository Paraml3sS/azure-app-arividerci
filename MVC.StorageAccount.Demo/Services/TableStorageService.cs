using Azure;
using Azure.Data.Tables;
using MVC.StorageAccount.Demo.Data;

namespace MVC.StorageAccount.Demo.Services
{
    public class TableStorageService : ITableStorageService
    {
        private readonly IConfiguration _configuration;
        private const string TableName = "Attendees";

        public TableStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<AttendeeEntity> GetAttendee(string id, string industry)
        {
            var tableClient = await GetTableClient();
            return await tableClient.GetEntityAsync<AttendeeEntity>(industry, id);
        }

        public async Task<List<AttendeeEntity>> GetAttendees()
        {
            var tableClient = await GetTableClient();
            Pageable<AttendeeEntity> attendeeEntities = tableClient.Query<AttendeeEntity>();
            return attendeeEntities.ToList();
        }

        public async Task UpsertAttendee(AttendeeEntity attendeeEntity)
        {
            var tableClient = await GetTableClient();
            await tableClient.UpsertEntityAsync(attendeeEntity);
        }

        public async Task DeleteAttendee(string id, string industry)
        {
            var tableClient = await GetTableClient();
            await tableClient.DeleteEntityAsync(industry, id);
        }

        private async Task<TableClient> GetTableClient()
        {
            var serviceClient = new TableServiceClient(_configuration["StorageConnectionString"]);

            var tableClient = serviceClient.GetTableClient(TableName);
            await tableClient.CreateIfNotExistsAsync();

            return tableClient;
        }
    }
}
