using Atom.Domain.Attribute;
using Atom.Domain.Enum;
using System;

namespace Atom.Data.Criteria.Security
{
    [DataAccess(DataAccessMethod.StoredProcedure, name: "css.up_GetCSS")]
    public class GetCSSCriteria : CriteriaBase
    {
        public int CSSUserId { get; set; }
    
    }
}
