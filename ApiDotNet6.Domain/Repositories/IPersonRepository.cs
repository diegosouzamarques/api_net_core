﻿using ApiDotNet6.Domain.Entities;
using ApiDotNet6.Domain.FiltersDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Domain.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> GetByIdAsync(int id);
        Task<ICollection<Person>> GetPeopleAsync();
        Task<Person> CreateAsync(Person person);
        Task<Person> EditAsync(Person person);
        Task DeleteAsync(Person person);
        Task<int> GetByDocumentAsync(string document);
        Task<PagedBaseResponse<Person>> GetPagedAsync(PersonFilterDb request);

    }
}
