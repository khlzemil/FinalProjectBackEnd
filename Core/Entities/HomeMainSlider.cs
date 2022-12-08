﻿using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class HomeMainSlider : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoName { get; set; }
        public string LearnMore { get; set; }
        public string UrlAdress { get; set; }
        public int Order{ get; set; }
    }
}
