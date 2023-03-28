using ApiDotNet6.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Domain.Entities
{
    public sealed class Permission
    {
        public Permission(string visualName, string permissionName)
        {
            Validation(visualName, permissionName);
            UserPermissions = new List<UserPermission>();
        }

        public int Id { get; private set; }
        public string VisualName { get; private set; }
        public string PermissionName { get; private set; }

        public ICollection<UserPermission> UserPermissions { get; set; }

        private void Validation(string visualName, string permissionName)
        {
            DomainValidationException.When(string.IsNullOrEmpty(visualName), "NomeVisual dever ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(permissionName), "Nome permissão dever ser informado");

            VisualName = visualName;
            PermissionName = permissionName;
        }
    }
}
