using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manulife.TopFiveWebsites.Repository.Interface
{
    public class ExclusionEntry
    {
        public string host { get; set; }
        public DateTime? excludedSince { get; set; }
        public DateTime? excludedTill { get; set; }
    }
}
