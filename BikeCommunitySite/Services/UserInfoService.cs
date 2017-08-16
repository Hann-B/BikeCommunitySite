using BikeCommunitySite.Models;
using System.IO;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

namespace BikeCommunitySite.Services
{
    public class UserInfoService : IUserInfoService
    {
        readonly private Auth0Settings _auth0Settings;
        public UserInfoService(IOptions<Auth0Settings> optionsAccessor)
        {
            _auth0Settings = optionsAccessor.Value;
        }

        public async Task<Auth0UserInfo> GetUserInfo(string accessToken)
        {
            var auth0Settings = _auth0Settings;
            var getUserInfo = $"https://{auth0Settings.Domain}/userinfo";
            

            var request = (HttpWebRequest)WebRequest.Create(getUserInfo);
            request.Accept = "application/json";
            request.Headers["Authorization"] = $"Bearer {accessToken}";

            WebResponse response = await request.GetResponseAsync();

            var raw = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true, 1024, true))
            {
                raw = reader.ReadToEnd();
            }
            var userInfo = JsonConvert.DeserializeObject<Auth0UserInfo>(raw);

            return userInfo;
        }
    }
}
