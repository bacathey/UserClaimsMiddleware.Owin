using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace UserClaimsMiddlware.OWIN.Core
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class UserClaimsMiddleware
    {
        private readonly AppFunc _next;
        private readonly UserClaimsOptions _options;

        public UserClaimsMiddleware(AppFunc next, UserClaimsOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            if (environment == null) throw new ArgumentNullException(nameof(environment));

            var envCopy = environment;

            var context = new OwinContext(environment);
            var userPrincipal = context.Authentication.User;

            var newUserClaims = ReadyNewClaimsList(userPrincipal);

            if (_options.UseCaching)
            {
                newUserClaims.AddRange(await _options.ClaimsCacheManager.GetClaimsFromCache(context));
            }
            else
            {
                var newClaims = await _options.ProviderRunner.RunAllProviderTasksAsync(envCopy, _options.Providers);

                await _options.ClaimsCacheManager.SetClaimsInCache(context, newClaims);

                newUserClaims.AddRange(newClaims);
            }

            var identity = new ClaimsIdentity(newUserClaims, context.Authentication.User.Identity.AuthenticationType);
            var newPrincipal = new ClaimsPrincipal(identity);
            context.Request.User = newPrincipal;

            await _next.Invoke(environment);
        }

        private static List<Claim> ReadyNewClaimsList(ClaimsPrincipal userPrincipal)
        {
            if (userPrincipal == null)
                throw new InvalidOperationException("No authenticated user provided in context");

            var newClaims = new List<Claim>();
            if (userPrincipal.Claims != null && userPrincipal.Claims.Any())
                newClaims.AddRange(userPrincipal.Claims);
            return newClaims;
        }

    }
}
