using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class TSDbContext : DbContext
    {
        public TSDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<ClassHasSubject> Class_Has_Subject { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Fee> Fee { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Manager> Manager { get; set; }
        public DbSet<Membership> Membership { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Tutee> Tutee { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Tutor> Tutor { get; set; }
        public DbSet<TutorTransaction> TutorTransaction { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<TutorUpdateProfile> TutorUpdateProfile { get; set; }
        public DbSet<ReportType> ReportType { get; set; }
        public DbSet<TuteeReport> TuteeReport { get; set; }
        public DbSet<TutorReport> TutorReport { get; set; }
        public DbSet<CourseDetail> CourseDetail { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //remove check cycle or multiple cascade SQL Exception
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<Class>()
                .HasKey(c => c.Id);
            
            modelBuilder.Entity<Subject>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Feedback>()
                .HasKey(f => f.Id);
            //
            modelBuilder.Entity<ClassHasSubject>()
                .HasOne(x => x.Class)
                .WithMany(c => c.ClassHasSubjects)
                .HasForeignKey(x => x.ClassId);

            modelBuilder.Entity<ClassHasSubject>()
                .HasOne(x => x.Subject)
                .WithMany(c => c.ClassHasSubjects)
                .HasForeignKey(x => x.SubjectId);
            //
            modelBuilder.Entity<Tutee>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Course>()
                .HasKey(s => s.Id);
            //set confirm by (manager) is nullable
            modelBuilder.Entity<Course>()
                .Property(c => c.ConfirmedBy).IsRequired(false);
            //set confirm date is nullable
            modelBuilder.Entity<Course>()
                .Property(c => c.ConfirmedDate).IsRequired(false);
            modelBuilder.Entity<Enrollment>()
                .HasOne(x => x.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(x => x.Tutee)
                .WithMany(c => c.Enrollements)
                .HasForeignKey(x => x.TuteeId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<TuteeTransaction>().HasNoKey();
          
            //
            modelBuilder.Entity<Tutor>()
                .HasOne(x => x.Role)
                .WithMany(r => r.Tutors)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            //
            modelBuilder.Entity<Tutee>()
                .HasOne(x => x.Role)
                .WithMany(r => r.Tutees)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            //
            modelBuilder.Entity<Course>()
                .HasOne(x => x.ClassHasSubject)
                .WithMany(c => c.Courses)
                .HasForeignKey(x => x.ClassHasSubjectId)
                .OnDelete(DeleteBehavior.Restrict);
            //
            modelBuilder.Entity<ClassHasSubject>()
                .HasKey(x => x.Id);

            //set foreignkey from Manager and Class
            modelBuilder.Entity<Class>()
                .HasOne(c => c.ClassUpdater)
                .WithMany(m => m.UpdatedClasses)
                .HasForeignKey(c => c.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            //set foreignkey from Manager and Class
            modelBuilder.Entity<Class>()
                .HasOne(c => c.ClassCreator)
                .WithMany(m => m.CreatedClasses)
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            //set foreignkey from Manager and Subject
            modelBuilder.Entity<Subject>()
                .HasOne(c => c.SubjectCreator)
                .WithMany(m => m.CreatedSubjects)
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            //set foreignkey from Manager and Subject
            modelBuilder.Entity<Subject>()
                .HasOne(c => c.SubjectUpdater)
                .WithMany(m => m.UpdatedSubjects)
                .HasForeignKey(c => c.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            //account set email is key
            modelBuilder.Entity<Account>()
                .HasKey(a => a.Email);
            //
            //set foreignkey from Manager and Fee
            modelBuilder.Entity<Fee>()
                .HasOne(c => c.FeeCreator)
                .WithMany(m => m.CreatedFees)
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            //set foreignkey from Manager and Fee
            modelBuilder.Entity<Fee>()
                .HasOne(c => c.FeeUpdater)
                .WithMany(m => m.UpdatedFees)
                .HasForeignKey(c => c.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            //
            //set foreignkey from Manager and Fee
            modelBuilder.Entity<Membership>()
                .HasOne(c => c.MembershipCreator)
                .WithMany(m => m.CreatedMemberships)
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            //set foreignkey from Manager and Membership
            modelBuilder.Entity<Membership>()
                .HasOne(c => c.MembershipUpdater)
                .WithMany(m => m.UpdatedMemberships)
                .HasForeignKey(c => c.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            //set foreignkey from Manager and Subject by manageBy attr
            modelBuilder.Entity<Subject>()
                .HasOne(c => c.SubjectManager)
                .WithMany(m => m.ManagedSubjects)
                .HasForeignKey(c => c.ManageBy)
                .OnDelete(DeleteBehavior.Restrict);
            //
            //set foreignkey from Manager and Feedback by manageBy attr
            modelBuilder.Entity<Feedback>()
                .HasOne(c => c.Manager)
                .WithMany(m => m.Feedbacks)
                .HasForeignKey(c => c.ConfirmedBy)
                .OnDelete(DeleteBehavior.Restrict);
            //
            //set foreignkey from Classhasubject and mamaner by createdBy attr
            modelBuilder.Entity<ClassHasSubject>()
                .HasOne(c => c.ClassHasSubjectCreator)
                .WithMany(m => m.CreatedClassHasSubjects)
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            //
            //set foreignkey from Classhasubject and mamaner by createdBy attr
            modelBuilder.Entity<Tutor>()
                .HasOne(t => t.TutorConfirmer)
                .WithMany(m => m.ConfirmedTutors)
                .HasForeignKey(c => c.ConfirmedBy)
                .OnDelete(DeleteBehavior.Restrict);
            //
            //set foreignkey from Manager and mamaner by createdBy attr
            modelBuilder.Entity<Manager>()
                .HasOne(m => m.ManagerCreator)
                .WithMany(c => c.CreatedManagers)
                .HasForeignKey(m => m.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            //report type
            //role
            modelBuilder.Entity<ReportType>()
               .HasOne(c => c.Role)
               .WithMany(r => r.ReportTypes)
               .HasForeignKey(c => c.RoleId)
               .OnDelete(DeleteBehavior.Restrict);
            //mânger
            modelBuilder.Entity<ReportType>()
                .HasOne(c => c.ReportTypeUpdater)
                .WithMany(m => m.UpdatedReportTypes)
                .HasForeignKey(c => c.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ReportType>()
                .HasOne(c => c.ReportTypeCreator)
                .WithMany(m => m.CreatedReportTypes)
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            //tutee report
            //
            modelBuilder.Entity<TuteeReport>()
                .HasOne(c => c.ReportType)
                .WithMany(m => m.TuteeReports)
                .HasForeignKey(c => c.ReportTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TuteeReport>()
                .HasOne(c => c.Enrollment)
                .WithMany(m => m.TuteeReports)
                .HasForeignKey(c => c.EnrollmentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TuteeReport>()
                .HasOne(c => c.Manager)
                .WithMany(m => m.TuteeReports)
                .HasForeignKey(c => c.ConfirmedBy)
                .OnDelete(DeleteBehavior.Restrict);
            //tutor report
            //
            modelBuilder.Entity<TutorReport>()
                .HasOne(c => c.ReportType)
                .WithMany(m => m.TutorReports)
                .HasForeignKey(c => c.ReportTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TutorReport>()
                .HasOne(c => c.Tutor)
                .WithMany(m => m.TutorReports)
                .HasForeignKey(c => c.TutorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TutorReport>()
                .HasOne(c => c.Manager)
                .WithMany(m => m.TutorReports)
                .HasForeignKey(c => c.ConfirmedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseDetail>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<CourseDetail>()
                .HasOne(x => x.Course)
                .WithMany(c => c.CourseDetails)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Feedback>()
                .HasOne(x => x.Course)
                .WithMany(c => c.Feedbacks)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
