using System;
using System.Threading.Tasks;
using TutorSearchSystem.Repositories;

namespace TutorSearchSystem.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        AccountRepository AccountRepository { get;}
        ClassRepository ClassRepository { get; }
        ClassSubjectRepository ClassSubjectRepository { get; }
        CourseRepository CourseRepository { get; }
        FeeRepository FeeRepository { get; }
        ManagerRepository ManagerRepository { get; }
        MembershipRepository MembershipRepository { get; }
        RoleRepository RoleRepository { get; }
        SubjectRepository SubjectRepository { get; }
        EnrollmentRepository EnrollmentRepository { get; }
        TuteeRepository TuteeRepository { get; }
        TutorRepository TutorRepository { get; }
        TutorTransactionRepository TutorTransactionRepository { get; }
        FeedbackRepository FeedbackRepository { get; }
        ImageRepository ImageRepository { get; }
        NotificationRepository NotificationRepository { get; }
        TutorUpdateProfileRepository TutorUpdateProfileRepository { get; }
        Task Commit();
        ReportTypeRepository ReportTypeRepository { get; }
        TuteeReportRepository TuteeReportRepository { get; }
        TutorReportRepository TutorReportRepository { get; }
        CourseDetailRepository CourseDetailRepository { get; }
    }
}
