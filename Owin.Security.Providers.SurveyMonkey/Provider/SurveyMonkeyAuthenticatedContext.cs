using Dopravo.Utilities;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json.Linq;
using Owin.Security.Providers.SurveyMonkey.Properties;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;

namespace Owin.Security.Providers.SurveyMonkey.Provider
{
    /// <summary>
    /// Contains information about the login session as well as the user <see cref="ClaimsIdentity"/>.
    /// </summary>
    public class SurveyMonkeyAuthenticatedContext : BaseContext
    {
        /// <summary>
        /// Initializes a <see cref="SurveyMonkeyAuthenticatedContext"/>
        /// </summary>
        /// <param name="context">The OWIN environment</param>
        /// <param name="client"></param>
        /// <param name="authenticationType"></param>
        /// <param name="user">The JSON-serialized user</param>
        /// <param name="accessToken">The access token provided by the Survey Monkey authentication service</param>
        public SurveyMonkeyAuthenticatedContext(IOwinContext context, HttpClient client, string authenticationType, JObject user, string accessToken) : base(context)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            IDictionary<string, JToken> userAsDictionary = user;

            User = user;
            Client = client;
            AccessToken = accessToken;
            AuthenticationType = authenticationType;

            JToken userId = User["id"];
            if (userId == null)
            {
                throw new ArgumentException(Resources.Exception_MissingId, "user");
            }

            Id = userId.ToString();
            UserName = userAsDictionary.PropertyValueIfExists("username");
            FirstName = userAsDictionary.PropertyValueIfExists("first_name");
            LastName = userAsDictionary.PropertyValueIfExists("last_name");
            AccountType = userAsDictionary.PropertyValueIfExists("account_type");
            Language = userAsDictionary.PropertyValueIfExists("language");
            Email = userAsDictionary.PropertyValueIfExists("email");
            DateCreated = userAsDictionary.PropertyValueIfExists("date_created");
        }

        /// <summary>
        /// Gets the JSON-serialized user
        /// </summary>
        public JObject User { get; private set; }

        /// <summary>
        /// Gets the access token provided by the Survey Monkey authentication service
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// Gets the Survey Monkey Account user ID
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the user name
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets the user first name
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the user last name
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets the user email address
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the <see cref="ClaimsIdentity"/> representing the user
        /// </summary>
        public ClaimsIdentity Identity { get; set; }

        /// <summary>
        /// Gets or sets a property bag for common authentication properties
        /// </summary>
        public AuthenticationProperties Properties { get; set; }

        /// <summary>
        /// Gets or sets the Account Type of the SurveyMonkey User.
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        ///  Gets or sets the Language of the SurveyMonkey Account.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the Type of service user was Authenticated through.
        /// </summary>
        public string AuthenticationType { get; set; }

        /// <summary>
        /// Gets or sets the Date that the surveyMonkey Account was created.
        /// </summary>
        public string DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the Client that the requests will be made through.
        /// </summary>
        public HttpClient Client { get; set; }
    }
}