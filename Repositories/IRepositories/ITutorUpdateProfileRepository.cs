using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;

namespace TutorSearchSystem.Repositories.IRepositories
{
    public interface ITutorUpdateProfileRepository
    {
        Task Delete(int id);
        Task<TutorUpdateProfile> GetByEmail(string email);
    }
}
