using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;

namespace Owin.Security.Providers.SurveyMonkey.Provider
{
    /// <summary>
    /// Context passed when a Challenge causes a redirect to authorize endpoint in the SurveyMonkey account middleware
    /// </summary>
    public class SurveyMonkeyApplyRedirectContext : BaseContext<SurveyMonkeyAuthenticationOptions>
    {
        /// <summary>
        /// Creates a new context object.
        /// </summary>
        /// <param name="context">The OWIN request context</param>
        /// <param name="options">The SurveyMonkey account middleware options</param>
        /// <param name="properties">The authentication properties of the challenge</param>
        /// <param name="redirectUri">The initial redirect URI</param>
        public SurveyMonkeyApplyRedirectContext(IOwinContext context, SurveyMonkeyAuthenticationOptions options,
            AuthenticationProperties properties, string redirectUri) : base(context, options)
        {
            RedirectUri = redirectUri;
            Properties = properties;
        }

        /// <summary>
        /// Gets the URI used for the redirect operation.
        /// </summary>
        public string RedirectUri { get; private set; }

        /// <summary>
        /// Gets the authentication properties of the challenge
        /// </summary>
        public AuthenticationProperties Properties { get; private set; }
    }
}