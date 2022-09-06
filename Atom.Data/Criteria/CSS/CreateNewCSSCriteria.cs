using Atom.Domain.Attribute;
using Atom.Domain.Enum;

namespace Atom.Data.Criteria.Security
{
    [DataAccess(DataAccessMethod.StoredProcedure, name: "css.up_SaveCSS")]
    public class CreateNewCSSCriteria : CriteriaBase
    {
        public string CSSVariableTypeId { get; set; }
        public string CSSVariableValue { get; set; }
    
    }
}
