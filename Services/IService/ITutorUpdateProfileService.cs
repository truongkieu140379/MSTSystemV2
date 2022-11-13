using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;

namespace TutorSearchSystem.Services.IService
{
    public interface ITutorUpdateProfileService : IBaseService<TutorUpdateProfileDto>
    {
        Task<bool> Delete(int id);
    }
}
