using Newtonsoft.Json;

namespace Owin.Security.Providers.SurveyMonkey.Models
{
    internal class MemberRequestResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }
    }
}