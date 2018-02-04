using UserClaimsMiddleware.OWIN.Caching;
using UserClaimsMiddleware.OWIN.CookieCaching;

namespace UserClaimsMiddlware.OWIN.Core
{
    public static class UserClaimsOptionDefaults
    {
        public static bool UseCaching = true;

        public static IClaimsCacheManager ClaimsCacheManager = new CookieCacheManager();
    }
}