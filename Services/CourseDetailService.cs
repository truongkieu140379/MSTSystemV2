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
    public class CourseDetailService : ICourseDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseDetailService(IUnitOfWork unit, IMapper mapper)
        {
            _unitOfWork = unit;
            _mapper = mapper;
        }

        public async Task DeleteByCourse(int courseId)
        {
            var entities = await _unitOfWork.CourseDetailRepository.GetByCourse(courseId);
            if (entities.Any())
            {
                foreach(CourseDetail course in entities)
                {
                    await _unitOfWork.CourseDetailRepository.Delete(course);
                }
            }
        }

        public async Task<IEnumerable<CourseDetailDto>> GetAll()
        {
            var entities = await _unitOfWork.CourseDetailRepository.GetAll();
            return _mapper.Map<IEnumerable<CourseDetailDto>>(entities).ToList();
        }

        public async Task<IEnumerable<CourseDetailDto>> GetByCourse(int courseId)
        {
            var entities = await _unitOfWork.CourseDetailRepository.GetByCourse(courseId);
            return _mapper.Map<IEnumerable<CourseDetailDto>>(entities).ToList();
        }

        public async Task<CourseDetailDto> GetById(int id)
        {
            var entity = await _unitOfWork.CourseDetailRepository.GetById(id);
            return _mapper.Map<CourseDetailDto>(entity);
        }

        public async Task Inactive(int id)
        {
            throw new Exception();
        }

        public async Task Insert(CourseDetailDto dto)
        {
            var entity = _mapper.Map<CourseDetail>(dto);
            await _unitOfWork.CourseDetailRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        public async Task Update(CourseDetailDto dto)
        {
            var entity = _mapper.Map<CourseDetail>(dto);
            await _unitOfWork.CourseDetailRepository.Update(entity);
            await _unitOfWork.Commit();
        }
    }
}
