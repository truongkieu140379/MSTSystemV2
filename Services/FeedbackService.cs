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
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Active(int id, FeedbackDto dto)
        {
            var entity = await _unitOfWork.FeedbackRepository.GetById(id);
            if(entity != null)
            {
                entity.Status = GlobalConstants.ACTIVE_STATUS;
                entity.ConfirmedBy = dto.ConfirmedBy;
                entity.ConfirmedDate = dto.ConfirmedDate;
                await _unitOfWork.FeedbackRepository.Update(entity);
                await _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _unitOfWork.FeedbackRepository.GetById(id);
            if (entity != null)
            {
                if(entity.Status == GlobalConstants.PENDING_STATUS)
                {
                    return await _unitOfWork.FeedbackRepository.Delete(id);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }

        public async Task<IEnumerable<FeedbackDto>> GetAll()
        {
            var entities = await _unitOfWork.FeedbackRepository.GetAll();
            return _mapper.Map<IEnumerable<FeedbackDto>>(entities).ToList();
        }

        public async Task<IEnumerable<ExtendedFeedbackDto>> GetAllExtended()
        {
            var entities = await _unitOfWork.FeedbackRepository.GetAllExtended();
            return _mapper.Map<IEnumerable<ExtendedFeedbackDto>>(entities).ToList();
        }

        public async Task<FeedbackDto> GetById(int id)
        {
            var entity = await _unitOfWork.FeedbackRepository.GetById(id);
            return _mapper.Map<FeedbackDto>(entity);
        }

        public async Task<IEnumerable<ExtendedFeedbackDto>> GetByManagerId(int managerId)
        {
            var entities = await _unitOfWork.FeedbackRepository.GetByManagerId(managerId);
            return _mapper.Map<IEnumerable<ExtendedFeedbackDto>>(entities).ToList();
        }

        public async Task<IEnumerable<ExtendedFeedbackDto>> GetByTutorId(int tutorId)
        {
            var entities = await _unitOfWork.FeedbackRepository.GetByTutorId(tutorId);
            return _mapper.Map<IEnumerable<ExtendedFeedbackDto>>(entities).ToList();
        }

        public async Task Inactive(int id)
        {
            var entity = await _unitOfWork.FeedbackRepository.GetById(id);
            entity.Status = GlobalConstants.INACTIVE_STATUS;
            await _unitOfWork.FeedbackRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task Insert(FeedbackDto dto)
        {
            var entity = _mapper.Map<Feedback>(dto);
            await _unitOfWork.FeedbackRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        public async Task<TutorDto> Search(int tuteeId)
        {
            var entity = await _unitOfWork.FeedbackRepository.Search(tuteeId);
            return _mapper.Map<TutorDto>(entity);
        }

        public async Task Update(FeedbackDto dto)
        {
            var entity = _mapper.Map<Feedback>(dto);
            await _unitOfWork.FeedbackRepository.Update(entity);
            await _unitOfWork.Commit();
        }
    }
}
