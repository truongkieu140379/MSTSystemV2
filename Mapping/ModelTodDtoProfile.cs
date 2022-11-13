using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Models;
using TutorSearchSystem.Models.ExtendedModels;
using TutorSearchSystem.Global;


namespace TutorSearchSystem.Mapping
{
    public class ModelTodDtoProfile : Profile
    {
        public ModelTodDtoProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Fee, FeeDto>().ReverseMap();
            CreateMap<Manager, ManagerDto>().ReverseMap();
            CreateMap<Membership, MembershipDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<Tutee, TuteeDto>().ReverseMap();
            CreateMap<Tutor, TutorDto>().ReverseMap();
            CreateMap<TutorTransaction, TutorTransactionDto>().ReverseMap();
            CreateMap<ClassHasSubject, ClassHasSubjectDto>().ReverseMap();
            CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
            CreateMap<Feedback, FeedbackDto>().ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();
            CreateMap<CourseTutor, CourseTutorDto>().ReverseMap();
            CreateMap<ExtendedFee, ExtendedFeeDto>().ReverseMap();
            CreateMap<ExtendedMembership, ExtendedMembershipDto>().ReverseMap();
            CreateMap<ExtendedFeedback, ExtendedFeedbackDto>().ReverseMap();
            CreateMap<ExtendedCourse, ExtendedCourseDto>().ReverseMap();
            CreateMap<ExtendedTutor, ExtendedTutorDto>().ReverseMap();
            CreateMap<ExtendedClassHasSubject, ExtendedClassHasSubjectDto>().ReverseMap();
            CreateMap<ExtendedClass, ExtendedClassDto>().ReverseMap();
            CreateMap<ExtendedSubject, ExtendedSubjectDto>().ReverseMap();
            CreateMap<ExtendedTutorTransaction, ExtendedTutorTransactionDto>().ReverseMap();
            CreateMap<ExtendedManager, ExtendedManagerDto>().ReverseMap();
            CreateMap<Notification, NotificationDto>().ReverseMap();
            CreateMap<TutorUpdateProfile, TutorUpdateProfileDto>().ReverseMap();
            CreateMap<ExtendedEnrollment, ExtendedEnrollmentDto>().ReverseMap();
            CreateMap<ReportTransaction, ReportTransactionDto>().ReverseMap();
            CreateMap<TuteeReport, TuteeReportDto>().ReverseMap();
            CreateMap<TutorReport, TutorReportDto>().ReverseMap();
            CreateMap<ReportType, ReportTypeDto>().ReverseMap();
            CreateMap<ExtendedReportType, ExtendedReportTypeDto>().ReverseMap();
            CreateMap<ExtendedTutorReport, ExtendedTutorReportDto>().ReverseMap();
            CreateMap<ExtendedTuteeReport, ExtendedTuteeReportDto>().ReverseMap();
            CreateMap<CourseDetail, CourseDetailDto>().ReverseMap();
            CreateMap<ExtendedCourseDetail, ExtendedCourseDetailDto>().ReverseMap();

        }


    }
}
