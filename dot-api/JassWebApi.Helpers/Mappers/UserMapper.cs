using JassWebApi.Entities;

namespace JassWebApi.Helpers.Mappers
{
    public static class UserMapper
    {
        public static User ToUserEntity(this UserSignUpRequest request)
        {
            return new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Avatar = request.Avatar,
                UserName = string.IsNullOrWhiteSpace(request.UserName)
                    ? request.Email.Split('@')[0]
                    : request.UserName,
                Role = string.IsNullOrWhiteSpace(request.Role)
                    ? "user"
                    : request.Role,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsVerified = false
            };
        }
    }
}
