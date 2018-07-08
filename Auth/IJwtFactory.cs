using System.Threading.Tasks;
using System.Security.Claims;
using System;

namespace crmApi.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, Guid id);
    }
}