using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;

namespace TutorSearchSystem.Services.IService
{
    public interface INotificationService : IBaseService<NotificationDto>
    {
        Task<IEnumerable<NotificationDto>> GetNotificationByEmail(string email);
        Task SendNotificationToAllManager(string title, string message);
        Task SendNotificationToAdmin(string title, string message);
    }
}
