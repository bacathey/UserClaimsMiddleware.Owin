using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin;
using UserClaimsMiddleware.OWIN.Caching;

namespace UserClaimsMiddleware.OWIN.CookieCaching
{
    public class CookieCacheManager : IClaimsCacheManager
    {
        public CookieCacheOptions CookieCacheOptions { get; set; }

        public CookieCacheManager()
        {
            CookieCacheOptions = new CookieCacheOptions();
        }

        public CookieCacheManager(CookieCacheOptions cookieCacheOptions)
        {
            CookieCacheOptions = cookieCacheOptions ?? throw new ArgumentNullException(nameof(cookieCacheOptions));
        }

        public Task SetClaimsInCache(OwinContext context, IEnumerable<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Claim>> GetClaimsFromCache(OwinContext context)
        {
            throw new NotImplementedException();
        }

        public Task InvalidateCache(OwinContext context)
        {
            throw new NotImplementedException();
        }
    }
}
