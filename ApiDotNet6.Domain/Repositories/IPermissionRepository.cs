using ApiDotNet6.Domain.Entities;

namespace ApiDotNet6.Domain.Repositories
{
    public interface IPermissionRepository
    {
        Task<ICollection<Permission>> GetPermissionAsync();
        Task<ICollection<Permission>> GetExistAsync(List<int> permissions);
    }
}
