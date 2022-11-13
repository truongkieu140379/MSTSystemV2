using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;

namespace TutorSearchSystem.Services.IService
{
    public interface IFeedbackService : IBaseService<FeedbackDto>
    {
        Task<IEnumerable<ExtendedFeedbackDto>> GetByManagerId(int managerId);
        Task<TutorDto> Search(int tuteeId);
        Task<bool> Delete(int id);
        Task<IEnumerable<ExtendedFeedbackDto>> GetAllExtended();
        Task<IEnumerable<ExtendedFeedbackDto>> GetByTutorId(int tutorId);
        Task<bool> Active(int id, FeedbackDto dto);
    }
}
