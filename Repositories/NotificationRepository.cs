using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Repositories.IRepositories;

namespace TutorSearchSystem.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        private readonly TSDbContext _context;
        public NotificationRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetNotificationByEmail(string email)
        {
            return await _context.Notification.Where( n => n.SendToUser == email).OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }
    }
}
