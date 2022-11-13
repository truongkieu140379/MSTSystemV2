using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Repositories;

namespace TutorSearchSystem.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TSDbContext _context;
        private bool disposedValue;

        public AccountRepository AccountRepository { get; private set; }
        public ClassRepository ClassRepository { get; private set; }
        public ClassSubjectRepository ClassSubjectRepository { get; private set; }
        public CourseRepository CourseRepository { get; private set; }
        public FeeRepository FeeRepository { get; private set; }
        public ManagerRepository ManagerRepository { get; private set; }
        public MembershipRepository MembershipRepository { get; private set; }
        public RoleRepository RoleRepository { get; private set; }
        public SubjectRepository SubjectRepository { get; private set; }
        public EnrollmentRepository EnrollmentRepository { get; private set; }
        public TuteeRepository TuteeRepository { get; private set; }
        public TutorRepository TutorRepository { get; private set; }
        public TutorTransactionRepository TutorTransactionRepository { get; private set; }
        public FeedbackRepository FeedbackRepository { get; private set; }
        public ImageRepository ImageRepository { get; private set; }
        public NotificationRepository NotificationRepository { get; private set; }
        public TutorUpdateProfileRepository TutorUpdateProfileRepository { get; private set; }

        public ReportTypeRepository ReportTypeRepository { get; private set; }
        public TutorReportRepository TutorReportRepository { get; private set; }
        public TuteeReportRepository TuteeReportRepository { get; private set; }

        public CourseDetailRepository CourseDetailRepository { get; private set; }

        public UnitOfWork(TSDbContext context)
        {
            _context = context;
            InitRepositories();
        }

        private void InitRepositories()
        {
            AccountRepository = new AccountRepository(_context);
            ClassRepository = new ClassRepository(_context);
            ClassSubjectRepository = new ClassSubjectRepository(_context);
            CourseRepository = new CourseRepository(_context);
            FeeRepository = new FeeRepository(_context);
            ManagerRepository = new ManagerRepository(_context);
            MembershipRepository = new MembershipRepository(_context);
            RoleRepository = new RoleRepository(_context);
            SubjectRepository = new SubjectRepository(_context);
            EnrollmentRepository = new EnrollmentRepository(_context);
            TuteeRepository = new TuteeRepository(_context);
            TutorTransactionRepository = new TutorTransactionRepository(_context);
            TutorRepository = new TutorRepository(_context);
            FeedbackRepository = new FeedbackRepository(_context);
            ImageRepository = new ImageRepository(_context);
            NotificationRepository = new NotificationRepository(_context);
            TutorUpdateProfileRepository = new TutorUpdateProfileRepository(_context);
            ReportTypeRepository = new ReportTypeRepository(_context);
            TutorReportRepository = new TutorReportRepository(_context);
            TuteeReportRepository = new TuteeReportRepository(_context);
            CourseDetailRepository = new CourseDetailRepository(_context);
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
