using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Models.ExtendedModels;

namespace TutorSearchSystem.Repositories.IRepositories
{
    public interface IFeeRepository : IGenericRepository<Fee>
    {
        Task<bool> CheckExist(String name);
        Task<IEnumerable<Fee>> GetByStatus(String status);
        Task<IEnumerable<ExtendedFee>> GetAllExtendedFee();
        Task<ExtendedFee> GetExtendedById(int id);
    }
}
