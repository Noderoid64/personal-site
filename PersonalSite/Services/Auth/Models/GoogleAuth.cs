using Newtonsoft.Json;

namespace PersonalSite.Services.Auth.Models;

public class GoogleAuth
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
    [JsonProperty("scope")]
    public string Scope { get; set; }
}