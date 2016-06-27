using Newtonsoft.Json;
using System.Collections.Generic;

namespace Owin.Security.Providers.SurveyMonkey.Models
{
    internal class GroupRequestResult : RequestResult
    {
        [JsonProperty("data")]
        public List<Group> Data { get; set; }
    }
}