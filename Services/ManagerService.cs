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
    public class ManagerService : IManagerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManagerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<ManagerDto>> Filter(ManagerParameter parameter)
        {
            var entities = await _unitOfWork.ManagerRepository.Filter(parameter);
            return new Response<ManagerDto>
            {
                CurrentPage = entities.CurrentPage,
                PageSize = entities.PageSize,
                TotalPages = entities.TotalPages,
                TotalCount = entities.TotalCount,
                HasNext = entities.HasNext,
                HasPrevious = entities.HasPrevious,
                Data = entities.Select(m => new ManagerDto
                {
                    Id = m.Id,
                    CreatedDate = m.CreatedDate,
                    CreatedBy = m.CreatedBy,
                    Fullname = m.Fullname,
                    Gender = m.Gender,
                    Birthday = m.Birthday,
                    Email = m.Email,
                    Phone = m.Phone,
                    AvatarImageLink = m.AvatarImageLink,
                    Address = m.Address,
                    RoleId = m.RoleId,
                    Description = m.Description,
                    Status = m.Status,
                }).ToList()
            };
        }

        public async Task<IEnumerable<ManagerDto>> GetAll()
        {
            var entities = await _unitOfWork.ManagerRepository.GetAll();
            return _mapper.Map<IEnumerable<ManagerDto>>(entities).ToList();
        }

        public async Task<ManagerDto> GetByEmail(string email)
        {
            var entity = await _unitOfWork.ManagerRepository.GetByEmail(email);
            return _mapper.Map<ManagerDto>(entity);
        }

        public async Task<ManagerDto> GetById(int id)
        {
            var entity = await _unitOfWork.ManagerRepository.GetById(id);
            return _mapper.Map<ManagerDto>(entity);
        }

        public async Task<IEnumerable<ExtendedManagerDto>> GetByRoleId(int roleId)
        {
            var entities = await _unitOfWork.ManagerRepository.GetByRoleId(roleId);
            return _mapper.Map<IEnumerable<ExtendedManagerDto>>(entities).ToList();
        }

        public async Task<IEnumerable<ManagerDto>> GetByStatus(string status, int role)
        {
            var entities = await _unitOfWork.ManagerRepository.GetByStatus(status, role);
            return _mapper.Map<IEnumerable<ManagerDto>>(entities).ToList();
        }

        public async Task Inactive(int id)
        {
            var entity = await _unitOfWork.ManagerRepository.GetById(id);
            entity.Status = GlobalConstants.INACTIVE_STATUS;
            await _unitOfWork.ManagerRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task Insert(ManagerDto dto)
        {
            var entity = _mapper.Map<Manager>(dto);
            await _unitOfWork.ManagerRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        public async Task Update(ManagerDto dto)
        {
            var entity = _mapper.Map<Manager>(dto);
            await _unitOfWork.ManagerRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task<string> GetManagerEmailBySubject(int subjectId)
        {
            string result = "";
           
                var classHasSubject = await _unitOfWork.ClassSubjectRepository.GetById(subjectId);
            if(classHasSubject != null)
            {
                var subject = await _unitOfWork.SubjectRepository.GetById(classHasSubject.SubjectId);
                var manager = await _unitOfWork.ManagerRepository.GetById(subject.ManageBy);
                result = manager.Email;
            }
            return result;
        }
    }
}
