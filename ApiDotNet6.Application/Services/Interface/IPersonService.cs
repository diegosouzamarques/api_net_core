using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Domain.FiltersDb;
using ApiDotNet6.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Application.Services.Interface
{
    public interface IPersonService
    {
        Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDTO);
        Task<ResultService<ICollection<PersonDTO>>> GetAsync();
        Task<ResultService<PersonDTO>> GetByIdAsync(int id);
        Task<ResultService<PersonDTO>> UpdateAsync(PersonDTO personDTO);
        Task<ResultService> DeleteAsync(int id);
        Task<ResultService<PagedBaseResponseDTO<PersonDTO>>> GetPagedAsync(PersonFilterDb personFilterDb);
    }
}
