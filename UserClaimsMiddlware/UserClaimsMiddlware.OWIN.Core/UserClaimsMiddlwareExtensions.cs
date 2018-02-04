using Owin;

namespace UserClaimsMiddlware.OWIN.Core
{
    public static class AppBuilderExtensions
    {
        //default options
        public static IAppBuilder UseUserClaims(this IAppBuilder app)
        {
            var options = new UserClaimsOptions();
            return app.Use<UserClaimsMiddleware>(options);
        }

        //with options
        public static IAppBuilder UseUserClaims(this IAppBuilder app,
            UserClaimsOptions options)
        {
            return app.Use<UserClaimsMiddleware>(options);
        }
    }
}