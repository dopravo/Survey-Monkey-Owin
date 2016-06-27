using Owin.Security.Providers.SurveyMonkey.Models;
using Owin.Security.Providers.SurveyMonkey.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Owin.Security.Providers.SurveyMonkey
{
    /// <summary>
    /// Extension methods for using <see cref="SurveyMonkeyAuthenticationMiddleware"/>
    /// </summary>
    public static class SurveyMonkeyAuthenticationExtensions
    {
        /// <summary>
        /// Authenticate users using SurveyMonkey Account.
        /// </summary>
        /// <param name="app">The <see cref="IAppBuilder"/> passed to the configuration method</param>
        /// <param name="options">The <see cref="SurveyMonkeyAuthenticationOptions"/> passed to the configuration method</param>
        public static IAppBuilder UseSurveyMonkeyAuthentication(this IAppBuilder app, SurveyMonkeyAuthenticationOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }
            return app.Use(typeof(SurveyMonkeyAuthenticationMiddleware), app, options);
        }

        /// <summary>
        /// Authenticate users using SurveyMonkey Account.
        /// </summary>
        /// <param name="app">The <see cref="IAppBuilder"/> passed to the configuration method</param>
        /// <param name="clientId">The application client ID assigned by the  authentication service</param>
        /// <param name="clientSecret">The application client secret assigned by the  authentication service</param>
        /// <param name="callbackPath">The return URL specified in the SurveyMonkey API App</param>
        /// <param name="apiKey">The SurveyMonkey API Key</param>

        public static IAppBuilder UseSurveyMonkeyAuthentication(this IAppBuilder app, string clientId, string clientSecret, string callbackPath, string apiKey)
        {
            Error Errors = new Error();
            SurveyMonkeyAuthenticationOptions surveyMonkeyAuthenticationOptions = new SurveyMonkeyAuthenticationOptions(Constants.DefaultAuthenticationType, apiKey, callbackPath);

            surveyMonkeyAuthenticationOptions.ClientId = clientId;
            surveyMonkeyAuthenticationOptions.ClientSecret = clientSecret;
            surveyMonkeyAuthenticationOptions.Provider = new SurveyMonkeyAuthenticationProvider
            {
                OnAuthenticated = async context =>
                {
                    if (context.AccountType != "select_yearly" && context.AccountType != "basic")
                    {
                        MappedResult<GroupRequestResult> group = await context.Client.DownloadJsonAsync<GroupRequestResult>(surveyMonkeyAuthenticationOptions.Endpoints.UserGroupEndpoint);
                        if (group.IsSuccessfull)
                        {
                            var groupData = group.Result.Data;
                            string groupMembersUri = string.Format(surveyMonkeyAuthenticationOptions.Endpoints.GroupMemberEndpoint, groupData.First().Id);
                            MappedResult<GroupMembersRequestResult> groupMembers = await context.Client.DownloadJsonAsync<GroupMembersRequestResult>(groupMembersUri);
                            if (groupMembers.IsSuccessfull)
                            {
                                List<Member> groupMembersData = groupMembers.Result.Data;
                                Member memberData = groupMembersData.Where(e => e.Username == context.UserName).First();
                                string groupMemberUri = string.Format(surveyMonkeyAuthenticationOptions.Endpoints.MemberEndpoint, group.Result.Data.First().Id, memberData.Id);
                                MappedResult<MemberRequestResult> groupMember = await context.Client.DownloadJsonAsync<MemberRequestResult>(groupMemberUri);

                                if (groupMember.IsSuccessfull)
                                {
                                    var UserGroup = group.Result.Data.First().Name;
                                    var Role = groupMember.Result.Type;

                                    context.Identity.AddClaim(new Claim("urn:" + context.AuthenticationType + ":access_token", context.AccessToken, Constants.XmlSchemaString, context.AuthenticationType));
                                    if (!string.IsNullOrWhiteSpace(UserGroup))
                                    {
                                        context.Identity.AddClaim(new Claim("urn:" + context.AuthenticationType + ":group", UserGroup, Constants.XmlSchemaString, context.AuthenticationType));
                                    }

                                    if (!string.IsNullOrWhiteSpace(Role))
                                    {
                                        context.Identity.AddClaim(new Claim(ClaimTypes.Role, Role, Constants.XmlSchemaString, context.AuthenticationType));
                                    }
                                }
                                else
                                {
                                    Errors.Add("member", groupMember.ReasonPhrase);
                                }
                            }
                            else
                            {
                                Errors.Add("members", groupMembers.ReasonPhrase);
                            }
                        }
                        else
                        {
                            Errors.Add("groups", group.ReasonPhrase);
                        }
                    }

                    if (Errors.Count > 0)
                    {
                        await Task.FromResult(Errors);
                    }
                },
                OnApplyRedirect = context => context.Response.Redirect(context.RedirectUri)
            };

            return UseSurveyMonkeyAuthentication(
                app,
                surveyMonkeyAuthenticationOptions
            );
        }
    }
}