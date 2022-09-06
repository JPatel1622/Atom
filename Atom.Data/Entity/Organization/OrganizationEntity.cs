using Atom.Data.BaseClass;
using Dapper.Contrib.Extensions;
using System;

namespace Atom.Data.Entity.Organization
{
    [Table("[org].[Organization]")]
    public class OrganizationEntity : EntityBase
    {
        [Computed]
        [Key]
        public int OrganizationId { get; set; }

        [Computed]
        [Key]
        public Guid OrganizationGuid { get; set; }

        public string OrganizationName { get; set; }
    }
}
