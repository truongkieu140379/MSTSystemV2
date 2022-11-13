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
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CheckExist(string name)
        {
            return await _unitOfWork.ClassRepository.CheckExist(name);
        }

        public async Task Deactive(int classId, int managerId)
        {
            var entity = await _unitOfWork.ClassRepository.GetById(classId);
            entity.Status = GlobalConstants.INACTIVE_STATUS;
            entity.UpdatedBy = managerId;
            entity.UpdatedDate = Tools.GetUTC();
            await _unitOfWork.ClassRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task<Response<ClassDto>> Filter(ClassParameter parameter)
        {
            var entities = await _unitOfWork.ClassRepository.Filter(parameter);
            return new Response<ClassDto>
            {
                CurrentPage = entities.CurrentPage,
                PageSize = entities.PageSize,
                TotalCount = entities.TotalCount,
                TotalPages = entities.TotalPages,
                HasNext = entities.HasNext,
                HasPrevious = entities.HasPrevious,
                Data = entities.Select(c => new ClassDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Status = c.Status,
                    CreatedBy = c.CreatedBy,
                    CreatedDate = c.CreatedDate,
                    Description = c.Description,
                    UpdatedBy = c.UpdatedBy,
                    UpdatedDate = c.UpdatedDate
                }).ToList()
            };
        }

        public async Task<IEnumerable<ClassDto>> GetAll()
        {
            var entities = await _unitOfWork.ClassRepository.GetAll();
            return _mapper.Map<IEnumerable<ClassDto>>(entities).ToList();
        }

        public async Task<IEnumerable<ClassDto>> GetByClassHasSubjectNotExist(int subjectId)
        {
            var classEntities = await _unitOfWork.ClassRepository.GetActive();
            var classHasSubjectEntities = await _unitOfWork.ClassSubjectRepository.GetBySubject(subjectId);
            List<Class> result = new List<Class>();
            bool check = false;
            if (classHasSubjectEntities.Any())
            {
                foreach (var c in classEntities)
                {
                    check = false;
                    foreach (var sb in classHasSubjectEntities)
                    {
                        if (sb.ClassId == c.Id)
                        {
                            check = true;
                            break;
                        }
                    }
                    if (!check)
                    {
                        result.Add(c);
                    }
                }
                return _mapper.Map<IEnumerable<ClassDto>>(result).ToList();
            }
            return _mapper.Map<IEnumerable<ClassDto>>(classEntities).ToList();

        }

        public async Task<ClassDto> GetById(int id)
        {
            var entity = await _unitOfWork.ClassRepository.GetById(id);
            return _mapper.Map<ClassDto>(entity);
        }

        public async Task<ExtendedClassDto> GetExtendedClassById(int id)
        {
            var entity = await _unitOfWork.ClassRepository.GetExtendedClassById(id);
            return _mapper.Map<ExtendedClassDto>(entity);
        }

        public async Task Inactive(int id)
        {
            var entity = await _unitOfWork.ClassRepository.GetById(id);
            entity.Status = GlobalConstants.INACTIVE_STATUS;
            await _unitOfWork.ClassRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task Insert(ClassDto dto)
        {
            var entity = _mapper.Map<Class>(dto);
            entity.UpdatedDate = Tools.GetUTC();
            await _unitOfWork.ClassRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        //get classes by subject id
        public async Task<IEnumerable<ClassDto>> Search(int subjectId, string status)
        {
            var entities = await _unitOfWork.ClassRepository.Search(subjectId, status);
            return _mapper.Map<IEnumerable<ClassDto>>(entities).ToList();
        }

        public async Task Update(ClassDto dto)
        {
            var entity = _mapper.Map<Class>(dto);
            entity.UpdatedDate = Tools.GetUTC();
            await _unitOfWork.ClassRepository.Update(entity);
            await _unitOfWork.Commit();
        }
    }
}
