using System.Net.Http.Headers;
using Newtonsoft.Json;
using PersonalSite.Infrastructure.Common.Models;
using PersonalSite.Services.Auth.Models;

namespace PersonalSite.Services.Auth.Services;

public class GoogleApi
{
    private readonly AuthConfig _authConfig;

    public GoogleApi(AuthConfig authConfig)
    {
        _authConfig = authConfig;
    }
    
    public async Task<Result<string?>> AuthAsync(string authCode)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage();
        request.Headers
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        request.Method = HttpMethod.Post;
        request.RequestUri = new Uri("https://oauth2.googleapis.com/token?code=" + authCode + 
                                     "&client_id=" + _authConfig.GoogleClientId +
                                     "&client_secret=" + _authConfig.GoogleSecret +
                                     "&redirect_uri=" + _authConfig.GoogleRedirectURI +
                                     "&grant_type=authorization_code");
        
        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var output = await response.Content.ReadAsStringAsync();
            var googleAuth =  JsonConvert.DeserializeObject<GoogleAuth>(output);
            return Result<string?>.Success(googleAuth.AccessToken);
        }
        return Result<string?>.Fail("Google authentication failed");
    }

    public async Task<Result<GoogleProfile?>> GetProfile(string accessToken)
    {
        var client = new HttpClient();
        var profileRequest =
            new HttpRequestMessage(HttpMethod.Get, "https://people.googleapis.com/v1/people/me?personFields=names,emailAddresses,photos");
        profileRequest.Headers.Add("Authorization", "Bearer " + accessToken);
        var profileResponse = await client.SendAsync(profileRequest);
        if (profileResponse.IsSuccessStatusCode)
        {
            var output = await profileResponse.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(output);

            return Result<GoogleProfile?>.Success(AssembleGProfile(obj));
        }
        return Result<GoogleProfile?>.Fail("Google profile get failed");
    }

    private GoogleProfile AssembleGProfile(dynamic obj)
    {
        GoogleProfile gp = new GoogleProfile()
        {
            SourceId = obj.names[0].metadata.source.id,
            FirstName = obj.names[0].givenName,
            LastName = obj.names[0].familyName,
        };

        if (obj.photos != null)
        {
            foreach (var photo in obj.photos)
            {
                if (photo?.metadata?.primary != null && bool.Parse(photo.metadata.primary.ToString()))
                {
                    gp.ProfilePicture = photo.url.ToString();
                }
            }
        }

        return gp;
    }
}