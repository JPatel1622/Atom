using Atom.Domain.Attribute;
using Atom.Domain.Enum;

namespace Atom.Data.Criteria.Security
{
    [DataAccess(DataAccessMethod.StoredProcedure, "sec.up_GetUserFavoriteEvents")]
    public class GetUserFavoriteEventsCriteria : CriteriaBase
    {
        public int UserID { get; set; }
    }
}
