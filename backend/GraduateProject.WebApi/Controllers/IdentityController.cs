using System.Net;
using GraduateProject.Application.Extensions;
using GraduateProject.Application.Ums.Dto;
using GraduateProject.Domain.Common;
using GraduateProject.Domain.Ums.Entities;
using GraduateProject.Domain.Ums.Repositories;
using GraduateProject.Extensions;
using GraduateProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Controllers;

[Authorize]
[Route("/api/identity")]
public class IdentityController : ControllerBase
{
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly UserManager<UserAccount> _userManager;
    private readonly SignInManager<UserAccount> _signInManager;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IObjectMapper _mapper;
    private readonly ICurrentUser<Guid> _currentUser;
    private readonly IValidator _validator;

    public IdentityController(IUserAccountRepository userAccountRepository, UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager,
        IRoleRepository roleRepository, IUnitOfWork unitOfWork, IObjectMapper mapper, ICurrentUser<Guid> currentUser, IValidator validator)
    {
        _userAccountRepository = userAccountRepository;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUser = currentUser;
        _validator = validator;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ApiResponse<object>> LoginAction([FromBody] LoginForm form)
    {
        if (ModelState.IsValid && _validator.Validate<LoginForm>(form, out var validateResult))
        {
            var user = await _userAccountRepository.Queryable()
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserName == form.UserName);
            if (user is null) return ApiResponse<object>.Fail("User not found!");
            var identityResult = await _signInManager.PasswordSignInAsync(form.UserName, form.Password, form.RememberMe, false);
            if (identityResult.Succeeded)
            {
                return ApiResponse<object>.Ok(await this.GetUserProfile(user));
            }
            else
            {
                ModelState.AddModelError("IncorrectCredentials", "Tài khoản hoặc mật khẩu sai. Vui lòng nhập lại!");
                return ApiResponse<object>.Fail(GetModelStateMessages(ModelState));
            }
        }
        return ApiResponse<object>.Fail(GetModelStateMessages(ModelState));
    }

    [AllowAnonymous]
    [HttpGet("check-login")]
    public async Task<ApiResponse<object>> CheckLoginAction()
    {
        if (!_currentUser.IsAuthenticated) return ApiResponse<object>.Fail("Login fail!", null, (int) HttpStatusCode.Unauthorized);
        var user = await _userAccountRepository.Queryable()
            .Include(x => x.UserRoles).ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == _currentUser.Id);
        return ApiResponse<object>.Ok(await this.GetUserProfile(user));
    }

    [AllowAnonymous]
    [HttpPost("logout")]
    public async Task<ApiResponse> Logout()
    {
        await _signInManager.SignOutAsync();
        return ApiResponse.Ok();
    }


    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ApiResponse<object>> Register([FromBody] RegisterForm form)
    {
        if (ModelState.IsValid)
        {
            var newUser = new UserAccount()
            {
                UserName = form.UserName,
                PhoneNumber = form.PhoneNumber,
                Email = form.Email,
            };
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var identityResult = await _userManager.CreateAsync(newUser, form.Password);
                if (identityResult.Succeeded)
                {
                    var userRole = await _roleRepository.GetUserRole();
                    await _roleRepository.AddUserRole(newUser.Id, userRole.Id);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitTransactionAsync();
                    // var userProfile = await GetUserProfile(newUser);
                    return ApiResponse<object>.Ok();
                }
                else
                {
                    return ApiResponse<object>.Fail(string.Join("\n", identityResult.Errors.Select(x => $"{x.Code} - {x.Description}")));
                }
            }
            catch (Exception e)
            {
                await _unitOfWork.RollBackTransactionAsync();
                return ApiResponse<object>.Fail(e.Message);
            }
        }

        return ApiResponse<object>.Fail("Register fail");
    }

    private async Task<object> GetUserProfile(UserAccount userAccount)
    {
        List<string> claims;
        if (userAccount.HasRoleAdminSystem())
        {
            var allRoles = await _roleRepository.ToListAsync();
            claims = allRoles.Select(x => x.Code).ToList();
        }
        else
        {
            claims = userAccount.GetRoles().Select(x => x.Code).ToList();
        }

        var userAccountDto = _mapper.Map<UserAccount, UserInfoDto>(userAccount);
        return new
        {
            user = userAccountDto,
            rights = claims,
            isAdmin = userAccount.HasRoleAdminSystem(),
        };
    }
    private string GetModelStateMessages(ModelStateDictionary modelStateDictionary) => string.Join(" | ", modelStateDictionary.Values
        .SelectMany(v => v.Errors)
        .Select(e => e.ErrorMessage));
}