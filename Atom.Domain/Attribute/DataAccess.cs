using Atom.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.Domain.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DataAccess : System.Attribute
    {
        public string Name { get; set; }

        public DataAccessMethod AccessMethod { get; set; }


        /// <param name="accessMethod">Table or Stored Proc access method</param>
        /// <param name="name">The name of the stored proc or table</param>
        public DataAccess(DataAccessMethod accessMethod, string name)
        {
            AccessMethod = accessMethod;   
            Name = name;
        }
    }
}
