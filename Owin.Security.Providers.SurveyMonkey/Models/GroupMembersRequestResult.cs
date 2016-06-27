using Newtonsoft.Json;
using System.Collections.Generic;

namespace Owin.Security.Providers.SurveyMonkey.Models
{
    internal class GroupMembersRequestResult : RequestResult
    {
        [JsonProperty("data")]
        public List<Member> Data { get; set; }
    }
}