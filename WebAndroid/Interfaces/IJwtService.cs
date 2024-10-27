using System.Security.Claims;
using WebAndroid.Data.Entities;

namespace WebAndroid.Interfaces
{
    public interface IJwtService
    {
        Task<string> CreateTokenAsync(EntityUser user);
        Task<IEnumerable<Claim>> GetClaimsAsync(EntityUser user);
    }
}
