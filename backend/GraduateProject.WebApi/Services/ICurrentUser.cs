using System.Security.Claims;

namespace GraduateProject.Services;

public interface ICurrentUser<TKey> where TKey: struct
{
    TKey? Id { get; }
    bool IsAuthenticated { get; }
    string? GetId();
    string UserName { get; }
    IEnumerable<string> Roles { get; }
    IEnumerable<Claim> GetAllClaims();
}