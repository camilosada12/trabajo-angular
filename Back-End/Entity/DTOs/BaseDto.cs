﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class BaseDto
    {
        public int Id {  get; set; }

        public bool Active { get; set; }
        public bool isDeleted { get; set; }
    }
}
