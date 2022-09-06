using Atom.Domain.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.Data.Criteria.Security
{
    [DataAccess(Domain.Enum.DataAccessMethod.StoredProcedure, "sec.up_GetUser")]
    public class GetUserCriteria : CriteriaBase
    {
        public Guid UserGuid { get; set; }
    }
}
