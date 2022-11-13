using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Models.ExtendedModels;

namespace TutorSearchSystem.Repositories.IRepositories
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        Task<IEnumerable<ExtendedFeedback>> GetByManagerId(int managerId);
        Task<Tutor> Search(int tuteeId);
        Task<IEnumerable<ExtendedFeedback>> GetAllExtended();
        Task<IEnumerable<ExtendedFeedback>> GetByTutorId(int tutorId);
        Task<double> GetAverageRatingStateByTutorId(int tutorId);
        Task<bool> Delete(int id);
    }
}
