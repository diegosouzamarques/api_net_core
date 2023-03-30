
namespace ApiDotNet6.Application.DTOs
{
    public class PagedBaseResponseDTO<T>
    {
        public int TotalRegister { get; private set; }
        public List<T> Data { get; private set; }

        public PagedBaseResponseDTO(int totalRegister, List<T> data) 
        {
            this.TotalRegister = totalRegister;
            this.Data = data; 
        }
                

    }
}
