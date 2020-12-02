using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IUtilityRepository
    {
        Task<IEnumerable<IUsefulDocumentation>> GetUsefulDocumentationsAsync(
            string page = null, 
            string category = null, 
            bool asNoTracking = true);
        Task<IEnumerable<IUsefulDocumentation>> GetUsefulDocumentationsByCategoryAsync(
            string category, 
            bool asNoTracking = true);
        Task<IEnumerable<IUsefulDocumentation>> GetUsefulDocumentationsByPageAsync(
            string page, 
            bool asNoTracking = true);
    }
}