using ApiDotNet6.Domain.Validations;

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

            DomainValidationException.When(visualName.Length > 200, "NomeVisual máximo de 200 caracteres");
            DomainValidationException.When(permissionName.Length > 200, "Nome permissão máximo de 200 caracteres");

            VisualName = visualName;
            PermissionName = permissionName;
        }
    }
}
