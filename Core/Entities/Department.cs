﻿using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Department : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }
    }
}
