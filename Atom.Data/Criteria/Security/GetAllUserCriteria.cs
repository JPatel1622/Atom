using Atom.Domain.Attribute;

namespace Atom.Data.Criteria.Security
{
    [DataAccess(Domain.Enum.DataAccessMethod.StoredProcedure, "sec.up_GetAllUsers")]
    public class GetAllUserCriteria : CriteriaBase
    {
        public bool? Active { get; set; }
    }
}
