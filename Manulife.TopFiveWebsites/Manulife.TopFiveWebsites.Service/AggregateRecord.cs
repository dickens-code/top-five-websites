using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manulife.TopFiveWebsites.Service
{
    public class AggregateRecord
    {
        public DateTime Date { get; set; }
        public string Website { get; set; }
        public long TotalVisits { get; set; }
    }
}
