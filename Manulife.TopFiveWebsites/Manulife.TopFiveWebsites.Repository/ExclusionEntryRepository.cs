using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Manulife.TopFiveWebsites.Repository
{
    public class ExclusionEntryRepository : IExclusionEntryRepository
    {
        private readonly Uri _baseAddress = new Uri("http://private-1de182-mamtrialrankingadjustments4.apiary-mock.com/");

        public IQueryable<T> GetEntities<T>() where T : class
        {
            return null;
        }

        private async Task<string> GetExclusionContent()
        {
            //Common testing requirement. If you are consuming an API in a sandbox/test region, uncomment this line of code ONLY for non production uses.
            //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            using (var httpClient = new HttpClient { BaseAddress = _baseAddress })
            {
                using (var response = await httpClient.GetAsync("exclusions"))
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
