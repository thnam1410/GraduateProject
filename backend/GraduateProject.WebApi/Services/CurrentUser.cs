using System.Security.Claims;
using GraduateProject.Domain.Ums.Entities;
using GraduateProject.Domain.Ums.Repositories;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Services;

public class CurrentUser : ICurrentUser<Guid>
{
    private readonly ClaimsPrincipal _claimsPrincipal;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpContextAccessor _accessor;
    private UserAccount _userAccount;


    public CurrentUser(ClaimsPrincipal claimsPrincipal, IServiceProvider serviceProvider, IHttpContextAccessor accessor)
    {
        _claimsPrincipal = claimsPrincipal;
        _serviceProvider = serviceProvider;
        _accessor = accessor;
    }

    public Guid? Id
    {
        get
        {
            var id = _claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrWhiteSpace(id))
                return Guid.Parse(_claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));

            if (_accessor.HttpContext.User.Identity.AuthenticationType is IISDefaults.Negotiate
                || _accessor.HttpContext.User.Identity.AuthenticationType is IISDefaults.Ntlm
                || _accessor.HttpContext.User.Identity.AuthenticationType is IISDefaults.AuthenticationScheme)
            {
                var userRepo = _serviceProvider.GetRequiredService<IUserAccountRepository>();
                if (_accessor.HttpContext.User.Identity.Name != null)
                {
                    var username = _accessor.HttpContext.User.Identity.Name?.Split("\\")[1];
                    if (_userAccount != null) return _userAccount.Id;
                    var userFindTask = userRepo.Queryable().FirstOrDefaultAsync(x => x.UserName == username);
                    userFindTask.Wait();
                    _userAccount = userFindTask.Result;
                    return _userAccount?.Id;
                }
            }

            return null;
        }
    }

    public bool IsAuthenticated => _claimsPrincipal.Identity is {IsAuthenticated: true};

    public string? GetId() => Id?.ToString();

    public string UserName => _claimsPrincipal.FindFirstValue(ClaimTypes.Name);
    public IEnumerable<string> Roles { get; }

    public IEnumerable<Claim> GetAllClaims() => _claimsPrincipal.Claims;
}