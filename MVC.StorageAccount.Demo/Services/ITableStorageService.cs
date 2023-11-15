using MVC.StorageAccount.Demo.Data;

namespace MVC.StorageAccount.Demo.Services
{
    public interface ITableStorageService
    {
        Task DeleteAttendee(string id, string industry);
        Task<AttendeeEntity> GetAttendee(string id, string industry);
        Task<List<AttendeeEntity>> GetAttendees();
        Task UpsertAttendee(AttendeeEntity attendeeEntity);
    }
}