using Atom.Domain.Attribute;
using Dapper.Contrib.Extensions;
using System;
using Atom.Domain.Enum;
using Atom.Data.BaseClass;
using System.Collections.Generic;
using Atom.Data.Model.Security;

namespace Atom.Data.Entity.Security
{
    [Table("[sec].[User]")]
    public class UserEntity : EntityBase
    {
        [Computed]
        public int UserId { get; set; }

        public string EmailAddress { get; set; }

        [Computed]
        [Key]
        public Guid UserGuid { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string UserName { get; set; }

        public string? UserAvatarURL { get; set; }

        public bool? Active { get; set; } = true;

        public int? CreatedByUser { get; set; }

    }
}
