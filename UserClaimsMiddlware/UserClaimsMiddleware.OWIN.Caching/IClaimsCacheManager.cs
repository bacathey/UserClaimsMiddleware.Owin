using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace UserClaimsMiddleware.OWIN.Caching
{
    public interface IClaimsCacheManager
    {
        Task SetClaimsInCache(OwinContext owinContext, IEnumerable<Claim> claims);
        Task<IEnumerable<Claim>> GetClaimsFromCache(OwinContext owinContext);
        Task InvalidateCache(OwinContext owinContext);
    }
}