using BikeCommunitySite.Models;
using System.Threading.Tasks;

namespace BikeCommunitySite.Services
{
    public interface IUserInfoService
    {
        Task<Auth0UserInfo> GetUserInfo(string accessToken);
    }
}
