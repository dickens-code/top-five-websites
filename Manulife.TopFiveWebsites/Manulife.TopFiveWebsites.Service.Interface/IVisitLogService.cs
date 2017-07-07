using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manulife.TopFiveWebsites.Service.Interface
{
    public interface IVisitLogService
    {
        void PersistExclusionEntries(bool forceRefresh);
        int ImportVisitLogSource(TextReader reader);
        void ResetVisitLog();
        void ResetVisitLogExclusion();
    }
}
