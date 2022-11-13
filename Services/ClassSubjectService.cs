using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;
using TutorSearchSystem.Models;
using TutorSearchSystem.Global;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.UnitOfWorks;
using TutorSearchSystem.Filters;

namespace TutorSearchSystem.Services
{
    public class ClassSubjectService : IClassSubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassSubjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Message> Active(int id)
        {
            var entity = await _unitOfWork.ClassSubjectRepository.GetById(id);
            if (entity == null)
            {
                return new Message { Title= "Error",
                Content= "Class-Subject ID not found",
                Type = "error"};
            }
            var classEntity = await _unitOfWork.ClassRepository.GetById(entity.ClassId);
            var subjectEntity = await _unitOfWork.SubjectRepository.GetById(entity.SubjectId);
            if(classEntity.Status == GlobalConstants.ACTIVE_STATUS && subjectEntity.Status == GlobalConstants.ACTIVE_STATUS)
            {
                entity.Status = GlobalConstants.ACTIVE_STATUS;
                await _unitOfWork.ClassSubjectRepository.Update(entity);
                await _unitOfWork.Commit();
                return new Message
                {
                    Title = "Success",
                    Content = "Update successful",
                    Type = "success"
                };
            }
            else
            {
                return new Message
                {
                    Title = "Warning",
                    Content = "Can not activate because class or subject has been inactived",
                    Type = "info"
                };
            }         
            
        }

        public async Task<int> CheckByClass(int id)
        {
            return await _unitOfWork.ClassSubjectRepository.CheckByClass(id);
        }

        public async Task<int> CheckBySubject(int id)
        {
            return await _unitOfWork.ClassSubjectRepository.CheckBySubject(id);
        }

        public async Task<Response<ExtendedClassHasSubjectDto>> Filter(ClassHasSubjectParameter parameter)
        {
            var entities = await _unitOfWork.ClassSubjectRepository.Filter(parameter);
            return new Response<ExtendedClassHasSubjectDto>
            {
                CurrentPage = entities.CurrentPage,
                PageSize = entities.PageSize,
                TotalCount = entities.TotalCount,
                TotalPages = entities.TotalPages,
                HasNext = entities.HasNext,
                HasPrevious = entities.HasPrevious,
                Data = entities.Select(c => new ExtendedClassHasSubjectDto
                {
                    Id = c.Id,
                    SubjectName = c.SubjectName,
                    ClassName = c.ClassName,
                    Status = c.Status,
                    CreatedDate = c.CreatedDate
                }).ToList()
            };
        }

        public async Task<IEnumerable<ExtendedClassHasSubjectDto>> GetAllExteded()
        {
            var entities = await _unitOfWork.ClassSubjectRepository.GetAllExteded();
            return _mapper.Map<IEnumerable<ExtendedClassHasSubjectDto>>(entities);
        }

        public async Task<IEnumerable<ExtendedClassHasSubjectDto>> GetBySubject(int subjectId)
        {
            var entities = await _unitOfWork.ClassSubjectRepository.GetBySubject(subjectId);
            return _mapper.Map<IEnumerable<ExtendedClassHasSubjectDto>>(entities);
        }

        public async Task<bool> Inactive(int id)
        {
            var entity = await _unitOfWork.ClassSubjectRepository.GetById(id);
            if(entity == null)
            {
                return false;
            }
            entity.Status = GlobalConstants.INACTIVE_STATUS;
            await _unitOfWork.ClassSubjectRepository.Update(entity);
            await _unitOfWork.Commit();
            return true;
        }

        public async Task Insert(ClassHasSubjectDto dto)
        {
            var entity = _mapper.Map<ClassHasSubject>(dto);
            await _unitOfWork.ClassSubjectRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        public async Task<ClassHasSubjectDto> Search(int subjectId, int classId)
        {
            var entities = await _unitOfWork.ClassSubjectRepository.Search(subjectId, classId);
            return _mapper.Map<ClassHasSubjectDto>(entities);
        }
    }
}
