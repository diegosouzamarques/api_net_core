using Microsoft.AspNetCore.Http;

namespace ApiDotNet6.Application.DTOs
{

    public  class Image {
        public int PersonId { get; set; }
        public string Description { get; set; }
    }
    public class PersonImage64DTO : Image
    {
        public string Image { get; set; }
    }

    public class PersonImageFileDTO: Image
    {
        public IFormFile File { get; set; }
 
    }

    public class PersonImageDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }

    }
}
