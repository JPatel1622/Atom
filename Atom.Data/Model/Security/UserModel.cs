using Atom.Data.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Atom.Data.Model.Security
{
    public class UserModel : ModelBase
    {
        public int UserId { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string? Password { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public Guid UserGuid { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [DataType(DataType.ImageUrl)]
        [Url]
        public string? UserAvatarURL { get; set; }

        [Required]
        public bool? Active { get; set; } = true;

        public int? CreatedByUser { get; set; }

    }
}
