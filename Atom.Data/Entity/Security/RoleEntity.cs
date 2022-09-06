using Atom.Data.BaseClass;
using Dapper.Contrib.Extensions;
using System;

namespace Atom.Data.Entity.Security
{
    [Table("[sec].[Role]")]
    public class RoleEntity : EntityBase
    {
        [Key]
        [Computed]
        public int RoleId { get; set; }

        [Key]
        [Computed]
        public Guid RoleGuid { get; set; }

        public int UserId { get; set; }

        public int RoleTypeId { get; set; }

        public int? OrganizationId { get; set; }

        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

        public string? OrganizationName { get; set; }

    }
}
