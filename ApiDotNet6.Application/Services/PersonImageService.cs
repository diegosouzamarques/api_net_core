using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.DTOs.Validations;
using ApiDotNet6.Application.Services.Interface;
using ApiDotNet6.Domain.Entities;
using ApiDotNet6.Domain.Integrations;
using ApiDotNet6.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Application.Services
{
    public class PersonImageService : IPersonImageService
    {
        private readonly IPersonImageRepository _personImageRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ISavePersonImage _savePersonImage;

        public PersonImageService(IPersonImageRepository personImageRepository, IPersonRepository personRepository, ISavePersonImage savePersonImage)
        {
            _personImageRepository = personImageRepository;
            _personRepository = personRepository;
            _savePersonImage = savePersonImage;
        }

        public async Task<ResultService> CreateImageAsync(PersonImageDTO personImageDTO)
        {
            if (personImageDTO == null)
                return ResultService.Fail("Objeto deve ser informado");

            var validations = new PersonImageDTOValidator().Validate(personImageDTO);
            if (!validations.IsValid)
                return ResultService.RequestError("Problemas com a validação", validations);

            var person = await _personRepository.GetByIdAsync(personImageDTO.PersonId);
            if (person == null)
                return ResultService.Fail("Pessoa não encontrada");

            var patthImage = _savePersonImage.Save(personImageDTO.Image);

            var personImage = new PersonImage(person.Id, patthImage, null);
            await _personImageRepository.CreateAsync(personImage);

            return ResultService.Ok("Image salva");
        }

        public async Task<ResultService> CreateImageBase64Async(PersonImageDTO personImageDTO)
        {
            if (personImageDTO == null)
                return ResultService.Fail("Objeto deve ser informado");

            var validations = new PersonImageDTOValidator().Validate(personImageDTO);
            if (!validations.IsValid)
                return ResultService.RequestError("Problemas com a validação", validations);

            var person = await _personRepository.GetByIdAsync(personImageDTO.PersonId);
            if (person == null)
                return ResultService.Fail("Id pessoa não encontrado");

            var personImage = new PersonImage(personImageDTO.PersonId, null, personImageDTO.Image);

            await _personImageRepository.CreateAsync(personImage);
            return ResultService.Ok("Imagem em base64 salva");
        }
    }
}
