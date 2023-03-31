using ApiDotNet6.Domain.Entities;
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
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _db;
        public PermissionRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<ICollection<Permission>> GetPermissionAsync()
        {
            return await _db.Permissions.ToListAsync();
        }
        public async Task<ICollection<Permission>> GetExistAsync(List<int> permissions)
        {
            return await _db.Permissions.Where(w => permissions.Contains(w.Id)).ToListAsync();
        }
    }
}
