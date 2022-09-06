using Atom.Data.BaseClass;
using Atom.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.Data.Model.CSS
{
    public class CSSRuleModel : ModelBase
    {
       

        public int CSSId { get; set; }

        public int UserId { get; set; }

        public Guid CSSGuid { get; set; }

        public CSSVariableEnum CSSVariableId { get; set; }

        public string CSSVariableValue { get; set; }
        public bool Active { get; set; } = true;
    }
}
