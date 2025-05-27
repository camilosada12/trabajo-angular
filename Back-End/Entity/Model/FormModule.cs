using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class FormModule : BaseModel
    {
        public int formid { get; set; }
        public int moduleid { get; set; }

        public virtual Form Form { get; set; }
        public virtual Module Module { get; set; }

    }
}
