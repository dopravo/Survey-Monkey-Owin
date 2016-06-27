using Newtonsoft.Json;

namespace Owin.Security.Providers.SurveyMonkey.Models
{
    internal class Member
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }
    }
}