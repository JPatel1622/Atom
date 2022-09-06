using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Atom.Data.BaseClass
{
    public class ModelBase
    {
        [DataType(dataType: DataType.Date)]
        public DateTime CreatedDateTime { get; set; }

        [DataType(dataType: DataType.DateTime)]
        public DateTime UpdatedDateTime { get; set; }
    }
}
