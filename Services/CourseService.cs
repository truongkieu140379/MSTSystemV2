using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Global;
using TutorSearchSystem.GoogleMapHelpers;
using TutorSearchSystem.Models;
using TutorSearchSystem.Models.ExtendedModels;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.UnitOfWorks;

namespace TutorSearchSystem.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseDto>> Get(int tutor, string status)
        {
            var entities = await _unitOfWork.CourseRepository.Get(tutor, status);
            return _mapper.Map<IEnumerable<CourseDto>>(entities).ToList();
        }

        public async Task<IEnumerable<CourseTutorDto>> GetActiveUnregisteredCourses(int tuteeId)
        {
            IEnumerable<CourseTutor> entities = await _unitOfWork.CourseRepository.GetActiveUnregisteredCourses(tuteeId);
            //
            foreach (var c in entities)
            {
                //
                var quantityEnrollment = await _unitOfWork.EnrollmentRepository.CountEnrollmentByCourseIdWithStatus(GlobalConstants.ACTIVE_STATUS,c.Id);
                var availableSlot = c.MaxTutee - quantityEnrollment;
                c.AvailableSlot = availableSlot;
                //set avg rating
                var avg = await _unitOfWork.FeedbackRepository.GetAverageRatingStateByTutorId(c.CreatedBy);
                c.AverageRatingStar = avg;
                //set distance
                //GoogleMapHandler googleMapHelpers = new GoogleMapHandler();
                //var distance = googleMapHelpers.GetDistanceByAddresses(originAddress, c.Location);
                //c.Distance = distance;
            }
            //
            return _mapper.Map<IEnumerable<CourseTutorDto>>(entities).ToList();
        }

        public async Task<IEnumerable<CourseDto>> GetAll()
        {
            var entities = await _unitOfWork.CourseRepository.GetAll();
            return _mapper.Map<IEnumerable<CourseDto>>(entities).ToList();
        }

        public async Task<CourseDto> GetById(int id)
        {
            var entity = await _unitOfWork.CourseRepository.GetById(id);
            return _mapper.Map<CourseDto>(entity);
        }

        public async Task<ExtendedCourseDto> GetExtendedCourse(int id, int tuteeId)
        {
            Course course = await _unitOfWork.CourseRepository.GetById(id);
            //
            ClassHasSubject classHasSubject = await _unitOfWork.ClassSubjectRepository.GetById(course.ClassHasSubjectId);
            Class classes = await _unitOfWork.ClassRepository.GetById(classHasSubject.ClassId);
            Subject subject = await _unitOfWork.SubjectRepository.GetById(classHasSubject.SubjectId);
            Tutor tutor = await _unitOfWork.TutorRepository.GetById(course.CreatedBy);
            //
            Enrollment enrollment = null;
            if (tuteeId != 0)
            {
                enrollment = await _unitOfWork.EnrollmentRepository.Get(course.Id, tuteeId);
            }

            //
            var quantityEnrollment = await _unitOfWork.EnrollmentRepository.CountEnrollmentByCourseIdWithStatus(GlobalConstants.ACTIVE_STATUS ,course.Id);
            var availableSlot = course.MaxTutee - quantityEnrollment;
            //
            return new ExtendedCourseDto
            {
                ExtraImages = course.ExtraImages,
                Location = course.Location,
                Id = course.Id,
                Name = course.Name,
                BeginTime = course.BeginTime,
                EndTime = course.EndTime,
                StudyFee = course.StudyFee,
                DaysInWeek = course.DaysInWeek,
                BeginDate = course.BeginDate,
                EndDate = course.EndDate,
                ClassHasSubjectId = course.ClassHasSubjectId,
                ConfirmedBy = course.ConfirmedBy,
                ConfirmedDate = course.ConfirmedDate,
                CreatedBy = course.CreatedBy,
                CreatedDate = course.CreatedDate,
                MaxTutee = course.MaxTutee,
                Description = course.Description,
                Status = course.Status,
                SubjectName = subject.Name,
                ClassName = classes.Name,
                TutorAvatarUrl = tutor.AvatarImageLink,
                TutorName = tutor.Fullname,
                TutorAddress = tutor.Address,
                EnrollmentStatus = enrollment?.Status,
                FollowDate = enrollment?.CreatedDate,
                EnrollmentId = enrollment?.Id,
                AvailableSlot = availableSlot,
                IsTakeFeedback = enrollment?.IsTakeFeedback,
                TutorEmail = tutor.Email,
                Precondition = course.Precondition
            };
        }

        public async Task<IEnumerable<ExtendedCourseDto>> GetCoursesByManagerId(int managerId)
        {
            var entities = await _unitOfWork.CourseRepository.GetCoursesByManagerId(managerId);
            return _mapper.Map<IEnumerable<ExtendedCourseDto>>(entities).ToList();

        }

        public async Task Inactive(int id)
        {
            var entity = await _unitOfWork.CourseRepository.GetById(id);
            entity.Status = GlobalConstants.INACTIVE_STATUS;
            await _unitOfWork.CourseRepository.Update(entity);
            await _unitOfWork.Commit();
        }

        public async Task Insert(CourseDto dto)
        {
            var entity = _mapper.Map<Course>(dto);
            await _unitOfWork.CourseRepository.Insert(entity);
            await _unitOfWork.Commit();
            string email = await _unitOfWork.ManagerRepository.GetManagerEmailByClassHasSubject(dto.ClassHasSubjectId);
            if (!String.IsNullOrEmpty(email))
            {
                NotificationDto noti = new NotificationDto
                {
                    Title = "Confirm Course Request",
                    Message = "You has a new course request",
                    IsRead = false,
                    SendToUser = email
                };
                INotificationService notificationService = new NotificationService(_unitOfWork, _mapper);
                await notificationService.Insert(noti);
            }
        }

        public async Task<IEnumerable<ExtendedCourseDto>> Search(int tuteeId)
        {
            var entities = await _unitOfWork.CourseRepository.Search(tuteeId);
            return _mapper.Map<IEnumerable<ExtendedCourseDto>>(entities).ToList();
        }

        public async Task<IEnumerable<ExtendedCourseDto>> Search(int tuteeId, string enrollmentStatus)
        {
            var entities = await _unitOfWork.CourseRepository.Search(tuteeId, enrollmentStatus);
            return _mapper.Map<IEnumerable<ExtendedCourseDto>>(entities).ToList();
        }

        public async Task<IEnumerable<CourseDto>> Search(int tuteeId, int subjectId, int classId)
        {
            var entities = await _unitOfWork.CourseRepository.Search(tuteeId, subjectId, classId);
            return _mapper.Map<IEnumerable<CourseDto>>(entities).ToList();
        }

        public async Task<IEnumerable<CourseTutorDto>> Search(FilterQueryDto filterDto)
        {
            var entities = await _unitOfWork.CourseRepository.Search(filterDto);
            //
            foreach (var c in entities)
            {
                //
                var quantityEnrollment = await _unitOfWork.EnrollmentRepository.CountEnrollmentByCourseIdWithStatus(GlobalConstants.ACTIVE_STATUS, c.Id);
                var availableSlot = c.MaxTutee - quantityEnrollment;
                c.AvailableSlot = availableSlot;
                //
                var avg = await _unitOfWork.FeedbackRepository.GetAverageRatingStateByTutorId(c.CreatedBy);
                c.AverageRatingStar = avg;
                //set distance
                //GoogleMapHandler googleMapHelpers = new GoogleMapHandler();
                //var distance = googleMapHelpers.GetDistanceByAddresses(originAddress, c.Location);
                //c.Distance = distance;
            }
            //
            return _mapper.Map<IEnumerable<CourseTutorDto>>(entities).ToList();
        }

        public async Task Update(CourseDto dto)
        {
            var entity = _mapper.Map<Course>(dto);
            await _unitOfWork.CourseRepository.Update(entity);
            await _unitOfWork.Commit();
            string email = await _unitOfWork.ManagerRepository.GetManagerEmailByClassHasSubject(dto.ClassHasSubjectId);
            if (!String.IsNullOrEmpty(email))
            {
                NotificationDto noti = new NotificationDto
                {
                    Title = "Confirm Course Request",
                    Message = "You has a new course request",
                    IsRead = false,
                    SendToUser = email
                };
                INotificationService notificationService = new NotificationService(_unitOfWork, _mapper);
                await notificationService.Insert(noti);
            }
            
        }

        public async Task<IEnumerable<ExtendedCourseDto>> GetAllExtended()
        {
            var entities = await _unitOfWork.CourseRepository.GetAllExtended();
            return _mapper.Map<IEnumerable<ExtendedCourseDto>>(entities).ToList();
        }

        public async Task<ExtendedCourseDto> GetExtendedCourseById(int id)
        {
            Course course = await _unitOfWork.CourseRepository.GetById(id);
            //
            ClassHasSubject classHasSubject = await _unitOfWork.ClassSubjectRepository.GetById(course.ClassHasSubjectId);
            Class classes = await _unitOfWork.ClassRepository.GetById(classHasSubject.ClassId);
            Subject subject = await _unitOfWork.SubjectRepository.GetById(classHasSubject.SubjectId);
            Tutor tutor = await _unitOfWork.TutorRepository.GetById(course.CreatedBy);


            return new ExtendedCourseDto
            {
                ExtraImages = course.ExtraImages,
                Location = course.Location,
                Id = course.Id,
                Name = course.Name,
                BeginTime = course.BeginTime,
                EndTime = course.EndTime,
                StudyFee = course.StudyFee,
                DaysInWeek = course.DaysInWeek,
                BeginDate = course.BeginDate,
                EndDate = course.EndDate,
                ClassHasSubjectId = course.ClassHasSubjectId,
                ConfirmedBy = course.ConfirmedBy,
                ConfirmedDate = course.ConfirmedDate,
                CreatedBy = course.CreatedBy,
                CreatedDate = course.CreatedDate,
                MaxTutee = course.MaxTutee,
                Description = course.Description,
                Status = course.Status,
                SubjectName = subject.Name,
                ClassName = classes.Name,
                TutorAvatarUrl = tutor.AvatarImageLink,
                TutorName = tutor.Fullname,
                TutorAddress = tutor.Address,
                TutorEmail = tutor.Email,
                Precondition = course.Precondition
            };
        }

        public async Task<CourseDto> CheckValidate(CourseDto courseDto)
        {
            var entity = await _unitOfWork.CourseRepository.CheckValidate(courseDto);
            return _mapper.Map<CourseDto>(entity);
        }

        public async Task<int> CheckCourseByTutorId(int tutorId)
        {
            return await _unitOfWork.CourseRepository.CheckCourseByTutorId(tutorId);
        }

        public async Task<Response<ExtendedCourseDto>> Filter(CourseParameter parameter)
        {   
            var entities = await _unitOfWork.CourseRepository.Filter(parameter);
            return new Response<ExtendedCourseDto>
            {
                TotalCount = entities.TotalCount,
                TotalPages = entities.TotalPages,
                CurrentPage = entities.CurrentPage,
                HasNext = entities.HasNext,
                HasPrevious = entities.HasPrevious,
                PageSize = entities.PageSize,
                Data = entities.Select(e => new ExtendedCourseDto
                {
                    Description = e.Description,
                    Status = e.Status,
                    Id = e.Id,
                    Name = e.Name,
                    BeginTime = e.BeginTime,
                    EndTime = e.EndTime,
                    StudyFee = e.StudyFee,
                    DaysInWeek = e.DaysInWeek,
                    BeginDate = e.BeginDate,
                    EndDate = e.EndDate,
                    ClassHasSubjectId = e.ClassHasSubjectId,
                    ConfirmedBy = e.ConfirmedBy,
                    ConfirmedDate = e.ConfirmedDate,
                    CreatedBy = e.CreatedBy,
                    CreatedDate = e.CreatedDate,
                    MaxTutee = e.MaxTutee,
                    Location = e.Location,
                    ExtraImages = e.ExtraImages,
                    SubjectName = e.SubjectName,
                    ClassName = e.ClassName,
                    EnrollmentId = e.EnrollmentId,
                    FollowDate = e.FollowDate,
                    EnrollmentStatus = e.EnrollmentStatus,
                    NumberOfTutee = e.NumberOfTutee,
                    TutorAvatarUrl = e.TutorAvatarUrl,
                    TutorName = e.TutorName,
                    TutorEmail = e.TutorEmail,
                    ConfirmerName = e.ConfirmerName,
                    Precondition = e.Precondition
                }).ToList()
            };
            
        }

        public async Task<int> CheckCourseByClassHasSubject(int id)
        {
            return await _unitOfWork.CourseRepository.CheckCourseByClassHasSubject(id);
        }

        public async Task<bool> ConfirmCourse(CourseDto dto)
        {
            var entity = await _unitOfWork.CourseRepository.GetById(dto.Id);
            if(entity != null)
            {
                if(entity.Status == GlobalConstants.PENDING_STATUS)
                {
                    entity.Status = dto.Status;
                    entity.ConfirmedBy = dto.ConfirmedBy;
                    entity.ConfirmedDate = Tools.GetUTC();
                    await _unitOfWork.CourseRepository.Update(entity);
                    await _unitOfWork.Commit();
                    return true;
                }
                return false;
            }return false;
        }

        public async Task<CourseTutorDto> GetByTutor(int tutorId)
        {
            var entities = await _unitOfWork.CourseRepository.GetByTutor(tutorId);
            var result = _mapper.Map<IEnumerable<CourseTutorDto>>(entities).ToList();
            if (result.Any())
            {
                return result[0];
            }
            else return null;
        }

        public async Task<CusResponse> InactiveCourse(int courseId, int confirmedBy)
        {
            var response = new CusResponse
            {
                Message = "Course is not found",
                Status = false,
                Type = "fail"
            };
            var entity = await _unitOfWork.CourseRepository.GetById(courseId);
            if(entity != null)
            {
                if(entity.Status != GlobalConstants.INACTIVE_STATUS)
                {
                    entity.Status = GlobalConstants.INACTIVE_STATUS;
                    entity.ConfirmedBy = confirmedBy;
                    entity.ConfirmedDate = Tools.GetUTC();
                    await _unitOfWork.CourseRepository.Update(entity);
                    await _unitOfWork.Commit();
                    response = new CusResponse
                    {
                        Message = "Course update successfully",
                        Status = true,
                        Type = "success"
                    };

                }
                else
                {
                    response = new CusResponse
                    {
                        Message = "This course has been confirmed",
                        Status = false,
                        Type = "fail"
                    };
                }
            }
            else
            {
                return response;
            }
            return response;
        }
    }
}
