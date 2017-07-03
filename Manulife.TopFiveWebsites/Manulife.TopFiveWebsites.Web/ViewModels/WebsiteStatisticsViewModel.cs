using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manulife.TopFiveWebsites.Web.ViewModels
{
    public class WebsiteStatisticsViewModel
    {
        public DateTime Date { get; set; }
        public string Website { get; set; }
        public long TotalVisits { get; set; }
    }
}