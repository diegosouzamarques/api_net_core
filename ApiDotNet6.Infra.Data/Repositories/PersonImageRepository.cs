using ApiDotNet6.Domain.Entities;
using ApiDotNet6.Domain.Repositories;
using ApiDotNet6.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiDotNet6.Infra.Data.Repositories
{
    public class PersonImageRepository : IPersonImageRepository
    {
        private readonly AppDbContext _appDbContext;

        public PersonImageRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PersonImage> CreateAsync(PersonImage personImage)
        {
            _appDbContext.Add(personImage);
            await _appDbContext.SaveChangesAsync();
            return personImage;
        }

        public async Task EditAsync(PersonImage personImage)
        {
            _appDbContext.Update(personImage);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<PersonImage?> GetByIdAsync(int id)
        {
            return await _appDbContext.PersonImages.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<PersonImage>> GetByPersonIdAsync(int personId)
        {
            return await _appDbContext.PersonImages.AsNoTracking().Where(x => x.PersonId== personId).ToListAsync();
        }
    }
}
