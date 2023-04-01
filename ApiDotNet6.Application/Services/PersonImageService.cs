using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.DTOs.Validations;
using ApiDotNet6.Application.Services.Interface;
using ApiDotNet6.Domain.Entities;
using ApiDotNet6.Domain.Integrations;
using ApiDotNet6.Domain.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace ApiDotNet6.Application.Services
{
    public class PersonImageService : IPersonImageService
    {
        private readonly IPersonImageRepository _personImageRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ISavePersonImage _savePersonImage;
        private readonly IMapper _mapper;
        private readonly string[] extensions = { "png", "jpg", "ico", "bmp" };


        public PersonImageService(IPersonImageRepository personImageRepository, IPersonRepository personRepository, ISavePersonImage savePersonImage, IMapper mapper)
        {
            _personImageRepository = personImageRepository;
            _personRepository = personRepository;
            _savePersonImage = savePersonImage;
            _mapper = mapper;
        }

        public async Task<ResultService> CreateImageAsync(PersonImageFileDTO personImageDTO)
        {
            if (personImageDTO == null)
                return ResultService.Fail("Objeto deve ser informado");

            var validations = new PersonImageFileDTOValidator().Validate(personImageDTO);
            if (!validations.IsValid)
                return ResultService.RequestError("Problemas com a validação", validations);

            var person = await _personRepository.GetByIdAsync(personImageDTO.PersonId);
            if (person == null)
                return ResultService.Fail("Pessoa não encontrada");

            int SizeinKB = (int) personImageDTO.File.Length / 1024;

            if (SizeinKB > 512)
                return ResultService.Fail("Arquivo maior que 512 Kylobytes, tamanho original: " + SizeinKB);

            string nomeInvertido = new string(personImageDTO.File.FileName.Reverse().ToArray());
            string extFile = nomeInvertido.Substring(0, nomeInvertido.IndexOf('.'));
            extFile = new string(extFile.Reverse().ToArray());


            if (!extensionAccepted(extFile))
                return ResultService.Fail("Tipo de imagem não permitido, apenas: " + string.Join(", ", extensions));

            await using var memoryStream = new MemoryStream();
            await personImageDTO.File.CopyToAsync(memoryStream);

            var patthImage = _savePersonImage.Save(memoryStream.ToArray(), extFile);

            var personImage = new PersonImage(person.Id, patthImage, personImageDTO.Description);
            await _personImageRepository.CreateAsync(personImage);

            return ResultService.Ok("Image salva");
        }

        public async Task<ResultService> CreateImageBase64Async(PersonImage64DTO personImageDTO)
        {
            if (personImageDTO == null)
                return ResultService.Fail("Objeto deve ser informado");

            var validations = new PersonImage64DTOValidator().Validate(personImageDTO);
            if (!validations.IsValid)
                return ResultService.RequestError("Problemas com a validação", validations);

            var person = await _personRepository.GetByIdAsync(personImageDTO.PersonId);
            if (person == null)
                return ResultService.Fail("Id pessoa não encontrado");

            byte[] bytes = Convert.FromBase64String(personImageDTO.Image);

            int SizeinKB = (int)bytes.Length / 1024;

            if (SizeinKB > 512)
                return ResultService.Fail("Arquivo maior que 512 Kylobytes, tamanho original: " + SizeinKB);

            var extFile = GetFileExtension(personImageDTO.Image);

            if (!extensionAccepted(extFile))
                return ResultService.Fail("Tipo de imagem não permitido, apenas: "+ string.Join(", ", extensions));

            var patthImage = _savePersonImage.Save(bytes, extFile);

            var personImage = new PersonImage(person.Id, patthImage, personImageDTO.Description);

            await _personImageRepository.CreateAsync(personImage);

            return ResultService.Ok("Imagem em base64 salva");
        }

        public async Task<ResultService> GetPersonImageAsync(int idPerson)
        {
            if (idPerson <= 0)
                return ResultService.Fail("Id da pessoa deve ser informado");

            var imagesPerson = await _personImageRepository.GetByPersonIdAsync(idPerson);

            return ResultService.Ok<ICollection<PersonImageDTO>>(_mapper.Map<ICollection<PersonImageDTO>>(imagesPerson));
        }

        private bool extensionAccepted(string extFile)
        {               
            if (extensions.Contains(extFile)) return true;

            return false;
        }

        private string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return "png";
                case "/9J/4":
                    return "jpg";
                case "AAAAF":
                    return "mp4";
                case "JVBER":
                    return "pdf";
                case "AAABA":
                    return "ico";
                case "UMFYI":
                    return "rar";
                case "E1XYD":
                    return "rtf";
                case "U1PKC":
                    return "txt";
                case "Qk":
                    return "bmp";
                case "MQOWM":
                case "77U/M":
                    return "srt";
                default:
                    return string.Empty;
            }
        }


        public async Task<ActionResult> GetDownloadImageAsync(int idimg)
        {
            if (idimg <= 0)
                throw new Exception("Id da imagem deve ser informado");

            var image = await _personImageRepository.GetByIdAsync(idimg);

            if (image == null)
                throw new Exception("Imagem não encontrada");

            if (string.IsNullOrEmpty(image.ImageUri))
                throw new Exception("Imagem não encontrada");

        
            var fileStream = new FileStream(image.ImageUri, FileMode.Open, FileAccess.Read);

            return new FileStreamResult(fileStream, new MediaTypeHeaderValue("application/octet-stream"))
            {
                FileDownloadName = Path.GetFileName(image.ImageUri).ToLower()
            };
        }
    }
}
