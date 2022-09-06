using Atom.Data.BaseClass;
using System;
using System.ComponentModel.DataAnnotations;

namespace Atom.Data.Model.Organization
{
    public class OrganizationModel : ModelBase
    {
        public int OrganizationId { get; set; }

        public Guid OrganizationGuid { get; set; }

        [Display(Name = "Organization Name")]
        [MaxLength(length: 50)]
        public string OrganizationName { get; set; }
    }
}
