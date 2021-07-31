using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JeopardyApi.Models
{
    public class ValueDateLookupModel
    {
        public bool IsFinalQuestion { get; set; } = false;
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}
