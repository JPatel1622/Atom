using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.Domain.Enum
{
    public enum DataAccessMethod
    {
        StoredProcedure = CommandType.StoredProcedure
    }

    public enum Operation
    {
        Insert = 1,
        Update = 2
    }
}
