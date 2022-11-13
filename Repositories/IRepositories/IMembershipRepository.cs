using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Models.ExtendedModels;

namespace TutorSearchSystem.Repositories.IRepositories
{

    public interface IMembershipRepository : IGenericRepository<Membership>
    {
        Task<IEnumerable<ExtendedMembership>> GetAllExtendedMembership();
        Task<IEnumerable<Membership>> GetByStatus(String status);
        Task<bool> CheckExist(string name);
        Task<ExtendedMembership> GetExtendedById(int id);
    }
}
