using System;

namespace UserClaimsMiddleware.OWIN.CookieCaching
{
    public static class CookieCacheOptionDefaults
    {
        /// <summary>
        /// default to 5 minutes
        /// </summary>
        public static TimeSpan CookieTimeSpan = new TimeSpan(0, 0, 5, 0);

        /// <summary>
        /// The prefix used to provide a default CookieCacheOptions.CookiePrefix
        /// </summary>
        public const string CookiePrefix = ".UserClaims.";

        /// <summary>
        /// Whether the cookie should be marked secured or not. Defaults to SameAsRequest.
        /// </summary>
        public static CookieSecureOption CookieSecureOption = CookieSecureOption.Always;

    }
}
