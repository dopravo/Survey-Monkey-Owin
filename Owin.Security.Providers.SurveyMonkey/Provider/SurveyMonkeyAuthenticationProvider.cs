using Microsoft.Owin.Security;
using System;
using System.Threading.Tasks;

namespace Owin.Security.Providers.SurveyMonkey.Provider
{
    /// <summary>
    /// Default <see cref="ISurveyMonkeyAuthenticationProvider"/> implementation.
    /// </summary>
    public class SurveyMonkeyAuthenticationProvider : ISurveyMonkeyAuthenticationProvider
    {
        /// <summary>
        /// Initializes a new <see cref="SurveyMonkeyAuthenticationProvider"/>
        /// </summary>
        public SurveyMonkeyAuthenticationProvider()
        {
        }

        /// <summary>
        /// Gets or sets the delegate that is invoked when the ApplyRedirect method is invoked.
        /// </summary>
        public Action<SurveyMonkeyApplyRedirectContext> OnApplyRedirect
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the function that is invoked when the Authenticated method is invoked.
        /// </summary>
        public Func<SurveyMonkeyAuthenticatedContext, Task> OnAuthenticated
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the function that is invoked when the ReturnEndpoint method is invoked.
        /// </summary>
        public Func<SurveyMonkeyReturnEndpointContext, Task> OnReturnEndpoint
        {
            get; set;
        }

        /// <summary>
        /// Called when a Challenge causes a redirect to authorize endpoint in the SurveyMonkey account middleware
        /// </summary>
        /// <param name="context">Contains redirect URI and <see cref="AuthenticationProperties"/> of the challenge </param>
        public virtual void ApplyRedirect(SurveyMonkeyApplyRedirectContext context)
        {
            OnApplyRedirect(context);
        }

        /// <summary>
        /// Invoked whenever Microsoft successfully authenticates a user
        /// </summary>
        /// <param name="context">Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.</param>
        /// <returns>A <see cref="Task"/> representing the completed operation.</returns>
        public virtual Task Authenticated(SurveyMonkeyAuthenticatedContext context)
        {
            return OnAuthenticated(context);
        }

        /// <summary>
        /// Invoked prior to the <see cref="System.Security.Claims.ClaimsIdentity"/> being saved in a local cookie and the browser being redirected to the originally requested URL.
        /// </summary>
        /// <param name="context">Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/></param>
        /// <returns>A <see cref="Task"/> representing the completed operation.</returns>
        public virtual Task ReturnEndpoint(SurveyMonkeyReturnEndpointContext context)
        {
            return OnReturnEndpoint(context);
        }
    }
}