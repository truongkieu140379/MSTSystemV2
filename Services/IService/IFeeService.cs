using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;

namespace TutorSearchSystem.Services.IService
{
    public interface IFeeService : IBaseService<FeeDto>
    {
        Task<IEnumerable<FeeDto>> GetByStatus(String status);
        Task<IEnumerable<ExtendedFeeDto>> GetAllExtendedFee();
        Task<bool> CheckExist(string name);
        Task<ExtendedFeeDto> GetExtendedById(int id);
    }
}
