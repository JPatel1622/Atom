using Atom.Domain.Attribute;
using Atom.Domain.Enum;

namespace Atom.Data.Criteria.Security
{
    [DataAccess(DataAccessMethod.StoredProcedure, name: "sec.up_CreateUser")]
    public class CreateNewUserCriteria : CriteriaBase
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int CreateUserID { get; set; }
        public bool ExternalAuth { get; set; }
    }
}
