using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UserClaimsMiddlware.OWIN.Core;

namespace UserClaimsMiddleware.OWIN.Providers.InLine
{
    public class InLine<TProviderOptions> : IClaimsProvider<TProviderOptions> where TProviderOptions : InLineOptions, new()
    {
        private readonly Func<IDictionary<string, object>, Task<List<Claim>>> _code;
        public readonly TProviderOptions InLineOptions;

        public InLine(Func<IDictionary<string, object>, Task<List<Claim>>> code)
        {
            _code = code ?? throw new ArgumentNullException(nameof(code));
            InLineOptions = new TProviderOptions();
        }

        public InLine(Func<IDictionary<string, object>, Task<List<Claim>>> code,
            TProviderOptions options)
        {
            _code = code ?? throw new ArgumentNullException(nameof(code));
            InLineOptions = options ?? throw new ArgumentNullException(nameof(options));
        }

        public TProviderOptions ClaimsProviderOptions { get; set; }

        public async Task<List<Claim>> GetClaimsAsync(IDictionary<string, object> environment)
        {
            try
            {
                return await _code.Invoke(environment);
            }
            catch
            {
                if (!InLineOptions.IgnoreExceptions)
                    throw;

                return new List<Claim>();
            }
        }
    }
}