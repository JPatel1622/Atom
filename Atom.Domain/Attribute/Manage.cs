using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Atom.Domain.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Manage : System.Attribute
    {
        public Manage()
        {

        }
    }
}
