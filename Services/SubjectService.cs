using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Global;
using TutorSearchSystem.Models;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.UnitOfWorks;

namespace TutorSearchSystem.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Active(int id, int managerId)
        {
            var entity = await _unitOfWork.SubjectRepository.GetById(id);
            if(entity != null)
            {
                entity.Status = GlobalConstants.ACTIVE_STATUS;
                entity.UpdatedBy = managerId;
                entity.UpdatedDate = Tools.GetUTC();
                await _unitOfWork.SubjectRepository.Update(entity);
                await _unitOfWork.Commit();
            }
        }

        public Task<bool> CheckExist(string name)
        {
            return _unitOfWork.SubjectRepository.CheckExist(name);
        }

        public async Task Deactive(int subjectId, int managerId)
        {
            var entity = await _unitOfWork.SubjectRepository.GetById(subjectId);
            entity.Status = GlobalConstants.INACTIVE_STATUS;
            entity.UpdatedBy = managerId;
            entity.UpdatedDate = Tools.GetUTC();
            await _unitOfWork.SubjectRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task<Response<ExtendedSubjectDto>> Filter(SubjectParameter parameter)
        {
            var entities = await _unitOfWork.SubjectRepository.Filter(parameter);
            return new Response<ExtendedSubjectDto>
            {
                CurrentPage = entities.CurrentPage,
                PageSize = entities.PageSize,
                TotalPages = entities.TotalPages,
                TotalCount = entities.TotalCount,
                HasNext = entities.HasNext,
                HasPrevious = entities.HasPrevious,
                Data = entities.Select(s => new ExtendedSubjectDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    CreatedDate = s.CreatedDate,
                    Description = s.Description,
                    ManagerName = s.ManagerName,
                    Status = s.Status
                }).ToList()
            };
        }

        public async Task<IEnumerable<SubjectDto>> GetAll()
        {
            var entities = await _unitOfWork.SubjectRepository.GetAll();
            return _mapper.Map<IEnumerable<SubjectDto>>(entities).ToList();
        }

        public async Task<IEnumerable<ExtendedSubjectDto>> GetAllExtendedSubject()
        {
            var entities = await _unitOfWork.SubjectRepository.GetAllExtendedSubject();
            return _mapper.Map<IEnumerable<ExtendedSubjectDto>>(entities).ToList();
        }

        public async Task<SubjectDto> GetById(int id)
        {
            var entity = await _unitOfWork.SubjectRepository.GetById(id);
            return _mapper.Map<SubjectDto>(entity);
        }

        public async Task<IEnumerable<SubjectDto>> GetByManagerId(int managerId)
        {
            var entities = await _unitOfWork.SubjectRepository.GetByManagerId(managerId);
            return _mapper.Map<IEnumerable<SubjectDto>>(entities).ToList();
        }

        public async Task<IEnumerable<SubjectDto>> GetByStatus(string status)
        {
            var entities = await _unitOfWork.SubjectRepository.GetByStatus(status);
            return _mapper.Map<IEnumerable<SubjectDto>>(entities).ToList();
        }

        public async Task<IEnumerable<SubjectDto>> GetByTutor(int tutorId)
        {
            var entities = await _unitOfWork.SubjectRepository.GetByTutor(tutorId);
            return _mapper.Map<IEnumerable<SubjectDto>>(entities).ToList();
        }

        public async Task<ExtendedSubjectDto> GetExtendedSubjectById(int id)
        {
            var entity = await _unitOfWork.SubjectRepository.GetExtendedSubjectById(id);
            return _mapper.Map<ExtendedSubjectDto>(entity);
        }

        public async Task Inactive(int id)
        {
            var entity = await _unitOfWork.SubjectRepository.GetById(id);
            entity.Status = GlobalConstants.INACTIVE_STATUS;
            await _unitOfWork.SubjectRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task Insert(SubjectDto dto)
        {
            var entity = _mapper.Map<Subject>(dto);
            entity.UpdatedDate = Tools.GetUTC();
            await _unitOfWork.SubjectRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        public async Task Update(SubjectDto dto)
        {
            var entity = _mapper.Map<Subject>(dto);
            entity.UpdatedDate = Tools.GetUTC();
            await _unitOfWork.SubjectRepository.Update(entity);
            await _unitOfWork.Commit();
        }
    }
}
