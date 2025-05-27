    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Entity.DTOs
    {
        public class FormModuleDto : BaseDto
        {
            public int formid { get; set; }
            public string? formname { get; set; }
            public int moduleid { get; set; }
            public string? modulename { get; set; }

        }
    }
