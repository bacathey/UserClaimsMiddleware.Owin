namespace UserClaimsMiddlware.OWIN.Core
{
    public interface IClaimsProviderOptions
    {
        // Should the middleware swallow exceptions that may occur on this provider
        // This prevents an exception from being thrown, but any claims normally sourced from this provider will be missing
        bool IgnoreExceptions { get; set; }
    }
}