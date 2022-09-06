using Atom.Domain.Attribute;
using Atom.Domain.Enum;

namespace Atom.Data.Criteria.Security
{
    [DataAccess(DataAccessMethod.StoredProcedure, "sec.up_RemoveUserFavoriteEvent")]
    public class RemoveUserFavoriteEventsCriteria : CriteriaBase
    {
        public int UserID { get; set; }
        public EventType EventTypeID { get; set; }
        public string EventID { get; set; }
    }
}
