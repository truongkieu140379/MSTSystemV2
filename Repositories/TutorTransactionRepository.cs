using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Models.ExtendedModels;
using TutorSearchSystem.Repositories.IRepositories;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Repositories
{
    public class TutorTransactionRepository : GenericRepository<TutorTransaction>, ITutorTransactionRepository
    {
        private readonly TSDbContext _context;
        public TutorTransactionRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        //insert tutortransaction; then update tutor point and membership
        public async Task PostTutorTransaction(TutorTransaction tutorTransaction)
        {
            //get memebership of this tutor
            Membership membership = await GetMembershipByTutorTransaction(tutorTransaction);
            //udpate achievedPoint of tutor transaction
            tutorTransaction.ArchievedPoints = (int)Math.Round(tutorTransaction.TotalAmount * membership.PointRate);
            //insert tutor transaction
            _context.TutorTransaction.Add(tutorTransaction);
            await _context.SaveChangesAsync();
            //update tutor point and return tutor
            Tutor tutor = UpdateTutorPoint(tutorTransaction);
            //update tutor membership
            tutor = await UpdateTutorMembership(tutor);
            //put tutor
            _context.Entry(tutor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            //return
        }

        private async Task<Membership> GetMembershipByTutorTransaction(TutorTransaction tutorTransaction)
        {
            return await _context.Membership
                .Where(m => m.Tutors.Any(t => t.Id == tutorTransaction.TutorId))
                .FirstOrDefaultAsync();
        }


        //udpat tutor point
        private Tutor UpdateTutorPoint(TutorTransaction tutorTransaction)
        {
            Tutor tutor = _context.Tutor.Find(tutorTransaction.TutorId);
            //updatenew point to tutor
            tutor.Points = tutor.Points
                + tutorTransaction.ArchievedPoints
                - tutorTransaction.UsedPoints;
            //return
            return tutor;
        }

        //updatutor memeberhsip
        private async Task<Tutor> UpdateTutorMembership(Tutor tutor)
        {

            //get all membership in db; sort by ascending point amount
            IEnumerable<Membership> memberships = await _context.Membership
                .OrderBy(m => m.PointAmount)
                .ToListAsync();
            //if point is greater than pointamount; set membershipId of tutor = that memberhshipid
            foreach (var membership in memberships)
            {
                if (tutor.Points >= membership.PointAmount)
                {
                    tutor.MembershipId = membership.Id;
                }
            }
            //return tutor with new membership
            return tutor;
        }

        public async Task<IEnumerable<ExtendedTutorTransaction>> GetTransactionByTutor(int tutorId)
        {
            return await _context.TutorTransaction.
                Where(t => t.TutorId == tutorId).Select(t => new ExtendedTutorTransaction
                {
                    Id = t.Id,
                    ArchievedPoints = t.ArchievedPoints,
                    UsedPoints = t.UsedPoints,
                    FeeId = t.FeeId,
                    TutorId = t.TutorId,
                    DateTime = t.DateTime,
                    TotalAmount = t.TotalAmount,
                    Amount = t.Amount,
                    Description = t.Description,
                    Status = t.Status,
                    FeeName = t.Fee.Name,
                    FeePrice = t.FeePrice,
                    CourseName = t.Course.Name,
                    CourseId = t.CourseId
                })
                .OrderByDescending(t => t.DateTime)
                .ToListAsync();
        }

        public async Task<float> GetTotalAmountInMonth()
        {
            float total = 0;
            DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var tutors = await _context.TutorTransaction.Where(t => t.Status == GlobalConstants.SUCCESSFUL_STATUS && t.DateTime >= fromDate).ToListAsync();
            foreach (var item in tutors)
            {
                total += item.TotalAmount;
            }
            return total;
        }

        public async Task<IEnumerable<ReportTransaction>> GetSumAmount(int year)
        {
            DateTime fromDate = new DateTime(year, 1, 1);
            DateTime toDate = new DateTime(year + 1, 1, 1);
            return await _context.TutorTransaction.Where(t => t.Status == GlobalConstants.SUCCESSFUL_STATUS 
            && (t.DateTime >= fromDate && t.DateTime < toDate)).Select(
                t => new { t.DateTime.Year, t.DateTime.Month, t.TotalAmount }
                ).GroupBy(
                x => new { x.Year, x.Month }, (key, group) => new ReportTransaction{
                    Year = key.Year,
                    Month = key.Month,
                    TotalAmount = group.Sum(t => t.TotalAmount)
                }).ToListAsync();         
        }
    }
}
