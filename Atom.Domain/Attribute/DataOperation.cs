using Atom.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.Domain.Attribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DataOperation : System.Attribute
    {
        public Operation Operation { get; set; }

        public DataOperation(Operation operation)
        {
            this.Operation = operation;
        }
    }
}
