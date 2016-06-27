using Newtonsoft.Json;

namespace Owin.Security.Providers.SurveyMonkey.Models
{
    internal class Group
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }
    }
}