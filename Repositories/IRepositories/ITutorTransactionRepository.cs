using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Models.ExtendedModels;

namespace TutorSearchSystem.Repositories.IRepositories
{
    public interface ITutorTransactionRepository : IGenericRepository<TutorTransaction>
    {
        Task PostTutorTransaction(TutorTransaction tutorTransaction);
        Task<IEnumerable<ExtendedTutorTransaction>> GetTransactionByTutor(int tutorId);
        Task<float> GetTotalAmountInMonth();
        Task<IEnumerable<ReportTransaction>> GetSumAmount(int year);
    }
}
