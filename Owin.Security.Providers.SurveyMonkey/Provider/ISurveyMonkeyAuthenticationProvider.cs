using Microsoft.Owin.Security;
using System.Threading.Tasks;

namespace Owin.Security.Providers.SurveyMonkey.Provider
{
    /// <summary>
    /// Specifies callback methods which the <see cref="SurveyMonkeyAuthenticationMiddleware"></see> invokes to enable developer control over the authentication process. />
    /// </summary>
    public interface ISurveyMonkeyAuthenticationProvider
    {
        /// <summary>
        /// Called when a Challenge causes a redirect to authorize endpoint in the SurveyMonkey middleware
        /// </summary>
        /// <param name="context">Contains redirect URI and <see cref="AuthenticationProperties"/> of the challenge </param>

        void ApplyRedirect(SurveyMonkeyApplyRedirectContext context);

        /// <summary>
        /// Invoked whenever SurveyMonkey successfully authenticates a user
        /// </summary>
        /// <param name="context">Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.</param>
        /// <returns>A <see cref="Task"/> representing the completed operation.</returns>

        Task Authenticated(SurveyMonkeyAuthenticatedContext context);

        /// <summary>
        /// Invoked prior to the <see cref="System.Security.Claims.ClaimsIdentity"/> being saved in a local cookie and the browser being redirected to the originally requested URL.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>A <see cref="Task"/> representing the completed operation.</returns>
        Task ReturnEndpoint(SurveyMonkeyReturnEndpointContext context);
    }
}