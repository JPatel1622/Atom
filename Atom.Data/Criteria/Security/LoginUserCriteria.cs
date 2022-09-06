using Atom.Data.Entity.Security;
using Atom.Domain.Attribute;
using Atom.Domain.Enum;

namespace Atom.Data.Criteria.Security
{
    [DataAccess(DataAccessMethod.StoredProcedure, "sec.up_LoginUser")]
    public class LoginUserCriteria : CriteriaBase
    {
        public bool ExternalAuth { get; set; }

        public string EmailAddress { get; set; }

        public string? Password { get; set; }
    }
}
