using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;
using TutorSearchSystem.Global;
using TutorSearchSystem.Models;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.UnitOfWorks;

namespace TutorSearchSystem.Services
{
    public class TutorTransactionService : ITutorTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TutorTransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<IEnumerable<TutorTransactionDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TutorTransactionDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReportTransactionDto>> GetSumAmount(int year)
        {
            var entities = await _unitOfWork.TutorTransactionRepository.GetSumAmount(year);
            return _mapper.Map<IEnumerable<ReportTransactionDto>>(entities).ToList();
        }

        public async Task<float> GetTotalAmountInMonth()
        {
            return await _unitOfWork.TutorTransactionRepository.GetTotalAmountInMonth();
        }

        public async Task<IEnumerable<ExtendedTutorTransactionDto>> GetTransactionByTutor(int tutorId)
        {
            var entities = await _unitOfWork.TutorTransactionRepository.GetTransactionByTutor(tutorId);
            return _mapper.Map<IEnumerable<ExtendedTutorTransactionDto>>(entities).ToList();
        }

        public Task Inactive(int id)
        {
            throw new NotImplementedException();
        }

        public Task Insert(TutorTransactionDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task PostTutorTransaction(TutorTransactionDto dto)
        {
            var entity = _mapper.Map<TutorTransaction>(dto);
            entity.DateTime = Tools.GetUTC();
            await _unitOfWork.TutorTransactionRepository.PostTutorTransaction(entity);
            await _unitOfWork.Commit();
        }

        public Task Update(TutorTransactionDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
