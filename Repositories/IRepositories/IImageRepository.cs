using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;

namespace TutorSearchSystem.Repositories.IRepositories
{
    public interface IImageRepository : IGenericRepository<Image>
    {
        Task Delete(int id);
        Task<string[]> Get(string ownerEmail, string imageType);
        Task DeleteByOwnerEmail(string email);
    }
}
