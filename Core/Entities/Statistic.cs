using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Statistic : BaseEntity
    {
        public int Count { get; set; }
        public string Title { get; set; }
    }
}
