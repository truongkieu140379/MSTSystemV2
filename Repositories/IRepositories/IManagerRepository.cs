using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Global;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Models.ExtendedModels;

namespace TutorSearchSystem.Repositories.IRepositories
{
    public interface IManagerRepository : IGenericRepository<Manager>
    {
        Task<IEnumerable<ExtendedManager>> GetByRoleId(int roleId);
        Task<Manager> GetByEmail(String email);
        Task<PagedList<Manager>> Filter(ManagerParameter parameter);
        Task<IEnumerable<Manager>> GetByStatus(string status, int role);
        Task<IEnumerable<Manager>> GetAllByStatus(string status);
        Task<string> GetManagerEmailByClassHasSubject(int id);
    }
}
