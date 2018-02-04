using System.Collections.Generic;
using UserClaimsMiddleware.OWIN.Caching;

namespace UserClaimsMiddlware.OWIN.Core
{
    public class UserClaimsOptions
    {
        public bool UseCaching { get; set; }
        public IList<IClaimsProvider<IClaimsProviderOptions>> Providers { get; set; }

        //dependencies
        public IClaimsCacheManager ClaimsCacheManager { get; set; }
        public IClaimsProviderRunner ProviderRunner { get; set; }

        /// <summary>
        /// Sets cookie caching as a default
        /// </summary>
        public UserClaimsOptions()
        {
            UseCaching = UserClaimsOptionDefaults.UseCaching;
            ClaimsCacheManager = UserClaimsOptionDefaults.ClaimsCacheManager;

            ProviderRunner = new ClaimsProviderRunner();
        }
    }
}
