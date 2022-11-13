using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Repositories.IRepositories;

namespace TutorSearchSystem.Repositories
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private readonly TSDbContext _context;
        public ImageRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }


        //delete Image
        public async Task Delete(int id)
        {
            var entity = await _context.Image.FindAsync(id);
            _context.Image.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByOwnerEmail(string email)
        {
            var entities = await _context.Image.Where(i => i.OwnerEmail == email).ToListAsync();
            foreach (var i in entities)
            {
                _context.Image.Remove(i);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<string[]> Get(string ownerEmail, string imageType)
        {
            //return await _context.Image
            //    .Where( i => i.OwnerEmail == ownerEmail && i.ImageType == imageType)
            //    .ToListAsync();
            return await  _context.Image
                .Where(i => i.OwnerEmail == ownerEmail && i.ImageType == imageType)
                .Select(i => i.ImageLink).ToArrayAsync();
        }
    }
}
