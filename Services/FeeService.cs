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
    public class FeeService : IFeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<bool> CheckExist(string name)
        {
            return _unitOfWork.FeeRepository.CheckExist(name);
        }

        public async Task<IEnumerable<ExtendedFeeDto>> GetAllExtendedFee()
        {
            var entities = await _unitOfWork.FeeRepository.GetAllExtendedFee();
            return _mapper.Map<IEnumerable<ExtendedFeeDto>>(entities).ToList();
        }

        public async Task<FeeDto> GetById(int id)
        {
            var entity = await _unitOfWork.FeeRepository.GetById(id);
            return _mapper.Map<FeeDto>(entity);
        }

        public async Task<IEnumerable<FeeDto>> GetByStatus(string status)
        {
            var entities = await _unitOfWork.FeeRepository.GetByStatus(status);
            return _mapper.Map<IEnumerable<FeeDto>>(entities).ToList();
        }

        public async Task<ExtendedFeeDto> GetExtendedById(int id)
        {
            var entity = await _unitOfWork.FeeRepository.GetExtendedById(id);
            return _mapper.Map<ExtendedFeeDto>(entity);
        }

        public async Task Inactive(int id)
        {
            var entity = await _unitOfWork.FeeRepository.GetById(id);
            entity.Status = GlobalConstants.INACTIVE_STATUS;
            await _unitOfWork.FeeRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task Insert(FeeDto dto)
        {
            var entity = _mapper.Map<Fee>(dto);
            entity.UpdatedDate = Tools.GetUTC();
            await _unitOfWork.FeeRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        public async Task Update(FeeDto dto)
        {
            var entity = _mapper.Map<Fee>(dto);
            entity.UpdatedDate = Tools.GetUTC();
            await _unitOfWork.FeeRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        Task<IEnumerable<FeeDto>> IBaseService<FeeDto>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
