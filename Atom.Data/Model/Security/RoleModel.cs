using Atom.Data.BaseClass;
using Atom.Domain.Enum;
using System;

namespace Atom.Data.Model.Security
{
    public class RoleModel : ModelBase
    {
        public int RoleId { get; set; }

        public Guid RoleGuid { get; set; }

        public int UserId { get; set; }

        public Roles RoleTypeId { get; set; }

        public int? OrganizationId { get; set; }

        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

        public string? OrganizationName { get; set; }   
    }
}
