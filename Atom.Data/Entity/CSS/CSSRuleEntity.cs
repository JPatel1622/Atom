using Atom.Data.BaseClass;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.Data.Entity.CSS
{
    [Table("[css].[Rule]")]
    public class CSSRuleEntity : EntityBase
    {

        [Key]
        public int CSSId {  get; set; }

        public int UserId { get; set; }

        [Computed]
        [Key]
        public Guid CSSGuid { get; set; }

        public int CSSVariableId { get; set; }
        
        public string CSSVariableValue { get; set; }

        public bool Active { get; set; } = true;

    }
}
