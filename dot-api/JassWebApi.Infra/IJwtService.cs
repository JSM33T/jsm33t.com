using JassWebApi.Entities;

namespace JassWebApi.Infra
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}
