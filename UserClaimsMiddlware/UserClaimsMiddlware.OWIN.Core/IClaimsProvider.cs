using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UserClaimsMiddlware.OWIN.Core
{
    public interface IClaimsProvider<TProviderOptions> where TProviderOptions : class, IClaimsProviderOptions
    {
        TProviderOptions ClaimsProviderOptions { get; set; }

        Task<List<Claim>> GetClaimsAsync(IDictionary<string, object> environment);
    }
}