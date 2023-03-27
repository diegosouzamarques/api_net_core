using ApiDotNet6.Domain.Entities;
using ApiDotNet6.Domain.FiltersDb;
using ApiDotNet6.Domain.Repositories;
using ApiDotNet6.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Infra.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _db;
        public PersonRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Person> CreateAsync(Person person)
        {
            _db.Add(person);
            await _db.SaveChangesAsync();
            return person;
        }

        public async Task DeleteAsync(Person person)
        {
            _db.Remove(person);
            await _db.SaveChangesAsync();

        }

        public async Task<Person> EditAsync(Person person)
        {
            _db.Update(person);
            await _db.SaveChangesAsync();
            return person;

        }

        public async Task<int> GetByDocumentAsync(string document)
        {
            return (await _db.People.FirstOrDefaultAsync(f => f.Document == document))?.Id ?? 0;
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _db.People.FirstOrDefaultAsync(f => f.Id == id);

        }

        public async Task<PagedBaseResponse<Person>> GetPagedAsync(PersonFilterDb request)
        {
            var people = _db.People.AsQueryable();
            if(!string.IsNullOrEmpty(request.Name))
               people = people.Where(x => x.Name.Contains(request.Name));

            return await PagedBaseResponseHelper
                         .GetResponseAsync<PagedBaseResponse<Person>, Person>(people, request);
        }

        public async Task<ICollection<Person>> GetPeopleAsync()
        {
            return await _db.People.ToListAsync();
        }
    }
}
