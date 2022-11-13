using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Models;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.UnitOfWorks;

namespace TutorSearchSystem.Services
{
    public class TutorUpdateProfileService : ITutorUpdateProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TutorUpdateProfileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _unitOfWork.TutorUpdateProfileRepository.GetById(id);
            if(entity != null)
            {
                await _unitOfWork.TutorUpdateProfileRepository.Delete(id);
                await _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<TutorUpdateProfileDto>> GetAll()
        {
            var entities = await _unitOfWork.TutorUpdateProfileRepository.GetAll();
            return _mapper.Map<IEnumerable<TutorUpdateProfileDto>>(entities).ToList();
        }

        public async Task<TutorUpdateProfileDto> GetById(int id)
        {
            var entity = await _unitOfWork.TutorUpdateProfileRepository.GetById(id);
            return _mapper.Map<TutorUpdateProfileDto>(entity);
        }

        public async Task Inactive(int id)
        {
            var entity = await _unitOfWork.TutorUpdateProfileRepository.GetById(id);
            await _unitOfWork.TutorUpdateProfileRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task Insert(TutorUpdateProfileDto dto)
        {
            var entity = _mapper.Map<TutorUpdateProfile>(dto);
            await _unitOfWork.TutorUpdateProfileRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        public async Task Update(TutorUpdateProfileDto dto)
        {
            var entity = _mapper.Map<TutorUpdateProfile>(dto);
            await _unitOfWork.TutorUpdateProfileRepository.Update(entity);
            await _unitOfWork.Commit();
        }
    }
}
