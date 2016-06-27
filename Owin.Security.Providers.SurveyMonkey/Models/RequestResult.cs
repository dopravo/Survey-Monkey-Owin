using Newtonsoft.Json;
using System.Collections.Generic;

namespace Owin.Security.Providers.SurveyMonkey.Models
{
    internal class RequestResult
    {
        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("links")]
        public IDictionary<string, string> Links { get; set; }
    }
}