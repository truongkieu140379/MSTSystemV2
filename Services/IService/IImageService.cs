using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;

namespace TutorSearchSystem.Services.IService
{
    public interface IImageService : IBaseService<ImageDto>
    {
        Task Delete(int id);
        Task<string[]> Get(string ownerEmail, string imageType);
        Task DeleteByOwnerEmail(string email);
    }
}
