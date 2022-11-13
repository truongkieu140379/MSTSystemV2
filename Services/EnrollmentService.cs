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
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EnrollmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CheckFullCourse(int courseId)
        {
            int counter = await _unitOfWork.EnrollmentRepository.CountEnrollmentByCourseId(courseId);
            //
            var course = await _unitOfWork.CourseRepository.GetById(courseId);
            //if course is full
            if (counter >= course.MaxTutee)
            {
                return true;
            }
            return false;
        }

        public async Task<EnrollmentDto> Get(int courseId, int tuteeId)
        {
            var entity = await _unitOfWork.EnrollmentRepository.Get(courseId, tuteeId);
            return _mapper.Map<EnrollmentDto>(entity);
        }

        public async Task<IEnumerable<EnrollmentDto>> GetAll()
        {
            var entities = await _unitOfWork.EnrollmentRepository.GetAll();
            return _mapper.Map<IEnumerable<EnrollmentDto>>(entities).ToList();
        }

        public async Task<EnrollmentDto> GetById(int id)
        {
            var entity = await _unitOfWork.EnrollmentRepository.GetById(id);
            return _mapper.Map<EnrollmentDto>(entity);
        }

        public async Task<IEnumerable<ExtendedEnrollmentDto>> GetEnrollmentsByTutorId(int tutorId, DateTime toDate)
        {
            var entities = await _unitOfWork.EnrollmentRepository.GetEnrollmentsByTutorId(tutorId, toDate);
            return _mapper.Map<IEnumerable<ExtendedEnrollmentDto>>(entities).ToList();
        }

        public async Task Inactive(int id)
        {
            var entity = await _unitOfWork.EnrollmentRepository.GetById(id);
            entity.Status = GlobalConstants.INACTIVE_STATUS;
            await _unitOfWork.EnrollmentRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task Insert(EnrollmentDto dto)
        {
            var entity = _mapper.Map<Enrollment>(dto);
            await _unitOfWork.EnrollmentRepository.Insert(entity);
            await _unitOfWork.Commit();
        }

        public async Task Update(EnrollmentDto dto)
        {
            var entity = _mapper.Map<Enrollment>(dto);
            await _unitOfWork.EnrollmentRepository.Update(entity);
            await _unitOfWork.Commit();
        }
    }
}
