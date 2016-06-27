using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin.Security.Providers.SurveyMonkey.Provider;
using System;
using System.Net.Http;

namespace Owin.Security.Providers.SurveyMonkey
{
    /// <summary>
    ///
    /// </summary>
    public class SurveyMonkeyAuthenticationEndpoints
    {
        /// <summary>
        /// Endpoint which is used to redirect users to request SurveyMonkey access
        /// </summary>
        /// <remarks>
        /// Defaults to https://api.surveymonkey.net/oauth/authorize?api_key={0}
        /// </remarks>
        public string AuthorizationEndpoint { get; set; }

        /// <summary>
        /// Endpoint which is used to exchange code for access token
        /// </summary>
        /// <remarks>
        /// Defaults to https://api.surveymonkey.net/v3/oauth/token?api_key={0}
        /// </remarks>
        public string TokenEndpoint { get; set; }

        /// <summary>
        /// Endpoint which is used to obtain user information after authentication
        /// </summary>
        /// <remarks>
        /// Defaults to https://api.surveymonkey.net/v3/users/me?api_key={0}
        /// </remarks>
        public string UserInfoEndpoint { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <remarks>
        /// Defaults to https://api.surveymonkey.net/v3/groups?api_key={0}
        /// </remarks>
        public string UserGroupEndpoint { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <remarks>
        /// Defaults to https://api.surveymonkey.net/v3/groups/{0}/members?api_key={1}
        /// </remarks>
        public string GroupMemberEndpoint { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <remarks>
        /// Defaults to https://api.surveymonkey.net/v3/groups/{0}/members/{1}?api_key={2}
        /// </remarks>
        public string MemberEndpoint { get; set; }
    }

    /// <summary>
    /// Configuration options for <see cref="SurveyMonkeyAuthenticationMiddleware"/>
    /// </summary>
    public class SurveyMonkeyAuthenticationOptions : AuthenticationOptions
    {
        const string AuthorizationEndPoint = "https://api.surveymonkey.net/oauth/authorize?api_key={0}";
        const string TokenEndpoint = "https://api.surveymonkey.net/v3/oauth/token?api_key={0}";
        const string UserInfoEndpoint = "https://api.surveymonkey.net/v3/users/me?api_key={0}";
        const string UserGroupEndpoint = "https://api.surveymonkey.net/v3/groups?api_key={0}";
        const string GroupMemberEndpoint = "https://api.surveymonkey.net/v3/groups/{0}/members";
        const string MemberEndpoint = "https://api.surveymonkey.net/v3/groups/{0}/members{1}";

        /// <summary>
        /// Initializes a new <see cref="SurveyMonkeyAuthenticationOptions"/>.
        /// </summary>
        public SurveyMonkeyAuthenticationOptions(string AuthenticationType, string ApiKey, string CallbackPath) : base(AuthenticationType)
        {
            Caption = AuthenticationType;
            this.CallbackPath = new PathString(CallbackPath);
            AuthenticationMode = AuthenticationMode.Passive;
            BackchannelTimeout = TimeSpan.FromSeconds(60);
            Endpoints = new SurveyMonkeyAuthenticationEndpoints
            {
                AuthorizationEndpoint = string.Format(AuthorizationEndPoint, ApiKey),
                TokenEndpoint = string.Format(TokenEndpoint, ApiKey),
                UserInfoEndpoint = string.Format(UserInfoEndpoint, ApiKey),
                UserGroupEndpoint = string.Format(UserInfoEndpoint, ApiKey),
                GroupMemberEndpoint = string.Concat(GroupMemberEndpoint, string.Format("?api_key={0}", ApiKey)),
                MemberEndpoint = string.Concat(MemberEndpoint, string.Format("?api_key={0}", ApiKey))
            };
        }

        /// <summary>
        /// Gets or sets the a pinned certificate validator to use to validate the endpoints used
        /// in back channel communications belong to Survey Monkey.
        /// </summary>
        /// <value>
        /// The pinned certificate validator.
        /// </value>
        /// <remarks>If this property is null then the default certificate checks are performed,
        /// validating the subject name and if the signing chain is a trusted party.</remarks>
        public ICertificateValidator BackchannelCertificateValidator { get; set; }

        /// <summary>
        /// Get or sets the text that the user can display on a sign in user interface.
        /// </summary>
        /// <remarks>
        /// The default value is 'SurveyMonkey'
        /// </remarks>
        public string Caption
        {
            get { return Description.Caption; }
            set { Description.Caption = value; }
        }

        /// <summary>
        /// The application client ID assigned by the Survey Monkey authentication service.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The application client secret assigned by the Survey Monkey authentication service.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets timeout value in milliseconds for back channel communications with Survey Monkey.
        /// </summary>
        /// <value>
        /// The back channel timeout.
        /// </value>
        public TimeSpan BackchannelTimeout { get; set; }

        /// <summary>
        /// The HttpMessageHandler used to communicate with Survey Monkey.
        /// This cannot be set at the same time as BackchannelCertificateValidator unless the value
        /// can be downcast to a WebRequestHandler.
        /// </summary>
        public HttpMessageHandler BackchannelHttpHandler { get; set; }

        /// <summary>
        /// The request path within the application's base path where the user-agent will be returned.
        /// The middleware will process this request when it arrives.
        /// </summary>
        public PathString CallbackPath { get; set; }

        /// <summary>
        /// Gets or sets the name of another authentication middleware which will be responsible for actually issuing a user <see cref="System.Security.Claims.ClaimsIdentity"/>.
        /// </summary>
        public string SignInAsAuthenticationType { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ISurveyMonkeyAuthenticationProvider"/> used to handle authentication events.
        /// </summary>
        public ISurveyMonkeyAuthenticationProvider Provider { get; set; }

        /// <summary>
        /// Gets or sets the type used to secure data handled by the middleware.
        /// </summary>
        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }

        /// <summary>
        ///
        /// </summary>
        public SurveyMonkeyAuthenticationEndpoints Endpoints { get; set; }
    }
}