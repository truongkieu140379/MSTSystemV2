using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;

namespace TutorSearchSystem.Services.IService
{
    public interface ITutorTransactionService : IBaseService<TutorTransactionDto>
    {
        Task PostTutorTransaction(TutorTransactionDto dto);
        Task<IEnumerable<ExtendedTutorTransactionDto>> GetTransactionByTutor(int tutorId);
        Task<float> GetTotalAmountInMonth();

        Task<IEnumerable<ReportTransactionDto>> GetSumAmount(int year);
    }
}
