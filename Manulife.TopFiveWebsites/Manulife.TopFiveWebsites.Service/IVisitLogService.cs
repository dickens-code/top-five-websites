using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manulife.TopFiveWebsites.Service
{
    public interface IVisitLogService
    {
        void PersistExclusionEntries();
        int ImportVisitLogSource(TextReader reader);
    }
}
