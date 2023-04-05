using ApiDotNet6.Domain.Validations;
using System.Numerics;

namespace ApiDotNet6.Domain.Entities
{
    public class PersonImage
    {
        public int Id { get; private set; }
        public int PersonId { get; private set; }
        public string? ImageUri { get; private set; }
        public string Description { get; private set; }

        public Person Person { get; set; }


        public PersonImage(int personId, string? imageUri, string description)
        {
            Validation(personId, description);
            ImageUri = imageUri;  
        }

        private void Validation(int personId, string description)
        {
            DomainValidationException.When(personId <= 0, "Id da pessoa dever ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(description), "Descrição da imagem dever ser informado");
            DomainValidationException.When(description.Length > 200, "Descrição máximo de 200 caracteres");

            Description = description;
            PersonId  = personId;        
  
        }

    }
}
