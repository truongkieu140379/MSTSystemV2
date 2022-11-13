using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;

namespace TutorSearchSystem.Services.IService
{
    public interface IMembershipService : IBaseService<MembershipDto>
    {
        Task<IEnumerable<ExtendedMembershipDto>> GetAllExtendedMembership();
        Task<IEnumerable<MembershipDto>> GetByStatus(String status);
        Task<bool> CheckExist(string name);
        Task<ExtendedMembershipDto> GetExtendedById(int id);
    }
}
