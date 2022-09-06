using Atom.Domain.Attribute;

namespace Atom.Data.Criteria.Security
{
    [DataAccess(Domain.Enum.DataAccessMethod.StoredProcedure, "sec.up_GetUserRoles")]
    public class GetRolesCriteria : CriteriaBase
    {
        public int UserId { get; set; }
    }
}
