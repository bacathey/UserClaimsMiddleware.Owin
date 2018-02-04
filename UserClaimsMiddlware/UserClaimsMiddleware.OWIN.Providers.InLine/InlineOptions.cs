using UserClaimsMiddlware.OWIN.Core;

namespace UserClaimsMiddleware.OWIN.Providers.InLine
{
    public class InLineOptions : IClaimsProviderOptions
    {
        public bool IgnoreExceptions { get; set; } = true;
    }
}